using UnityEngine;
using System.Collections;

public class HammerEnemy : Enemy  {

    public ParticleSystem HammerParticles;
    public float StompDistance;
    bool hasStomped;
    bool waiting;

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

    public void ResumeAttack() {
        waiting = false;
        rb.AddForce(Vector2.left * 400f);

        base.BasicAttack();
    }

    public void HammerDown()
    {
        Player.cam.PlayBump();
        HammerParticles.Play();
        AudioManager.PlayClip(AudioManager.Instance.hammerDown);

        //check player to stun
        if(Player.transform.position.y < 0.1)
        {
            Player.enabled = false;          
        }        
    }

    protected override void Update()
    {
        if (hasAttacked) {
            return;
        }        

        if (!hasStomped)
        {
            if(transform.position.x < StompDistance) {
                anim.SetTrigger("HammerDown");
                Debug.Log("Call to stomp");
                hasStomped = true;
            } else {
                transform.position += new Vector3(- moveSpeed * Time.deltaTime, 0f, 0f);
            }            
        }       
	}
}
