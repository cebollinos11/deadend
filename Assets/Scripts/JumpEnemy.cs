using UnityEngine;
using System.Collections;

public class JumpEnemy : Enemy {
    bool hasjumped;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (!hasjumped && transform.position.x - base.player.position.x < 0.5f) {

            /*
            Debug.Log("jump");
            hasjumped = true;

            hasAttacked = true;
            sHitEffect.enabled = true;
            anim.SetBool("Attack", true);
            rb.gravityScale = 0f;
            
            StartCoroutine(Drop());
             * */
            rb.AddForce(new Vector2(0, 1f) * 400f);
            hasjumped = true;
            BasicAttack();
        }
    }
}
