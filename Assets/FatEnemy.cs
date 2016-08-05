using UnityEngine;
using System.Collections;

public class FatEnemy : Enemy  {

    public ParticleSystem HammerParticles;
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
        rb.AddForce(Vector2.left * 400f);

        base.BasicAttack();
    }

    public void HammerDown()
    {

        Hero hero = player.GetComponent<Hero>();
        hero.cam.PlayBump();
        HammerParticles.Play();
        AudioManager.PlayClip(AudioManager.Instance.hammerDown);

        //check player to stun
        if(hero.transform.position.y < 0.1)
        {
            hero.enabled = false;
            
           
        }
        
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
