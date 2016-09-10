using UnityEngine;
using System.Collections;

public class RigidBodyHandler : MonoBehaviour {

    Rigidbody2D rb;
    Player hero;
	// Use this for initialization
	void Start () {
        hero = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
	}
    public void GetHit()
    {
        if (hero.CheckIfHit())
            return;
        hero.GetHit();
        rb.isKinematic = false;
        rb.AddForce(Vector2.left*100f);
        rb.AddTorque(1f);

    }

    void OnCollisionEnter2D(Collision2D col) {
            

            GetHit();
    }
}
