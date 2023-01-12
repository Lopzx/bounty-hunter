using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public float distance;

    //KnockBack
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public bool KnockFromRight;

    [SerializeField] float baseCastDist;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Damage TAKEN, the hp now is " + health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
