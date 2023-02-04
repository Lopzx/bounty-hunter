using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private GameObject player;
    public Rigidbody2D rb;
    public float force;
    private float timer;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        rb.velocity = direction * force;

        //Vector3 direction = player.transform.position - transform.position;
        //rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        // rotation
        //float rot = Mathf.Atan2(-direction.y, -direction.x);
        //transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 7)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerScript player = collision.transform.parent.GetComponent<PlayerScript>();
            if(player == null)
            {
                return;
            }
            AudioManager.instance.PlaySound("ArrowHit");
            player.KBCounter = player.KBTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                player.KnockFromRight = true;
            }
            else
            {
                player.KnockFromRight = false;
            }
            player.TakeDamage(1);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            AudioManager.instance.PlaySound("ArrowHit");
            Destroy(gameObject);
        }
    }

    public void setDirection(Vector2 direction)
    {
        this.direction = direction;
    }
}
