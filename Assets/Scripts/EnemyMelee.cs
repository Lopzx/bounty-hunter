using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    private Rigidbody2D rb;
    public EnemyDetectPlayer detectAI;

    public Transform posOne;
    public Transform posTwo;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = posOne;
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    private void FixedUpdate()
    {
        if (KBCounter <= 0)
        {
            Move();
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

    public void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
