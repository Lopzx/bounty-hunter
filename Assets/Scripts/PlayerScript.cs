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

    //Health
    public GameObject prefabHealth;
    public List<GameObject> healths;
    public float healthGap;

    [SerializeField] public LayerMask groundLayer;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        characterSprite = transform.Find("CharacterSprite").gameObject;
        anim = characterSprite.GetComponent<Animation>();
        sprites = GetComponentsInChildren<SpriteRenderer>();

        //for (int i = 0; i < lives; i++)
        //{
        //    Vector3 nextPos = gameObject.transform.position + new Vector3(-371f + (healthGap * healths.Count), 150f, 0f);
        //    GameObject heartImage = Instantiate(prefabHealth, nextPos, Quaternion.Euler(0, 0, 0));
        //    //GameObject heartImage = Instantiate(prefabHealth, nextPos, Quaternion.EulerRotation(new Vector3(0, 0, 0)));
        //    //GameObject peluru = Instantiate(projectile, launchPoint2.position, Quaternion.Euler(0, 0, 0));
        //    Debug.Log(i);
        //    healths.Add(heartImage);
        //    heartImage.transform.parent = gameObject.transform;
        //}
    }

    void move()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //foreach(SpriteRenderer sprite in sprites)
            //{
            //    sprite.flipX = true;
            //}
            transform.rotation = Quaternion.Euler(0,180,0);
            facingRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            //foreach (SpriteRenderer sprite in sprites)
            //{
            //    sprite.flipX = false;
            //}
            transform.rotation = Quaternion.Euler(0, 0, 0);
            facingRight = true;
        }
        /*gameObject.transform.Translate(newPos);*/
    }

    private IEnumerator dash() {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if (this.transform.rotation.y == -1) {
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
        if (hitObj.CompareTag("Ground") || hitObj.CompareTag("AirGround"))
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
        if (Input.GetKeyDown(KeyCode.Space) && CheckJump()) {
            jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) {
            StartCoroutine(dash());
        }

        //attack
        if (timeBtwAttack <= 0)
        {
            //then you can attack
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    Enemy enemyScript = enemiesToDamage[i].GetComponent<Enemy>();
                    if (enemyScript != null)
                    {
                        enemyScript.KBCounter = enemyScript.KBTotalTime;
                        if (facingRight)
                        {
                            enemyScript.KnockFromRight = false;
                        }
                        else
                        {
                            enemyScript.KnockFromRight = true;
                        }
                        enemyScript.TakeDamage(damage);
                    }
                }
                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if(isDashing) { return; }

        if (KBCounter <= 0)
        {
           rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);
        }
        else
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
        
    }

    public void TakeDamage(int damage)
    {
        lives -= damage;

        //InGameScript

        if (lives <= 0)
        {
            Destroy(gameObject);
        }

        Debug.Log("Damage TAKEN, the hp now is " + lives);
    }
}
