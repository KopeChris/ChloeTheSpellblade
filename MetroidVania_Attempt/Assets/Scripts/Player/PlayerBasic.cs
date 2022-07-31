using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using UnityEngine.SceneManagement;

using BayatGames.SaveGameFree;

public class PlayerBasic : MonoBehaviour
{
    public Animator animator;
    public HealthBar healthBar;
    public ManaBar manaBar;

    public int maxHealth = 100;
    public int currentHealth;

    public int mana=100;
    public int maxMana;
    public int manaCost;

    public int berries=2;
    public int maxBerries=2;

    public int playerCoin=0;

    public static float movementSpeed = 17;
    [SerializeField]
    private float groundCheckRadius;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float slopeCheckDistance;
    [SerializeField]
    private float maxSlopeAngle;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private PhysicsMaterial2D noFriction;
    [SerializeField]
    private PhysicsMaterial2D fullFriction;

    [SerializeField]
    private int rollForce;
    [SerializeField]
    private float rollIframes;
    [SerializeField]
    private float hurtIseconds;

    private float xInput;   // xinput is the horizMovement
    private float slopeDownAngle;
    private float slopeSideAngle;
    private float lastSlopeAngle;

    public static int facingDirection = 1;

    //states
    public static bool isRolling;
    public static bool canAction = true;    //if canAction but !canMo
                                            //ve then you can flip but not move used in crouch
    public static bool canMove;
    //public static bool canAction = true;
    public static bool isInvincible;
    public static bool actionInv;
    public static bool isDead;
    [SerializeField]
    public static bool cinematicState = false;

    public static bool comboAttack;

    private bool isGrounded;
    private bool isOnSlope;
    [HideInInspector]
    public bool isJumping;
    private bool canWalkOnSlope;
    private bool canJump;
    

    public static Vector2 newVelocity;
    private Vector2 newForce;
    private Vector2 capsuleColliderSize;

    private Vector2 slopeNormalPerp;

    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public CapsuleCollider2D cc;


    

    //bools for animation states to activate
    //removed that replaced with normal input, did this for buffering to work public static bool chloeAttack1;

    //coyote time for jump
    public float coyoteTimeCoolDown= 0.08f;
    //buffering
    [SerializeField]
    private float bufferTime = 0.1f;
    private float jumpBufferCounter;
    private float attackBufferCounter;
    private float rollBufferCounter;
    private float healBufferCounter;
    private float castBufferCounter;


    public static float positionX;
    public static float positionY;


    SpriteRenderer sprite;
    public IEnumerator Flash()
    {
        for (float i = 0; i < hurtIseconds; i += 0.2f)
        {
            sprite.color = new Color(1, 1, 1, 0.4f);
            yield return new WaitForSeconds(0.1f);
            sprite.color = Color.white; 
            yield return new WaitForSeconds(0.1f);
        }
    }

    //to jump off platforms
    bool ignorePlatformsCoroutineIsRunning;
    int playerLayer, platformsLayer, enemiesLayer, enemyCollisionBlocker, playerCollisionBlocker, enemyProjectile;
    public GameObject platformCollider;
    IEnumerator IgnorePlatforms()
    {
        ignorePlatformsCoroutineIsRunning = true;
        platformCollider.SetActive(false);
        yield return new WaitForSeconds(0.24f);
        platformCollider.SetActive(true);
        ignorePlatformsCoroutineIsRunning = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerLayer = LayerMask.NameToLayer("Player");
        platformsLayer = LayerMask.NameToLayer("Platforms");
        enemiesLayer = LayerMask.NameToLayer("Enemies");
        enemyCollisionBlocker = LayerMask.NameToLayer("EnemyCollisionBlocker");
        playerCollisionBlocker = LayerMask.NameToLayer("PlayerCollisionBlocker");
        enemyProjectile = LayerMask.NameToLayer("ProjectileEnemies");

        capsuleColliderSize = cc.size;
        notInvincible();

        //currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        manaBar.SetMaxMana(maxMana);

        
    }

    void Update()
    {
        
        
        Input();
        /*
        if(isInvincible && actionInv)
        { actionInv = false;  InvincibleFunction(true);}
        if(!isInvincible && actionInv) 
        { actionInv = false;  InvincibleFunction(false);}
        */
        animator.SetFloat("speedParameter", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yParameter", rb.velocity.y);
        if (isGrounded) { animator.SetBool("isGrounded", true); }
        else { animator.SetBool("isGrounded", false); }

        healthBar.SetHealth(currentHealth);
        manaBar.SetMana(mana);

        positionX = transform.position.x;
        positionY = transform.position.y;
    }
    private void FixedUpdate()
    {
        CheckGround();
        SlopeCheck();
        if (cinematicState) { canAction = false; newVelocity.Set(0.0f, rb.velocity.y); rb.velocity = newVelocity; } //change to for not to slow down y velocity when praying
        else {if(canMove) ApplyMovement(); }

        //friction

        
        newForce.Set(30, 0.0f);
        if (rb.velocity.x > 0.01) { rb.AddForce(-newForce, ForceMode2D.Force); }
        if (rb.velocity.x < -0.01) { rb.AddForce(newForce, ForceMode2D.Force); }

        if (Mathf.Abs(rb.velocity.x )< 0.01 && xInput == 0 ) {
            newVelocity.Set(0.0f, rb.velocity.y);      
            rb.velocity = newVelocity;
        }
        

        //if (Mathf.Abs(rb.velocity.y) >150) { TakeDamage(maxHealth,0); }

        if (rb.velocity.y > -2)             // to make jump feel better and less floaty
            rb.gravityScale = 10;
        else
            rb.gravityScale = 12;
    }
    private void Input() 
    {
        xInput = UnityEngine.Input.GetAxisRaw("Horizontal");
        
        if ((xInput >0 && facingDirection == -1 && rb.velocity.x>=0 ) || (xInput < 0 && facingDirection == 1 && rb.velocity.x <= 0))
        {
            Flip();
        }
        
        if (UnityEngine.Input.GetButtonDown("Jump") ) 
        {
            jumpBufferCounter = bufferTime; 
        }
        else { jumpBufferCounter -= Time.deltaTime; }
        if (jumpBufferCounter >0 && !UnityEngine.Input.GetKey(KeyCode.S) && canJump && canAction && !isRolling && !ignorePlatformsCoroutineIsRunning)
        {
            Jump();
            jumpBufferCounter=0f;
        }
        else if (UnityEngine.Input.GetButtonDown("Jump") && UnityEngine.Input.GetKey(KeyCode.S) && !ignorePlatformsCoroutineIsRunning)
        {
            StartCoroutine("IgnorePlatforms");
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.O))
        {
            rollBufferCounter = bufferTime / 2;
        }
        else { rollBufferCounter -= Time.deltaTime; }
        if (rollBufferCounter > 0 && canAction && !isRolling && canJump)
        {
            Roll();
            rollBufferCounter=0f;
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.I))
        {
            attackBufferCounter = bufferTime;
        }
        else { attackBufferCounter -= Time.deltaTime; }
        if (attackBufferCounter > 0 && canAction && !isRolling && canJump &&!comboAttack)
        {
            Attack1();
            attackBufferCounter = 0;
        }

        if (UnityEngine.Input.GetKey(KeyCode.S) == true && canAction && isGrounded && !isRolling && rb.velocity.y<3 && !ignorePlatformsCoroutineIsRunning)    //interact button the rb.velocity.y<3 was to activate jump animation
        {
            canMove = false;
            animator.SetBool("stand", false);
            animator.Play("Chloe Crouch");
        }
        if (UnityEngine.Input.GetKey(KeyCode.S) == false) {animator.SetBool("stand",true);}
        if (UnityEngine.Input.GetKeyDown(KeyCode.I) && isJumping)   //jump Attack
        {
            canAction = false;
            //canMove= false;       //maybe to nerf the player but now keep it as is
            animator.Play("Chloe JumpAttack");
            //AudioManager.instance.PlaySwing();

        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.K)) {healBufferCounter = bufferTime;}
        else { healBufferCounter -= Time.deltaTime; }
        if (healBufferCounter>0 && canAction && isGrounded && berries > 0)     //heal &&berries>0
        {
            canAction=false;
            canMove = false;
            animator.Play("Chloe Heal");
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.J)) {castBufferCounter = bufferTime;}
        else { castBufferCounter -= Time.deltaTime; }
        if (castBufferCounter>0 && canAction && isGrounded && manaCost <= mana)     //Cast && manaCost<=mana
        {
            canAction=false;
            canMove = false;
            animator.Play("Cast");

            mana -= manaCost;
        }

    }
    

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (rb.velocity.y <= 0.01f && isGrounded)
        {
            isJumping = false;
        }

        if (isGrounded && !isJumping && slopeDownAngle <= maxSlopeAngle)
        {
            canJump = true;
        }
        else { Invoke("canNotJump", coyoteTimeCoolDown); }

    }
    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - (Vector3)(new Vector2(0.0f, capsuleColliderSize.y / 2));

        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }
    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, whatIsGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, whatIsGround);

        if (slopeHitFront)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);

        }
        else if (slopeHitBack)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }

    }
    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, whatIsGround);

        if (hit)
        {

            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != lastSlopeAngle)
            {
                isOnSlope = true;
            }

            lastSlopeAngle = slopeDownAngle;

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);

        }

        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle)
        {
            canWalkOnSlope = false;
        }
        else
        {
            canWalkOnSlope = true;
        }

        if (isOnSlope && canWalkOnSlope && xInput == 0.0f && !isRolling)
        {
            rb.sharedMaterial = fullFriction;
            newVelocity.Set(0.0f, rb.velocity.y);       //used to be (0,0)
            rb.velocity = newVelocity;
        }
        else
        {
            rb.sharedMaterial = noFriction;
        }
    }

    private void ApplyMovement()
    {
        if (isGrounded && !isOnSlope && !isJumping && !isRolling ) //if on straigth ground, not on slope
        {
            newVelocity.Set(movementSpeed * xInput * 50 * Time.fixedDeltaTime, rb.velocity.y); //y = 0 in the original slope code
            rb.velocity = newVelocity;
        }
        else if (isGrounded && isOnSlope && canWalkOnSlope && !isJumping && !isRolling) //If on slope
        {
            newVelocity.Set(movementSpeed * slopeNormalPerp.x * -xInput * 50 * Time.fixedDeltaTime, movementSpeed * slopeNormalPerp.y * -xInput * 50 * Time.fixedDeltaTime);
            rb.velocity = newVelocity;
        }
        
        else if (!isGrounded && !isRolling) //If in air
        {
            //newVelocity.Set(rb.velocity.x, rb.velocity.y);
            //rb.velocity = newVelocity;
            
            newForce.Set(170, 0.0f);
            rb.AddForce(newForce * xInput, ForceMode2D.Force);                                  // helps to move on air
            if (rb.velocity.x > movementSpeed-2) { rb.AddForce(-newForce, ForceMode2D.Force); }
            if (rb.velocity.x < -movementSpeed+2) { rb.AddForce(newForce, ForceMode2D.Force); }
            /*
            if (rb.velocity.x > 0.01) { rb.AddForce(-newForce / 6, ForceMode2D.Force); }    // that was to tend to be 0 x speed but now its permanent and acts like tension
            if (rb.velocity.x < 0.01) { rb.AddForce(newForce / 6, ForceMode2D.Force); }
            */
        }


    }
    
    private void Jump()
    {
        animator.SetTrigger("Jump");
        animator.Play("Chloe Jump");
        canJump = false;
        isJumping = true;
        newVelocity.Set(0.0f, 0.0f);
        rb.velocity = newVelocity;
        //newForce.Set(xInput * (jumpForce/4), jumpForce);
        //rb.AddForce(newForce, ForceMode2D.Impulse);
        Push(xInput*jumpForce/4, jumpForce);
    }

    private void Flip()
    {
        if(!isRolling && canAction)
        { 
        facingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
       // Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(AttackSphere.position, attack1Range);

    }
    public void Push(float pushForceX, float pushForceY)
    {
        newVelocity.Set(0.0f, 0.0f);
        rb.velocity = newVelocity;
        newForce.Set(pushForceX, pushForceY);

        rb.AddForce(newForce , ForceMode2D.Impulse);
    }

    private void Roll()
    {
        if (!isJumping  && isGrounded)
        {
            Push(rollForce * facingDirection,0);
            
            animator.SetTrigger("Roll");
            canAction = false;
            isRolling = true;
            InvincibleFunction(true);
            Invoke("notInvincible", rollIframes/60);

        }
    }    
    public void GetCoin(int coin)
    {
        playerCoin += coin;
    }
    public void LoseCoin(int coin)
    {
        playerCoin -= coin;
    }

    public void TakeDamage(int damage, float pushForce)
    {
        if(!isInvincible && !isDead)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            
            canAction = false;
            canMove = false;

            // AudioManager.instance.PlayPlayerHurt();
            CameraShaker.Instance.ShakeOnce((float)damage*maxHealth/2000f, 4f,0.1f,1f);
            StopTime(0.05f);

            if (currentHealth >= 0)
            {
                animator.SetTrigger("Stun");
                Push(2*pushForce, 0);
            }

            if (currentHealth <= 0)
            {
                isDead = true;
                animator.SetBool("isDead", true);
                LoseCoin(playerCoin);
                //this.enabled = false;
            }

            StartCoroutine(Flash());
            InvincibleFunction(true);
            Invoke("notInvincible", hurtIseconds);
        }

    }
   
    public void GetHardStunned()
    {

    }

    public void Heal(int heal, int manaRestored)
    {
            currentHealth += heal;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            mana += manaRestored;
            mana = Mathf.Clamp(mana, 0, maxMana);
            berries--;
    }

    

    void Attack1()
    {
        //AudioManager.instance.PlaySwing();
        canAction = false;
        canMove = false;
        animator.Play("Chloe Atk1");
        newVelocity.Set(0.0f, 0.0f);
        rb.velocity = newVelocity;

        //Push(3,0, facingDirection);
    }
    public void SetVelocity(float x, float y)
        {
        newVelocity.Set(x, y);
        rb.velocity = newVelocity;
    }
    

    public void InvincibleFunction(bool invincible)        //playerLayer, platformsLayer, enemiesLayer, collisionBlockerLayer
    {
        Physics2D.IgnoreLayerCollision(playerLayer, enemiesLayer, invincible);
        Physics2D.IgnoreLayerCollision(playerLayer, enemyCollisionBlocker, invincible);
        Physics2D.IgnoreLayerCollision(playerLayer, enemyProjectile, invincible);
        Physics2D.IgnoreLayerCollision(playerCollisionBlocker, enemiesLayer, invincible);
        Physics2D.IgnoreLayerCollision(playerCollisionBlocker, enemyCollisionBlocker, invincible);
        Physics2D.IgnoreLayerCollision(playerCollisionBlocker, enemyProjectile, invincible);
        isInvincible = invincible;

       // sprite.color = new Color(1, 1, 0, 1);
    }
    public void notInvincible()
    {
        InvincibleFunction(false);
       // sprite.color = Color.white;

    }
    void canNotJump()
    {
        canJump = false;
    }

    

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        currentHealth = data.currentHealth;
        maxHealth = data.maxHealth;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

    }

    #region //code monkey save system       left unfinished
    public float[] position;
    int coin;
    public static string sceneIndexString;
    public int maxHealthSS;
    public int currentHealthSS;

    public int manaSS;
    public int maxManaSS;

    public int berriesSS;
    public int maxBerriesSS;

    public void Save()
    {
        position[0] = transform.position.x;
        position[1] = transform.position.y;
        position[2] = transform.position.z;
        coin = playerCoin;
        maxHealthSS = maxHealth;
        currentHealthSS = currentHealth;
        manaSS = mana;
        maxManaSS = maxMana;
        berriesSS = berries;
        maxBerriesSS = maxBerries;

        PlayerPrefs.SetInt(sceneIndexString, SceneManager.GetActiveScene().buildIndex);

        SaveSystem.SavePlayer(this);

    }
    public void Load()
    {
        GetCoin(coin);
    }

    #endregion

    #region SaveGameFree

    
    void SaveGameFree()
    {
        SaveGame.Save<int>("currentHealth", currentHealth);
        SaveGame.Save<int>("maxHealth", maxHealth);
        SaveGame.Save<int>("mana", mana);
        SaveGame.Save<int>("maxMana", maxMana);
        SaveGame.Save<int>("coin", coin);
        SaveGame.Save<int>("berries", berries);
        SaveGame.Save<int>("maxBerries", maxBerries);

    }


    #endregion


    bool timeWaiting;
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
}
