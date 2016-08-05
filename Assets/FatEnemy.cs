using UnityEngine;
using System.Collections;

public class FatEnemy : Enemy  {

    public float StompDistance;
    bool hasStomped;
    [SerializeField]bool waiting;
    protected override void Start()
    {
        
        base.Start();
    }

    public void Stop()
    {
        anim.ResetTrigger("HammerDown");
        waiting = true;

        Debug.Log("Called to stop");
    }

    public void ResumeAttack()
    {
        waiting = false;
        Debug.Log("Called to resume");
    }

    public void HammerDown()
    {
        player.GetComponent<Hero>().cam.PlayBump();
    }

    protected override void Update()
    {

        

        if(!waiting)
            base.Update();
        //Debug.Log(StompDistance.ToString() + " vs " + transform.position.ToString());

        if(!hasStomped && StompDistance >  transform.position.x)
        {
            anim.SetTrigger("HammerDown");
            Debug.Log("Call to stomp");
            hasStomped = true;
            

        }
        
	}
}
