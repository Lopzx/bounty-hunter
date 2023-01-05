using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public int health;
    public int damage = 1;
    public float speed;
    public float distance;
    public float timeBtwAttack;

    private bool movingRight = true;

    public EnemyDetectPlayer detectAI;

    Vector3 baseScale;

    public Transform groundDetection;
    public Transform posOne;
    public Transform posTwo;
    public Transform target;

    [SerializeField] float baseCastDist;
    

    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        target = posOne;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        //Enemy Move
        if (detectAI.PlayerInArea)
        {
            target = detectAI.Player;
        }
        else if (detectAI.PlayerInArea == false && target == detectAI.Player)
        {
            target = posOne;
        }
        else if (Vector2.Distance(transform.position, posOne.position) <= 0.2f)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            target = posTwo;
        }
        else if (Vector2.Distance(transform.position, posTwo.position) <= 0.2f)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            target = posOne;
        }

        //RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);

        //if (groundInfo.collider == false)
        //{
        //    if (movingRight == true)
        //    {
        //        transform.eulerAngles = new Vector3(0, -180, 0);
        //        movingRight = false;
        //    }
        //    else
        //    {
        //        transform.eulerAngles = new Vector3(0, 0, 0);
        //        movingRight = true;
        //    }
        //}

        //Enemy Follow
        //if (Mathf.Abs(transform.position.x - target.position.x))
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        //}
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void Attack()
    {

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Damage TAKEN, the hp now is " + health);
    }
}
