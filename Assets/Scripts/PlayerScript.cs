using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    
    public int lives;
    public float movementSpeed;
    public float jumpForce;
    private float horizontalInput;
    private float dashingPower = 20f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    public GameObject characterSprite;
    private Rigidbody2D rb;
    Animation anim;
    SpriteRenderer[] sprites;

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;
    [SerializeField] public float iframeCounter = 0;

    //KnockBack
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public bool KnockFromRight;

    //State
    private bool jumping = false;
    private bool canDash = true;
    private bool isDashing = false;
    public bool facingRight = true;
    public bool isAttack = false;
    public bool isDeath = false;

    //Animation
    public Animator animator;


    [SerializeField] public LayerMask groundLayer;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.Euler(0, 180, 0);
        //characterSprite = transform.Find("CharacterSprite").gameObject;
        //anim = characterSprite.GetComponent<Animation>();
        //sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    void move()
    {
        if (Input.GetKeyDown(KeyCode.A) && animator.GetBool("Tiduran") == false)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
            facingRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.D) && animator.GetBool("Tiduran") == false)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            facingRight = true;
        }
        /*gameObject.transform.Translate(newPos);*/
    }

    private IEnumerator dash() {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if (this.transform.rotation.y == 0) {
            rb.velocity = new Vector2(-1f * dashingPower, 0f);
        } else {
            rb.velocity = new Vector2(dashingPower, 0f);
        }
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void jump()
    {
        jumping = true;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));

    }

    bool CheckJump()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, gameObject.transform.localScale.y,groundLayer);
        if (hit.collider == null) { return false; }
        GameObject hitObj = hit.transform.gameObject;
        if (hitObj.CompareTag("Ground") || hitObj.CompareTag("AirGround") || hitObj.CompareTag("Enemy"))
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing) { return; }
        move();
        if (Input.GetKeyDown(KeyCode.Space) && CheckJump() && isDeath == false) {
            jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) {
            StartCoroutine(dash());
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");

        //if(horizontalInput != 0 && )
        //{
        //    //walkSound.Play();
        //}

        if(rb.velocity != Vector2.zero)
        {
            //harus di check ground yg diinjek
            //harus di check apakah sound udh selesai dimainin baru bisa mainin lagi sound nya (biar gk numpuk)
            //AudioManager.instance.PlaySound("WalkDirt");
        }
           
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
    }

    private void FixedUpdate()
    {
        if(isDashing) { return; }

        if (KBCounter <= 0 && isAttack == false && animator.GetBool("IsHurt") == false && isDeath == false)
        {
           rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);
        }
        else if (KBCounter > 0)
        {
            if (KnockFromRight)
            {
                rb.velocity = new Vector2(-KBForce, KBForce);
            }
            else
            {
                rb.velocity = new Vector2(KBForce, KBForce);
            }

            KBCounter -= Time.deltaTime;
        }

        iframeCounter -= Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        if(iframeCounter <= 0)
        {
            lives -= damage;
            AudioManager.instance.PlaySound("PlayerHurt");
            Debug.Log("Damage TAKEN, the hp now is " + lives);
            animator.SetBool("IsHurt", true);
            iframeCounter = 0.5f;
        }

        if (lives <= 0)
        {
            isDeath = true;
            animator.SetTrigger("IsDeath");
            AudioManager.instance.PlaySound("PlayerDeath");

            //Destroy(gameObject);
        }
    }

    public void AlertObservers(string message)
    {
        if (message.Equals("CastingEnd"))
        {
            animator.SetBool("IsCast", false);
            animator.SetBool("IsAttack", true);
        }

        if (message.Equals("ArrowShot"))
        {
            animator.SetBool("ShootArrow", true);
        }

        if (message.Equals("AttackEnd"))
        {
            animator.SetBool("IsAttack", false);
            isAttack = false;
        }

        if (message.Equals("HurtEnd"))
        {
            animator.SetBool("IsHurt", false);
        }

        if (message.Equals("DeathEnd"))
        {
            animator.SetBool("Tiduran", true);
        }
    }
}
