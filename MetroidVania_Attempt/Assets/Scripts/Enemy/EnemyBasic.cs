using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class EnemyBasic : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Animator animator;
    public HealthBar healthBar;
    GameObject Player;
    public int enemyCoin;
    public int maxHealth = 100;
    public int currentHealth;

    public float speed=7;
    //to create the push function
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Vector2 newVelocity;
    [HideInInspector]
    public Vector2 newForce;

    public LayerMask targetLayer;
    //public UnityEvent<GameObject> OnPlayerDetected;

    [Header("Attacks")]
    public bool canAttack;
    public bool AttackPlayer;
    public float cooldown = 2.0f;
    public float attack1Range = 1;
    public float attack2Range = 1;
    public float attack3Range = 1;
    [Range(0.0f, 1f)]
    public float attack1Miss;
    [Range(0.0f, 1f)]
    public float attack2Chance;
    [Range(0.0f, 1f)]
    public float attack3Chance;

    float randValue;
    float randValue2;
    float randCooldown;
    private float nextAttackTime = 0f;

    [Header("Gizmos Parameters")]
    public bool showDetectionGizmos = true;
    public bool showAttackGizmos = true;
    public float detectionRadius;
    public float followRadius;
    public Transform DetectionPositionSphere;
    public Transform Attack1;
    public Transform Attack2;
    public Transform Attack3;
    
    [SerializeField]
    public bool PlayerDetected { get; internal set; }
    public bool PlayerFollowed { get; internal set; }
    public bool PlayerInMeleeRange { get; internal set; } //is equal to all playerrangss together
    public bool PlayerInRange1 { get; internal set; }
    public bool PlayerInRange2 { get; internal set; }
    public bool PlayerInRange3 { get; internal set; }


    public bool facingRight = false;
    public int  facingDirection;
    public int  playerDirectionX;
    public int  playerDirectionY;

    public bool CanGetStunned = false;
    bool isDead;

    bool timeWaiting;

    public AudioClip hurtSound;
    public IEnumerator FlashWhite()
    {
        sprite.color = new Color(1, 0.5f, 0.5f, 1);
        yield return new WaitForSeconds(0.1f);
        sprite.color = new Color(1, 0.8f, 0.8f, 1);
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        animator.SetBool("isFollowing", false);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        Player = GameObject.FindGameObjectWithTag("Player");

        InvokeRepeating("RandValue", 0, 2);
    }

    void Update()
    {
        if (rb.velocity.x > 0) { facingRight = true; facingDirection = 1; } else { facingRight = false; facingDirection = -1; }     //facing depends on x movement direction
        if (Player.transform.position.x > rb.transform.position.x) { playerDirectionX = 1; } else { playerDirectionX = -1; }
        if (Player.transform.position.y > rb.transform.position.y) { playerDirectionY = 1; } else { playerDirectionY = -1; }

        if (AttackPlayer && !isDead)
        {

            //In attack Range Player
            var collider = Physics2D.OverlapCircle(Attack1.position, attack1Range, targetLayer);
            PlayerInRange1 = collider != null;

            if (PlayerInRange1 && Time.time >= nextAttackTime && canAttack && attack1Miss < randValue)
            {
                animator.Play("Attack1");
                nextAttackTime = Time.time + randCooldown;
                canAttack = false;
            }

            collider = Physics2D.OverlapCircle(Attack2.position, attack2Range, targetLayer);
            PlayerInRange2 = collider != null;
            if (PlayerInRange2 && Time.time >= nextAttackTime && canAttack && attack2Chance > randValue)
            {
                animator.Play("Attack2");
                nextAttackTime = Time.time + randCooldown;
                canAttack = false;
            }
            collider = Physics2D.OverlapCircle(Attack3.position, attack3Range, targetLayer);
            PlayerInRange3 = collider != null;
            if (PlayerInRange3 && Time.time >= nextAttackTime && canAttack && attack3Chance > randValue)
            {
                animator.Play("Attack3");
                nextAttackTime = Time.time + randCooldown;
                canAttack = false;
            }
        }

        PlayerInMeleeRange = PlayerInRange1 || PlayerInRange2;  //if player in melee range following isnt activated as to not run while being right in front

        //detection and follow
        var detection = Physics2D.OverlapCircle(DetectionPositionSphere.position, detectionRadius, targetLayer);
        PlayerDetected = detection != null;
        if (PlayerDetected && !PlayerInMeleeRange || currentHealth < maxHealth) //if !playerdetected then it doesnt follow, if it is detected but outside of attack range (follows), if inside attack range chance to follow
        {
            animator.SetBool("isFollowing", true);
            AttackPlayer = true;
        }
        //stop follow
        var follow = Physics2D.OverlapCircle(DetectionPositionSphere.position, followRadius, targetLayer);
        PlayerFollowed = follow != null;
        if (PlayerFollowed == false)
        {
            animator.SetBool("isFollowing", false);
            AttackPlayer = false;

        }

        if(isDead)
        {
            Color opaqueRed = new Color(1, 0, 0, 0f);
            sprite.color = Color.Lerp(sprite.color, opaqueRed, 0.005f);
        }    
            

    }

    void FixedUpdate()
    {
        //friction
        newForce.Set(50, 0.0f);
        if (rb.velocity.x > 0.001) { rb.AddForce(-newForce, ForceMode2D.Force); }
        if (rb.velocity.x < -0.001) { rb.AddForce(newForce, ForceMode2D.Force); }

        if(Mathf.Abs(rb.velocity.x) <0.1)
        {
            newVelocity.Set(0, rb.velocity.y); //y = 0 in the original slope code
            rb.velocity = newVelocity;
        }
        
    }
    void Death()
    {
        //Physics2D.IgnoreCollision(Player.GetComponent<CapsuleCollider2D>(), GetComponent<CapsuleCollider2D>(), true);
        //Physics2D.IgnoreCollision((CapsuleCollider2D)Player.GetComponentInChildren(typeof(CapsuleCollider2D)), GetComponent<CapsuleCollider2D>(), true);

        //Player.GetComponent<PlayerBasic>().GetCoin(enemyCoin);

        //destroy all children except first
        /*
        for (var i = rb.transform.childCount - 1; i >= 1; i--)
        {
            Object.Destroy(rb.transform.GetChild(i).gameObject,5f);
        }
        */
        //Destroy(GetComponent<EnemyBasic>(),5f);

        Object.Destroy(rb.transform.GetChild(rb.transform.childCount-1).gameObject, 1f);    //destroys collision block
        Destroy(GetComponent<CapsuleCollider2D>(),1f);
        Destroy(this.gameObject, 5f);
        
    }
   
    //Gizmos
    private void OnDrawGizmosSelected()
    {
        if (showDetectionGizmos)
        {
            Gizmos.color = new Color(1, 1, 0, 0.4f);
            Gizmos.DrawWireSphere(DetectionPositionSphere.position, detectionRadius);
            Gizmos.color = new Color(0, 1, 0, 0.4f);
            Gizmos.DrawWireSphere(DetectionPositionSphere.position, followRadius);

        }
        AttackGizmos();

    }
    private void OnDrawGizmos()
    {
        if (showAttackGizmos)
        {
            AttackGizmos();
        }
    }
    private void AttackGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.7f);
        Gizmos.DrawWireSphere(Attack1.position, attack1Range);
        Gizmos.color = new Color(1, 0.5f, 0, 0.7f);
        Gizmos.DrawWireSphere(Attack2.position, attack2Range);
        Gizmos.color = new Color(1, 0, 0.5f, 0.7f);
        Gizmos.DrawWireSphere(Attack3.position, attack3Range);
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)  
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            healthBar.SetHealth(currentHealth);

            //AudioManager.instance.PlayHurt();
            CameraShake.shake = true;

            //StopTime(0.1f);                                                 // hitstop stop time when hit

            if (currentHealth > 0) { StartCoroutine(FlashWhite());}      

            if (CanGetStunned == true && currentHealth >= 0)
            {
                animator.Play("Hurt");
                Push(15 * (-playerDirectionX));
            }

            if (currentHealth <= 0)     // ON DEATH
            {
                Player.GetComponent<PlayerBasic>().GetCoin(enemyCoin);
                isDead = true;
                animator.Play("Death");
                Death();
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

    public void StopTime(float duration)
    {
        if (timeWaiting) { return; }
        Time.timeScale = 0.0f;
        StartCoroutine(ContinueTime(duration));
    }
    IEnumerator ContinueTime(float duration)
    {
        timeWaiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        timeWaiting = false;
    }

    void RandValue()
    {
        randValue = Random.value;
        randValue2 = Random.Range(0.5f, 1.5f);
        randCooldown = randValue2 * cooldown;
    }
}
