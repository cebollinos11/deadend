using UnityEngine;
using System.Collections;

public class FatEnemy : Enemy  {

    public ParticleSystem HammerParticles;
    public float StompDistance;
    bool hasStomped;
    bool waiting;

    protected override void Start()
    {
        
        base.Start();
    }

    void HideHitEffect()
    {
        hitSpriteEffect.enabled = false;
    }

    public void Stop()
    {
        anim.ResetTrigger("HammerDown");
        waiting = true;

    }

    public void ResumeAttack() {
        

        waiting = false;
        rb.AddForce(Vector2.left * 400f);

        base.BasicAttack();
    }

    public void HammerDown()
    {
        hitSpriteEffect.enabled = true;
        Invoke("HideHitEffect", 0.1f);
        Player.cam.PlayBump();
        HammerParticles.Play();
        AudioManager.PlayClip(AudioManager.Instance.hammerDown);

        //check player to stun
        if(Player.transform.position.y < 0.1)
        {

            Player.GetStunned();    
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
                hasStomped = true;
            } else {
                transform.position += new Vector3(- moveSpeed * Time.deltaTime, 0f, 0f);
            }            
        }       
	}
}
