using UnityEngine;
using System.Collections;

public class WaitEnemy : Enemy {
    public float WaitTime;

    private float waitStart;
    private bool waiting;

	// Use this for initialization
    protected override void Start()
    {
        base.Start();
    }
	
	// Update is called once per frame
    protected override void Update()
    {
        if (hasAttacked) {
            return;
        }
        // Only move forward if not waiting
        if (!waiting) {
            // Move foward until close enough to the enemy
            transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0f, 0f);
            if (transform.position.x - Player.transform.position.x < 0.5f) {
                waiting = true;
                waitStart = Time.time;
                anim.SetBool("Wait", true);
            }
        } else {
            // Wait for waitTime seconds
            if (Time.time - waitStart > WaitTime) {
                waiting = false;
                base.BasicAttack();
            }
        }
    }

    public override bool EvaluatePerformance() {
        float yDist = Player.transform.position.y - transform.position.y;
        if (yDist > perfectMin && yDist < perfectMax && 
            (Player.state == Player.HeroState.idle || !Player.HasCharged)) {
            return true;
        }
        return false;
    }
}
