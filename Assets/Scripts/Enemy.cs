using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    
    protected Transform player;
    protected Animator anim;
    protected bool hasAttacked;
    protected Rigidbody2D rb;

    protected ShadowTrail shadowTrail;

    bool hasKilledThePlayer;
    bool hasGivenPoints;

    [SerializeField]
    Sprite hitSprite;

    protected GameManager gm;

    public float perfectRange;

    

    protected SpriteRenderer sHitEffect;
	// Use this for initialization
	protected virtual void Start () {
        player = GameObject.FindObjectOfType<Hero>().transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        shadowTrail = gameObject.AddComponent<ShadowTrail>();   
    
        //instantiate hitSpriteEffect
        sHitEffect = new GameObject().AddComponent<SpriteRenderer>();
        
        sHitEffect.sprite = Resources.Load<Sprite>("Sprites/hitEffect");
        sHitEffect.transform.parent = transform;
        sHitEffect.transform.localPosition = Vector3.left * 0.1f ;
        sHitEffect.enabled = false;
        sHitEffect.material = Resources.Load("Materials/Flash") as Material;

        gm = GameObject.FindObjectOfType<GameManager>();
        

	}


    void CheckForPerfect()
    {

        float ydist = player.transform.position.y - transform.position.y;

        if (ydist < perfectRange && ydist >0.1f)
        {
            Debug.Log("Perfect!");
            gm.CallPerfect();

        }
        else {
            Debug.Log("no");
        }

    }

    protected IEnumerator Drop() {
        yield return new WaitForSeconds(0.3f);
        if (!hasKilledThePlayer)
        {
            anim.SetBool("Fall", true);
            rb.gravityScale = 1f;
            rb.drag = 0f;
            
        }

        shadowTrail.Stop();
        sHitEffect.enabled = false;
        
        
    
    }

   

    protected void BasicAttack() {
        CheckForPerfect();
        AudioManager.PlayClip(AudioManager.Instance.swing);
        sHitEffect.enabled = true;
        shadowTrail.Play();
        hasAttacked = true;
        anim.SetBool("Attack", true);
        rb.gravityScale = 0f;
        rb.AddForce(Vector2.left * 400f);
        StartCoroutine(Drop());
    
    }
	// Update is called once per frame
	protected virtual void Update () {

        if (!hasGivenPoints && transform.position.x < player.transform.position.x)
        {
            hasGivenPoints = true;
            gm.IncreaseScore(1);
        }

        if (hasAttacked)
            return;

        transform.position += new Vector3(- Time.deltaTime, 0f, 0f);

        if ( transform.position.x - player.position.x  < 0.4f)
        {

            BasicAttack();           

        }

        
        
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!player)
            return;
        if (col.gameObject == player.gameObject)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            anim.SetBool("Attack", true);
            hasKilledThePlayer = true;
        }     
            
            
       
    }
}
