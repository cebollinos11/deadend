using UnityEngine;
using System.Collections;

public class Enemy : AbstractEnemy {
    protected Animator anim;
    protected bool hasAttacked;
    protected Rigidbody2D rb;

    bool hasDied;

    protected ShadowTrail shadowTrail;

    [HideInInspector] public bool hasKilledThePlayer;
    bool hasGivenPoints;
   
    [SerializeField]
    Sprite hitSprite;

    protected SpriteRenderer hitSpriteEffect;

    [SerializeField]
    protected float perfectMin = 0;
    [SerializeField]
    protected float perfectMax = 0.5f;

    [SerializeField]
    protected float moveSpeed = 1.0f;

    [SerializeField]
    protected float dashForce = 400;

    public override bool EvaluatePerformance() {
        float yDist = Player.transform.position.y - transform.position.y;
        if(yDist > perfectMin && yDist < perfectMax) {
            return true;
        }
        return false;
    }


    // Use this for initialization
    protected virtual void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        shadowTrail = gameObject.AddComponent<ShadowTrail>();

        //instantiate hitSpriteEffect
        hitSpriteEffect = new GameObject().AddComponent<SpriteRenderer>();

        hitSpriteEffect.sprite = Resources.Load<Sprite>("Sprites/hitEffect");
        hitSpriteEffect.transform.parent = transform;
        hitSpriteEffect.transform.localPosition = Vector3.left * 0.1f;
        hitSpriteEffect.enabled = false;
        hitSpriteEffect.material = Resources.Load("Materials/Flash") as Material;


    }

    protected IEnumerator Drop() {
        
        yield return new WaitForSeconds(0.3f);
        if (!hasKilledThePlayer) {
            DeathEvent();
            hasDied = true;
            anim.SetBool("Fall", true);
            rb.gravityScale = 1f;
            rb.drag = 0f;

        }
        shadowTrail.Stop();
        hitSpriteEffect.enabled = false;
    }



    protected void BasicAttack() {
        AttackEvent();

        AudioManager.PlayClip(AudioManager.Instance.swing);
        hitSpriteEffect.enabled = true;
        shadowTrail.Play();
        anim.SetBool("Attack", true);
        rb.gravityScale = 0f;
        rb.AddForce(Vector2.left * dashForce);
        hasAttacked = true;

        StartCoroutine(Drop());

    }
    // Update is called once per frame
    protected virtual void Update() {

        if (hasAttacked) {
            return;
        }

        transform.position += new Vector3(- moveSpeed * Time.deltaTime, 0f, 0f);

        if (transform.position.x - Player.transform.position.x < 0.4f) {
            BasicAttack();            
        }

    }

    void OnCollisionEnter2D(Collision2D col) {
        if (Player == null || hasDied )
            return;
        if (col.gameObject == Player.gameObject) {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            anim.SetBool("Attack", true);
            hasKilledThePlayer = true;
            Debug.Log(gameObject.name + " has killed the player with hasDied = "+hasDied.ToString());
        }



    }
}
