using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class EnemyAiMelee : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public Animator animator;
    //to create the push function
    private Rigidbody2D rb;
    private Vector2 newVelocity;
    private Vector2 newForce;

    public LayerMask targetLayer;
    //public UnityEvent<GameObject> OnPlayerDetected;

    [Header("Attacks")]
    public float attack1Range = 1;
    public int attack1Damage = 30;
    public float attack1Cooldown = 3.0f;
    public float attack2Range = 1;
    public int attack2Damage = 30;
    public float attack3Range = 1;
    public int attack3Damage = 30;

    private float nextAttackTime = 0f;

        [Header("Gizmos Parameters")]
    public Color DetectionColor;
    public Color FollowingColor;
    public bool showDetectionGizmos = true;
    public float detectionRadius;
    public float followRadius;
    public Transform DetectionPositionSphere;
    public Transform Attack1;
    public Transform Attack2;
    public Transform Attack3;
    public Transform Player;

    public bool PlayerInRange { get; internal set; }
    public bool PlayerDetected { get; internal set; }
    public bool PlayerFollowed { get; internal set; }

    public bool facingRight = false;
    public bool CanGetStunned = false;
    public static int pushDirection;
    public static bool attack1Hit = false;
    bool isDead;

        void Start()
    {
        animator.SetBool("isFollowing", false);
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //In attack Range Player
        var collider = Physics2D.OverlapCircle(Attack1.position, attack1Range, targetLayer);
        PlayerInRange = collider != null;
        if (PlayerInRange && Time.time >= nextAttackTime)
        {
            //OnPlayerDetected.Invoke(collider.gameObject);
            //Debug.Log("PlayerDetected");
            Attack();
            nextAttackTime = Time.time + attack1Cooldown;
        }

        if (PlayerInRange)
        {
            animator.SetBool("Attack2", true);
        }
        else { animator.SetBool("Attack2", false); }

        //detection
        var detection = Physics2D.OverlapCircle(DetectionPositionSphere.position, detectionRadius, targetLayer);
        PlayerDetected = detection != null;
        if (PlayerDetected)
        {
            // OnPlayerDetected.Invoke(detection.gameObject);
            animator.SetBool("isFollowing", true);

                
        }
        
        var follow = Physics2D.OverlapCircle(DetectionPositionSphere.position, followRadius, targetLayer);
        PlayerFollowed = follow != null;
        if (PlayerFollowed == false)
        {
            animator.SetBool("isFollowing", false);

        }

        if(attack1Hit)
        {
        Attack1Hit();
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
    }
    private void OnDrawGizmos()
    {
        if (showDetectionGizmos)
        {
            Gizmos.color = DetectionColor;
            Gizmos.DrawWireSphere(DetectionPositionSphere.position, detectionRadius);
        }

        Gizmos.color = FollowingColor;
        Gizmos.DrawWireSphere(DetectionPositionSphere.position, followRadius);
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(Attack1.position, attack1Range);
        Gizmos.color = new Color(1, 0.66f, 0, 0.5f);
        Gizmos.DrawWireSphere(Attack2.position, attack2Range);
        Gizmos.color = new Color(1, 0.33f, 0, 0.5f);
        Gizmos.DrawWireSphere(Attack3.position, attack3Range);

    }
    void Attack()
    {
        animator.SetBool("Attack", true);

            
    }
    public void Attack1Hit()
    {
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Attack1.position, attack1Range, targetLayer);
    //damage enemies effect of hit
    foreach (Collider2D enemy in hitEnemies)
    {
        enemy.GetComponent<PlayerBasic>().TakeDamage(attack1Damage,5, pushDirection);
    }
}


    public void TakeDamage(int damage)
    {
        if (!isDead)  
        {
            
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (CanGetStunned == true && currentHealth >= 0)
            {
                animator.Play("Hurt");
                Push(15*(-pushDirection));
            }
            if (currentHealth <= 0)
            {
                isDead = true;
                animator.SetBool("isDead", true);
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


}
