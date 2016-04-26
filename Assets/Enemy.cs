using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float speed;

    [SerializeField]
    Sprite attackFrame;

    Transform player;
    Animator anim;

    bool hasAttacked;

    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<Hero>().transform;
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (hasAttacked)
            return;

        transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);

        if ( transform.position.x - player.position.x  < 0.4f)
        {
            hasAttacked = true;
            //anim.enabled = false;
            //GetComponent<SpriteRenderer>().sprite = attackFrame;
            anim.SetBool("Attack", true);
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.AddForce(Vector2.left * 110f);
            

        }
            
        
        

        
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == player.gameObject)
        
            rb.velocity = Vector2.zero;
       
    }
}
