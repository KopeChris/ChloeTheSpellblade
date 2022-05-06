using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class EnemyBasic : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Animator animator;
    public HealthBar healthBar;
    public int enemyCoin;
    public int maxHealth = 100;
    public int currentHealth;

    //to create the push function
    private Rigidbody2D rb;
    private Vector2 newVelocity;
    private Vector2 newForce;

    public LayerMask targetLayer;
    //public UnityEvent<GameObject> OnPlayerDetected;

    [Header("Attacks")]
    public static bool canAttack;
    public float attack1Cooldown = 3.0f;
    public float attack1Range = 1;
    public float attack2Range = 1;
    public float attack3Range = 1;
    public float attack1Miss;
    public float attack2Chance;
    public float attack3Chance;

    float randValue;
    private float nextAttackTime = 0f;

    [Header("Gizmos Parameters")]
    public bool showDetectionGizmos = true;
    public float detectionRadius;
    public float followRadius;
    public Transform DetectionPositionSphere;
    public Transform Attack1;
    public Transform Attack2;
    public Transform Attack3;
    public Transform Player;

    public bool PlayerDetected { get; internal set; }
    public bool PlayerFollowed { get; internal set; }
    public bool PlayerInRange { get; internal set; } //is equal to all playerrangss together
    public bool PlayerInRange1 { get; internal set; }
    public bool PlayerInRange2 { get; internal set; }
    public bool PlayerInRange3 { get; internal set; }

    public bool facingRight = false;
    public bool CanGetStunned = false;
    public static int pushDirection;
    public static bool attack1Hit = false;
    bool isDead;



    public IEnumerator FlashWhite()
    {
        sprite.color = new Color(1, 0.5f, 0.5f, 1);
        yield return new WaitForSeconds(0.1f);
        sprite.color = new Color(1, 0.8f, 0.8f, 1);
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

        void Start()
    {
        animator.SetBool("isFollowing", false);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        rb = GetComponent<Rigidbody2D>();
        sprite=GetComponent<SpriteRenderer>();
        InvokeRepeating("RandValue", 0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
        //In attack Range Player
        var collider = Physics2D.OverlapCircle(Attack1.position, attack1Range, targetLayer);
        PlayerInRange1 = collider != null;

        if (PlayerInRange1 && Time.time >= nextAttackTime && canAttack && attack1Miss < randValue)
        {
            animator.Play("Attack1");
            animator.SetBool("isFollowing", false);
            nextAttackTime = Time.time + attack1Cooldown;
            canAttack = false;
        }

        collider = Physics2D.OverlapCircle(Attack2.position, attack2Range, targetLayer);
        PlayerInRange2 = collider != null;
        if (PlayerInRange2 && Time.time >= nextAttackTime && canAttack && attack2Chance > randValue)
        {
            animator.Play("Attack2");
            nextAttackTime = Time.time + attack1Cooldown;
            animator.SetBool("isFollowing", false);
            canAttack = false;
        }
        collider = Physics2D.OverlapCircle(Attack3.position, attack2Range, targetLayer);
        PlayerInRange3 = collider != null;
        if (PlayerInRange3 && Time.time >= nextAttackTime && canAttack && attack3Chance > randValue)
        {
            animator.Play("Attack3");
            nextAttackTime = Time.time + attack1Cooldown;
            animator.SetBool("isFollowing", false);
            canAttack = false;
        }

        PlayerInRange = PlayerInRange1 || PlayerInRange2 || PlayerInRange3;

        //detection and follow
        var detection = Physics2D.OverlapCircle(DetectionPositionSphere.position, detectionRadius, targetLayer);
        PlayerDetected = detection != null;
        if (PlayerDetected && !PlayerInRange || (randValue > 0.5f)) //if !playerdetected then it doesnt follow, if it is detected but outside of attack range (follows), if inside attack range chance to follow

        {
            // OnPlayerDetected.Invoke(detection.gameObject);
            animator.SetBool("isFollowing", true);
        }
        //stop follow
        var follow = Physics2D.OverlapCircle(DetectionPositionSphere.position, followRadius, targetLayer);
        PlayerFollowed = follow != null;
        if (PlayerFollowed == false)
        {
            animator.SetBool("isFollowing", false);

        }

        
        if (Player.position.x < rb.transform.position.x)
        {
            pushDirection = -1;
            
        }
        else { pushDirection = 1; }

    }
    void FixedUpdate()
    {
        

    }
    void OnDisable()
    {
        //Physics2D.IgnoreCollision(Player.GetComponent<CapsuleCollider2D>(), GetComponent<CapsuleCollider2D>(), true);
        //Physics2D.IgnoreCollision((CapsuleCollider2D)Player.GetComponentInChildren(typeof(CapsuleCollider2D)), GetComponent<CapsuleCollider2D>(), true);
        Destroy(GetComponent<CapsuleCollider2D>());
        Destroy(GetComponent<EnemyBasic>());

        //Player.GetComponent<PlayerBasic>().GetCoin(enemyCoin);

        //destroy all children except first
        for (var i = rb.transform.childCount - 1; i >= 1; i--)
        {
            Object.Destroy(rb.transform.GetChild(i).gameObject);
        }

    }
   
    
    private void OnDrawGizmosSelected()
    {
        if (showDetectionGizmos)
        {
            Gizmos.color = new Color(1, 1, 0, 0.5f);
            Gizmos.DrawWireSphere(DetectionPositionSphere.position, detectionRadius);
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawWireSphere(DetectionPositionSphere.position, followRadius);
        }

        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(Attack1.position, attack1Range);
        Gizmos.color = new Color(1, 0.5f, 0, 0.5f);
        Gizmos.DrawWireSphere(Attack2.position, attack2Range);
        Gizmos.color = new Color(1, 1, 0, 0.5f);
        Gizmos.DrawWireSphere(Attack3.position, attack3Range);

    }
    
    public void TakeDamage(int damage)
    {
        if (!isDead)  
        {

            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            healthBar.SetHealth(currentHealth);
            if (currentHealth > 0) { StartCoroutine(FlashWhite()); }

            if (CanGetStunned == true && currentHealth >= 0)
            {
                animator.Play("Hurt");
                Push(15*(-pushDirection));
            }
            if (currentHealth <= 0)
            {
                isDead = true;
                animator.Play("Death");
                this.enabled = false;
                
            }
        }
    }

    public void Push(int pushForce)
    {
        newVelocity.Set(0.0f, 0.0f);
        rb.velocity = newVelocity;
        newForce.Set(pushForce, 0.0f);
        rb.AddForce(newForce, ForceMode2D.Impulse);
    }
    void RandValue()
    {
        randValue = Random.value;
    }


}