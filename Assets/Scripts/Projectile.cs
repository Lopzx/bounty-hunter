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

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        projectileCount = projectileLife;
        rb.velocity = direction * speed;
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
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.transform.GetComponent<Enemy>();
            if(enemy == null)
            {
                return;
            }

            enemy.KBCounter = enemy.KBTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                enemy.KnockFromRight = true;
            }
            else
            {
                enemy.KnockFromRight = false;
            }
            enemy.TakeDamage(1);

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }
}
