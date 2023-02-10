using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    private Rigidbody2D rb;
    public EnemyDetectPlayer detectAI;
    private PlayerScript playerScript;

    public Transform posOne;
    public Transform posTwo;
    public Transform target;

    private float enemyPosition;
    private float targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        target = posOne;
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy Move
        if (detectAI.PlayerInArea)
        {
            if(playerScript.lives > 0)
            {
                target = detectAI.Player;
            }
            else
            {
                target = posOne;
            }
            
        }
        else if (detectAI.PlayerInArea == false && target == detectAI.Player)
        {
            target = posOne;
        }
        else if (Vector2.Distance(transform.position, posOne.position) <= 0.2f)
        {
            target = posTwo;
        }
        else if (Vector2.Distance(transform.position, posTwo.position) <= 0.2f)
        {
            target = posOne;
        }
    }

    private void FixedUpdate()
    {
        targetPosition = target.position.x;

        if(transform.position.x > targetPosition)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }

        if (KBCounter <= 0 && isAttack == false && isHurt == false && isDeath == false)
        {
            Move();
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
    }

    public void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
