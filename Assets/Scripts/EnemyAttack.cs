using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    //public Enemy enemy;
    public EnemyMelee enemy;
    public float timeBtwAttack;
    public float startTimeBtwAttack;

    private void OnCollisionStay2D(Collision2D collision)
    {
        //attack
        if (timeBtwAttack <= 0)
        {
            //then it can attack
            if (collision.gameObject.tag == "Player" && enemy.isAttack == false)
            {
                enemy.isAttack = true;
                enemy.animator.SetBool("IsCast", true);
            }

            if(enemy == null)
            {
                return;
            }

            if (enemy.animator.GetBool("SwordHit"))
            {
                timeBtwAttack = startTimeBtwAttack;
                enemy.animator.SetBool("SwordHit", false);
                enemy.isAttack = false;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
}
