using UnityEngine;
using System.Collections;

public class HammerEnemy : Enemy {

    public ParticleSystem HammerParticles;
    public float StompDistance;
    bool hasStomped;

    private static string waitAnimName = "EnemyStomp";

    private float? waitTime;
    public float WaitTime {
        get {
            if(!waitTime.HasValue) {
                waitTime = 0;
                AnimationClip waitAnim = null;
                anim = GetComponent<Animator>();
                foreach (AnimationClip clip in anim.runtimeAnimatorController.animationClips) {
                    if (clip.name == waitAnimName) {
                        waitAnim = clip;
                        break;
                    }
                }
                if (waitAnim != null) {
                    waitTime = waitAnim.length;
                    //Debug.Log(WaitTime);
                }
            }
            return waitTime.Value;
        }
        set {
            waitTime = value;
        }
    }

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

        //Debug.Log("Called to stop");
    }

    public void ResumeAttack() {
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
        if(Player.transform.position.y < 0.007)
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
                //Debug.Log("Call to stomp");
                hasStomped = true;
            } else {
                transform.position += new Vector3(- moveSpeed * Time.deltaTime, 0f, 0f);
            }            
        }       
	}
}
