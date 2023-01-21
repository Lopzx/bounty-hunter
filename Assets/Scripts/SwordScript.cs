using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public Enemy enemy;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemy.animator.GetBool("SwordHit"))
        {
            PlayerScript player = collision.transform.parent.GetComponent<PlayerScript>();
            player.KBCounter = player.KBTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                player.KnockFromRight = true;
            }
            else
            {
                player.KnockFromRight = false;
            }
            player.TakeDamage(enemy.damage);
        }
    }
}
