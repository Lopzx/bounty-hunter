using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 1;
    public PlayerScript player;
    public float timeBtwAttack;
    public float startTimeBtwAttack;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //attack
        if (timeBtwAttack <= 0)
        {
            //then it can attack
            if (collision.gameObject.tag == "Player")
            {
                player.KBCounter = player.KBTotalTime;
                if(collision.transform.position.x <= transform.position.x)
                {
                    player.KnockFromRight = true;
                }
                else
                {
                    player.KnockFromRight = false;
                }
                player.TakeDamage(damage);

                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
}
