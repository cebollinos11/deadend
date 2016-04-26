using UnityEngine;
using System.Collections;

public class RigidBodyHandler : MonoBehaviour {

    Rigidbody2D rb;
    Hero hero;
	// Use this for initialization
	void Start () {
        hero = GetComponent<Hero>();
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
	}
    void GetHit()
    {
        if (hero.CheckIfHit())
            return;
        hero.GetHit();
        rb.isKinematic = false;
        rb.AddForce(Vector2.left*100f);
        rb.AddTorque(1f);

    }
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.E))
        {
            GetHit();
        }
	
	}

    void OnCollisionEnter2D(Collision2D col) {
            

            GetHit();
    }
}
