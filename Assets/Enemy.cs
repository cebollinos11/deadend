using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    
    protected Transform player;
    protected Animator anim;
    protected bool hasAttacked;
    protected Rigidbody2D rb;

    bool hasKilledThePlayer;
	// Use this for initialization
	protected virtual void Start () {
        player = GameObject.FindObjectOfType<Hero>().transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}


    protected IEnumerator Drop() {
        yield return new WaitForSeconds(0.3f);
        if (!hasKilledThePlayer)
        {
            anim.SetBool("Fall", true);
            rb.gravityScale = 1f;
            rb.drag = 0f;
        }
        
        
    
    }

    protected void BasicAttack() {

        hasAttacked = true;
        anim.SetBool("Attack", true);
        rb.gravityScale = 0f;
        rb.AddForce(Vector2.left * 400f);
        StartCoroutine(Drop());
    
    }
	// Update is called once per frame
	protected virtual void Update () {

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
        if (col.gameObject == player.gameObject)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            anim.SetBool("Attack", true);
            hasKilledThePlayer = true;
        }     
            
            
       
    }
}
