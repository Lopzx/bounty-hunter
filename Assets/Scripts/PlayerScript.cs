using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int lives;
    public float movementSpeed;
    public float jumpForce;
    private float horizontalInput;

    public GameObject characterSprite;
    private Rigidbody2D rb;

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;

    //State
    public bool jumping = false;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        characterSprite = transform.Find("CharacterSprite").gameObject;
    }

    void move()
    {
        Animation anim = characterSprite.GetComponent<Animation>();
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();

        if (Input.GetKeyDown(KeyCode.A))
        {
            foreach(SpriteRenderer sprite in sprites)
            {
                sprite.flipX = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.flipX = false;
            }
        }
        /*gameObject.transform.Translate(newPos);*/
    }

    void jump()
    {
        jumping = true;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));

    }

    bool CheckJump()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, gameObject.transform.localScale.y);
        if (hit.collider == null) { return false; }
        GameObject hitObj = hit.transform.gameObject;
        if (hitObj.CompareTag("Ground"))
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        if (Input.GetKeyDown(KeyCode.Space) && CheckJump())
        {
            jump();
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
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
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
        rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);
    }
}
