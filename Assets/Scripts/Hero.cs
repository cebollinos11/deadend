using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

    enum heroStatus { 
        idle,chargejump,jump,hitfloor,hit
    }

    public AnimationCurve jumpCurve;

    public float jumpSpeed;
    public float jumpHeight;

    heroStatus status;

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

    CameraHandler cam;
    GameManager gm;

    SpriteRenderer sr;
    public bool CheckIfHit() {
        return status == heroStatus.hit;
    }
    void SetSprite(Sprite sprite) {
        sr.sprite = sprite;
    }
	// Use this for initialization
	void Start () {
        gm = GameObject.FindObjectOfType<GameManager>();
        sr = GetComponent<SpriteRenderer>();
        status = heroStatus.idle;
        origPos = transform.position;
        cam = GameObject.FindObjectOfType<CameraHandler>();
        
	}

    public void GetHit()
    {
        //Time.timeScale = 0.5f;
        AudioManager.PlayClip(AudioManager.Instance.playerGetHit);
        status=heroStatus.hit;
        SetSprite(s_gethit);
        cam.followPlayer = true;
        //cam.GetComponent<EffectsManager>().RunDeath();
        Invoke("CameraOnKill", 1f);
        cam.PlayBump();
        //cam.transform.Rotate(new Vector3(0f, 0f, -2f));
        BloodParticleEmitter.Play();
        gm.PlayerGotKilled();

        Invoke("RestartGame", 3f);
        
    
    }

    void RestartGame()
    {

        Application.LoadLevel(Application.loadedLevel);
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

		if(status == heroStatus.idle){


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
				status = heroStatus.chargejump;

			}
			#else
			#if UNITY_ANDROID
			if(Input.GetTouch(0).phase == TouchPhase.Began) {
				SetSprite(s_chargejump);
				status = heroStatus.chargejump;
			}
			#endif
			#endif  
		}         
        
        


        if (status == heroStatus.chargejump)
        {
			#if UNITY_STANDALONE || UNITY_EDITOR
            if (Input.GetKeyUp(KeyCode.Space)) {
                jumptimer = 0f;
                SetSprite(s_jumpUp);
                status = heroStatus.jump;
                //cam.PlayBump();
            }
			#else
			#if UNITY_ANDROID
			if (Input.GetTouch(0).phase == TouchPhase.Ended) {
				jumptimer = 0f;
				SetSprite(s_jumpUp);
				status = heroStatus.jump;
				//cam.PlayBump();
			}
			#endif
			#endif
        }

        if (status == heroStatus.jump)
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
                status = heroStatus.hitfloor;
                hitFloorCount = 0f;
                cam.PlayBump();
            }
        }

        if (status == heroStatus.hitfloor)
        {
            hitFloorCount += Time.deltaTime;
            if (hitFloorCount > hitFloorDelay)
            {
                status = heroStatus.idle;
                SetSprite(s_idle1);
                idleAnimCount = 0.0f;
            }
        }
        
	}
}
