using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour {

    public BossHook Owner;
    [SerializeField] float IdleRadious;
    [SerializeField]
    float IdleSpeed;

    GameManager gm;


    string status = "idle";

    [SerializeField]HookTiler hookTiler;
    [Header("Sounds")]
    [SerializeField]
    AudioClip soundLaunch;
    [SerializeField] AudioClip soundOnExtend;

	// Use this for initialization
	void Start () {
        gm = GameObject.FindObjectOfType<GameManager>();
 
	}

    public void StartThrow()
    {//called by the animation
        status = "throw";
        AudioManager.PlayClip(soundLaunch);
        transform.rotation = Quaternion.Euler(0, 0, 0);
     

    }

    public void FinishThrow()
    {
        status = "idle";
        hookTiler.RetractChain();
        
    }


	
	// Update is called once per frame
	void LateUpdate () {

        //rotate around owner

        if(status=="idle")
        {
            float x = Owner.transform.position.x + IdleRadious * Mathf.Sin(Time.time * Mathf.Deg2Rad * IdleSpeed);
            float y = Owner.transform.position.y + IdleRadious * Mathf.Cos(Time.time * Mathf.Deg2Rad * IdleSpeed);
            transform.position = new Vector3(x, y, transform.position.z);
            //Debug.Log("tring to rotate to " + x.ToString());
            transform.rotation = Quaternion.Euler(0,0, Time.time * IdleSpeed);

        }

      
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject == Owner.Player.gameObject)
        {
            Owner.Player.GetComponent<RigidBodyHandler>().GetHit();
        }
           
    }

    public void HookExtended()
    {
        //change chain material
        hookTiler.ExtendChain();
        gm.camHandler.PlayBump();
        AudioManager.PlayClip(soundOnExtend);
        
        
    }


}
