using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Animations;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public float speed;
    public float distance;

    //KnockBack
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public bool KnockFromRight;
    public bool isAttack;
    public bool isHurt;
    public bool isDeath;

    [SerializeField] float baseCastDist;

    //Animation
    public Animator animator;

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

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Damage TAKEN, the hp now is " + health);
        animator.SetBool("IsHurt", true);
        AudioManager.instance.PlaySound("EnemyHurt");
        isHurt = true;

        if (health <= 0)
        {
            isDeath = true;
            animator.SetTrigger("IsDeath");
            AudioManager.instance.PlaySound("PlayerDeath");
            StartCoroutine(DestroyEnemy());
        }
    }

    public void AlertObservers(string message)
    {
        if (message.Equals("CastingEnd"))
        {
            animator.SetBool("IsCast", false);
            animator.SetBool("IsAttack", true);
        }

        if (message.Equals("SwordHit"))
        {
            animator.SetBool("SwordHit", true);
        }

        if (message.Equals("ArrowShot"))
        {
            animator.SetBool("ShootArrow", true);
        }

        if (message.Equals("AttackEnd"))
        {
            animator.SetBool("IsAttack", false);
            animator.SetBool("SwordHit", false);
        }

        if (message.Equals("HurtEnd"))
        {
            animator.SetBool("IsHurt", false);
            isHurt = false;
        }

        if (message.Equals("DeathEnd"))
        {
            animator.SetBool("Tiduran", true);
        }
    }
}
