using UnityEngine;
using System.Collections;

public class WaitEnemy : Enemy {

    public float waitTime;
    bool hasWaited;
    bool waiting;

	// Use this for initialization
    protected override void Start()
    {
        base.Start();
    }
	
	// Update is called once per frame
    protected override void Update()
    {
        if(!waiting)
            base.Update();

        if (!hasWaited && transform.position.x - base.player.position.x < 0.5f)
        {
            hasWaited = true;
            waiting = true;
            anim.SetBool("Wait", true);
            

        }

        if (waiting)
        {
            waitTime -= Time.deltaTime;
            if (waitTime < 0f)
            {
                waiting = false;
                base.BasicAttack();
            }
        }
    }
}
