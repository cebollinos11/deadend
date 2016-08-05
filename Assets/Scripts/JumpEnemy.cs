using UnityEngine;
using System.Collections;

public class JumpEnemy : Enemy {
    bool hasJumped;
    [SerializeField]
    private float maxChargeForPerfect = 0.1f;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (hasAttacked) {
            return;
        }

        transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0f, 0f);

        if (!hasJumped && transform.position.x - Player.transform.position.x < 0.5f) {
            rb.AddForce(new Vector2(0, 1f) * 400f);
            hasJumped = true;
            BasicAttack();
        }
    }

    public override bool EvaluatePerformance() {
        float chargedTime = Mathf.Abs(Time.time - Player.ChargeStart);
        Debug.Log("Charged time " + chargedTime);
        if (Player.state == Hero.HeroState.chargejump && chargedTime < maxChargeForPerfect) {
            return true;
        }
        return false;
    }
}
