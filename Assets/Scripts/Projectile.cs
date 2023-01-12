using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    public float projectileLife;
    public float projectileCount;

    public PlayerScript playerScript;
    public bool facingRight;
    // Start is called before the first frame update
    void Start()
    {
        projectileCount = projectileLife;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        facingRight = playerScript.facingRight;
        if (!facingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        projectileCount -= Time.deltaTime;
        if (projectileCount <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (facingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.transform.GetComponent<Enemy>();
            if(enemy == null)
            {
                return;
            }
            enemy.TakeDamage(1);
        }
        Destroy(gameObject);
    }
}
