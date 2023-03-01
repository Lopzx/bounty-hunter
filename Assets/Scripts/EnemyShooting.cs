using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyShooting : Enemy
{
    public GameObject bullet;
    public Transform bulletPos;
    public Enemy enemy;

    private float timer;
    private PlayerScript player;

    private bool isStagger;
    private float staggerCounter;
    private float playerPosition;
    private bool facingRight;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
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

        if (distance < 9)
        {
            timer += Time.deltaTime;

            if (isStagger)
            {
                timer = 0;
            }

            if (timer > 4 && player.lives > 0)
            {
                enemy.animator.SetBool("IsCast", true);
            }
        }

        if (enemy == null)
        {
            return;
        }

        if (enemy.animator.GetBool("ShootArrow"))
        {
            timer = 0;
            if (facingRight)
            {
                GameObject peluru = Instantiate(bullet, bulletPos.position, Quaternion.Euler(0, 0, 0));
                AudioManager.instance.PlaySound("Arrow");
                peluru.GetComponent<EnemyBulletScript>().setDirection(Vector2.right);
                enemy.animator.SetBool("ShootArrow", false);
            }
            else
            {
                GameObject peluru = Instantiate(bullet, bulletPos.position, Quaternion.Euler(0, 180, 0));
                AudioManager.instance.PlaySound("Arrow");
                peluru.GetComponent<EnemyBulletScript>().setDirection(Vector2.left);
                enemy.animator.SetBool("ShootArrow", false);
            }
        }
    }

    private void FixedUpdate()
    {
        playerPosition = player.transform.position.x;

        if (transform.position.x > playerPosition)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            facingRight = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            facingRight = true;
        }
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
