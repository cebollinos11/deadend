using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public enum PlayerState { 
        Idle,
        ChargeJump,
        Jump,
        HitFloor,
        Hit
    }

    public float ChargeStart { get; set; }
    [SerializeField]
    private float minChargeTime = 0.1f;
    public bool HasCharged { get; set; }

    public AnimationCurve jumpCurve;

    public float jumpSpeed;
    public float jumpHeight;

    public PlayerState state;

    public Sprite s_idle1;
    public Sprite s_idle2;
    public Sprite s_chargejump;
    public Sprite s_jumpUp;
    public Sprite s_jumpDown;
    public Sprite s_hitFloor;
    public Sprite s_gethit;

    public float idleAnimSpeed;
    float idleAnimCount;

    public float hitFloorDelay;
    float hitFloorCount;

    Vector3 origPos;
    float previousy;

    float jumptimer;

    [SerializeField]
    ParticleSystem BloodParticleEmitter;

    [HideInInspector]
    public CameraHandler cam;
    GameManager gm;

    SpriteRenderer sr;
    public event System.Action OnDeath;


    public bool CheckIfHit() {
        return state == PlayerState.Hit;
    }
    void SetSprite(Sprite sprite) {
        sr.sprite = sprite;
    }
	// Use this for initialization
	void Start () {
        gm = GameObject.FindObjectOfType<GameManager>();
        sr = GetComponent<SpriteRenderer>();
        state = PlayerState.Idle;
        origPos = transform.position;
        cam = GameObject.FindObjectOfType<CameraHandler>();
        
	}

    public void GetHit()
    {
        //Time.timeScale = 0.5f;
        AudioManager.PlayClip(AudioManager.Instance.playerGetHit);
        state=PlayerState.Hit;
        SetSprite(s_gethit);
        cam.followPlayer = true;
        //cam.GetComponent<EffectsManager>().RunDeath();
        Invoke("CameraOnKill", 1f);
        cam.PlayBump();
        //cam.transform.Rotate(new Vector3(0f, 0f, -2f));
        BloodParticleEmitter.Play();

        if(OnDeath != null) {
            OnDeath();
        }
        

    }

    void CameraOnKill() {

        Vector3 target = GameObject.FindObjectOfType<Enemy>().transform.position;
        cam.zoomTo(target);

    }
	
	// Update is called once per frame
	void Update () {

    #if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R))
			Application.LoadLevel(Application.loadedLevel);
		#endif

		if(state == PlayerState.Idle) {

            idleAnimCount += Time.deltaTime;
			if (idleAnimCount > idleAnimSpeed)
			{
				if(sr.sprite != s_idle2)
					SetSprite(s_idle2);
				else
				{
					SetSprite(s_idle1);
				}
				idleAnimCount = 0f;
			}

			#if UNITY_STANDALONE || UNITY_EDITOR
			if (Input.GetKey(KeyCode.Space)) {
				SetSprite(s_chargejump);
				state = PlayerState.ChargeJump;
                ChargeStart = Time.time;


            }
#else
#if UNITY_ANDROID
			if(Input.GetTouch(0).phase == TouchPhase.Began || (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Moved)) {
				SetSprite(s_chargejump);
				state = PlayerState.ChargeJump;
                ChargeStart = Time.time;
			}
#endif
#endif
        }




        if (state == PlayerState.ChargeJump)
        {
            // Update Charged flag
            if(Time.time - ChargeStart > minChargeTime) {
                HasCharged = true;
            } else {
                HasCharged = false;
            }
			#if UNITY_STANDALONE || UNITY_EDITOR
            if (Input.GetKeyUp(KeyCode.Space)) {
                jumptimer = 0f;
                SetSprite(s_jumpUp);
                state = PlayerState.Jump;
                //cam.PlayBump();
            }
#else
#if UNITY_ANDROID
			if (Input.GetTouch(0).phase == TouchPhase.Ended) {
				jumptimer = 0f;
				SetSprite(s_jumpUp);
				state = PlayerState.Jump;
				//cam.PlayBump();
			}
#endif
#endif
        }

        if (state == PlayerState.Jump)
        {
            jumptimer += Time.deltaTime * jumpSpeed;
            float j = jumpCurve.Evaluate(jumptimer)*jumpHeight;
            transform.position = origPos+ new Vector3(0, j, 0);
            if (transform.position.y<previousy)
            {
                SetSprite(s_jumpDown);
            }

            previousy = transform.position.y;

            if (jumptimer > 1.0f)
            {
                SetSprite(s_hitFloor);
                state = PlayerState.HitFloor;
                hitFloorCount = 0f;
                cam.PlayBump();
            }
        }

        if (state == PlayerState.HitFloor)
        {
            hitFloorCount += Time.deltaTime;
            if (hitFloorCount > hitFloorDelay)
            {
                state = PlayerState.Idle;
                ChargeStart = Mathf.Infinity;
                SetSprite(s_idle1);
                idleAnimCount = 0.0f;
            }
        }
        
	}
}
