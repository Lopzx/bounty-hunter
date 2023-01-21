using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : Enemy
{
    public GameObject bullet;
    public Transform bulletPos;
    public Enemy enemy;

    private float timer;
    private GameObject player;

    private bool isStagger;
    private float staggerCounter;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        staggerCounter -= Time.deltaTime;

        if (staggerCounter <= 0)
        {
            isStagger = false;
        }

        if (distance < 6)
        {
            timer += Time.deltaTime;

            if (isStagger)
            {
                timer = 0;
            }

            if (timer > 2)
            {
                enemy.animator.SetBool("IsCast", true);
                if (enemy.animator.GetBool("ShootArrow"))
                {
                    shoot();
                    timer = 0;
                }
            }
        }
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        enemy.animator.SetBool("ShootArrow", false);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        enemy.animator.SetBool("IsCast", false);
        enemy.animator.SetBool("IsAttack", false);
        isStagger = true;
        staggerCounter = 1;
    }
}
