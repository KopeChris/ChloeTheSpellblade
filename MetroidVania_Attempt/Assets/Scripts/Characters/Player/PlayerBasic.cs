using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasic : MonoBehaviour
{
    public Animator animator;
    public HealthBar healthBar;

    public int maxHealth = 100;
    public  int currentHealth;
    public int playerCoin=0;

    [SerializeField]
    private float movementSpeed;
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
    private float iFrames;

    private float xInput;   // xinput is the horizMovement
    private float slopeDownAngle;
    private float slopeSideAngle;
    private float lastSlopeAngle;

    private int facingDirection = 1;

    private bool isGrounded;
    private bool isOnSlope;
    private bool isJumping;
    private bool canWalkOnSlope;
    private bool canJump;

    private Vector2 newVelocity;
    private Vector2 newForce;
    private Vector2 capsuleColliderSize;

    private Vector2 slopeNormalPerp;

    private Rigidbody2D rb;
    CapsuleCollider2D cc;

    //stuff to do with the attack
    public LayerMask targetLayer;
    public Transform AttackSphere;
    public float attack1Range;
    public int attack1Damage;

    //states
    public static bool isRolling;
    public static bool canMove = true;
    public static bool isInvincible;
    public static bool actionInv;
    public static bool isDead;
    public static bool cinematicState = false;

    //bools for animation states to activate
    //removed that replaced with normal input, did this for buffering to work public static bool chloeAttack1;
    public static bool chloeAttack1Hit;

    //coyote time for jump
    public float coyoteTimeCoolDown= 0.08f;
    //buffering
    [SerializeField]
    private float bufferTime = 0.1f;
    private float jumpBufferCounter;
    private float attackBufferCounter;
    private float rollBufferCounter;

    SpriteRenderer sprite;

    public IEnumerator FlashWhite()
    {
        sprite.color = new Color(1, 0.5f, 0.5f, 1);
        yield return new WaitForSeconds(0.083f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        sprite.color = new Color(1, 1, 0.5f, 0.2f);
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        sprite.color = new Color(1, 1, 1, 0.2f);
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
        sprite.color = new Color(1, 1, 1, 0.2f);
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        sprite.color = new Color(1, 1, 1, 0.2f);
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        sprite.color = new Color(1, 1, 1, 0.2f);
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        capsuleColliderSize = cc.size;
        InvincibleFunction(false);

        //transform.position = startingPosition.initialValue;
    }

    void Update()
    {
        CheckInput();
        
        if(isInvincible && actionInv)
        { actionInv = false;  InvincibleFunction(true);}
        if(!isInvincible && actionInv) 
        { actionInv = false;  InvincibleFunction(false);}

        
    }
    private void FixedUpdate()
    {
        CheckGround();
        SlopeCheck();
        if (cinematicState) { newVelocity.Set(0.0f, 0.0f); rb.velocity = newVelocity; }
        else{ ApplyMovement(); }

        animator.SetFloat("speedParameter", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yParameter", rb.velocity.y);
        if(isGrounded) { animator.SetBool("isGrounded", true);}
        else { animator.SetBool("isGrounded", false); }
        if (xInput == 0 && isGrounded && canMove &&!isRolling) {
            newVelocity.Set(movementSpeed * xInput,rb.velocity.y);
            rb.velocity = newVelocity;
        }
    }
    
    private void CheckInput() 
    {
        xInput = Input.GetAxisRaw("Horizontal");
        
        if ((xInput >0 && facingDirection == -1)|| (xInput < 0 && facingDirection == 1))
        {
            Flip();
        }
        

        if (Input.GetButtonDown("Jump") ) 
        { 
            jumpBufferCounter = bufferTime; 
        }
        else { jumpBufferCounter -= Time.deltaTime; }
        if (jumpBufferCounter >0 && canJump && canMove && !isRolling)
        {
            Jump();
            jumpBufferCounter=0f;
        }
        
        if(Input.GetKeyDown(KeyCode.O))
        {
            rollBufferCounter = bufferTime/2;
        }
        else { rollBufferCounter -= Time.deltaTime; }
        if (rollBufferCounter > 0 && canMove && !isRolling && canJump)
        {
            Roll();
            rollBufferCounter=0f;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            attackBufferCounter = bufferTime;
        }
        else { attackBufferCounter -= Time.deltaTime; }
        if (attackBufferCounter > 0 && canMove && !isRolling && canJump)
        {
            Attack1();
            attackBufferCounter = 0;
        }

        if (Input.GetKeyDown(KeyCode.W))    //interact button
        {
            
        }

    }
    

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (rb.velocity.y <= 0.0f)
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

        if (isOnSlope && canWalkOnSlope && xInput == 0.0f)
        {
            rb.sharedMaterial = fullFriction;
        }
        else
        {
            rb.sharedMaterial = noFriction;
        }
    }

    private void ApplyMovement()
    {
        if (isGrounded && !isOnSlope && !isJumping && !isRolling && canMove) //if on straigth ground, not on slope
        {
            newVelocity.Set(movementSpeed * xInput *50*Time.fixedDeltaTime, rb.velocity.y); //y = 0 in the original slope code
            rb.velocity = newVelocity;
        }
        else if (isGrounded && isOnSlope && canWalkOnSlope && !isJumping && !isRolling && canMove) //If on slope
        {
            newVelocity.Set(movementSpeed * slopeNormalPerp.x * -xInput * 50 * Time.fixedDeltaTime, movementSpeed * slopeNormalPerp.y * -xInput * 50 * Time.fixedDeltaTime);
            rb.velocity = newVelocity;

       
        }
        else if (!isGrounded && canMove && !isRolling) //If in air
        {
            //newVelocity.Set(rb.velocity.x, rb.velocity.y);
            //rb.velocity = newVelocity;
            newForce.Set(170, 0.0f);
            rb.AddForce(newForce * xInput, ForceMode2D.Force);
            if (rb.velocity.x > movementSpeed-2) { rb.AddForce(-newForce, ForceMode2D.Force); }
            if (rb.velocity.x < -movementSpeed+2) { rb.AddForce(newForce, ForceMode2D.Force); }
            if (rb.velocity.x > 0.01) { rb.AddForce(-newForce / 6, ForceMode2D.Force); }
            if (rb.velocity.x < 0.01) { rb.AddForce(newForce / 6, ForceMode2D.Force); }
        }

    }
    private void Jump()
    {
        animator.SetTrigger("Jump");
        canJump = false;
        isJumping = true;
        newVelocity.Set(0.0f, 0.0f);
        rb.velocity = newVelocity;
        //newForce.Set(xInput * (jumpForce/4), jumpForce);
        //rb.AddForce(newForce, ForceMode2D.Impulse);
        Push((jumpForce / 4), jumpForce, xInput);
    }

    private void Flip()
    {
        if(!isRolling && canMove)
        { 
        facingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackSphere.position, attack1Range);

    }
    public void Push(float pushForceX, float pushForceY, float pushDirection)
    {
        newVelocity.Set(0.0f, 0.0f);
        rb.velocity = newVelocity;
        newForce.Set(pushForceX * pushDirection, pushForceY);

        rb.AddForce(newForce , ForceMode2D.Impulse);
    }

    private void Roll()
    {
        if (!isJumping  && isGrounded)
        {
            Push(rollForce,0, facingDirection);
            animator.SetTrigger("Roll");
            isRolling = true;
            InvincibleFunction(true);
            Invoke("notInvincible", iFrames/60);

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



    public void TakeDamage(int damage, int pushForce , int pushDirection)
    {
        if(!isInvincible && !isDead)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            healthBar.SetHealth(currentHealth);


            if (currentHealth >= 0)
            {
                animator.SetTrigger("Stun");
                Push(pushForce,0, pushDirection);
            }

            if (currentHealth <= 0)
            {
                isDead = true;
                animator.SetBool("isDead", true);
                LoseCoin(playerCoin);
                this.enabled = false;
            }

            StartCoroutine(FlashWhite());
            InvincibleFunction(true);
            Invoke("notInvincible", 1);
        }

    }
   
    public void GetHardStunned()
    {

    }

    void Heal(int heal)
    {
        currentHealth += heal;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

    }

    void Attack1()
    {
        animator.Play("Chloe_Atk1");
        newVelocity.Set(0.0f, 0.0f);
        rb.velocity = newVelocity;

        //Push(3,0, facingDirection);
    }

    

    private void InvincibleFunction(bool invincible)
    {
        Physics2D.IgnoreLayerCollision(7, 8, invincible);
        Physics2D.IgnoreLayerCollision(7, 10, invincible);
        Physics2D.IgnoreLayerCollision(8, 10, invincible);
        isInvincible = invincible;
    }
    private void notInvincible()
    {
        Physics2D.IgnoreLayerCollision(7, 8, false);
        Physics2D.IgnoreLayerCollision(7, 10, false);
        Physics2D.IgnoreLayerCollision(8, 10, false);
        isInvincible = false;
    }
    void canNotJump()
    {
        canJump = false;
    }
}
