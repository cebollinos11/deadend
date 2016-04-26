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
            Debug.Log("jump");
            hasjumped = true;

            hasAttacked = true;
            anim.SetBool("Attack", true);
            rb.gravityScale = 0f;
            rb.AddForce(new Vector2(-1,1f) * 400f);
            StartCoroutine(Drop());
        }
    }
}
