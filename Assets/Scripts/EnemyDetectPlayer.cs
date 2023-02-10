using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectPlayer : MonoBehaviour
{
    [field: SerializeField]
    public bool PlayerInArea { get; private set; }
    public Transform Player { get; private set; }
    private PlayerScript playerScript;

    [SerializeField]
    private string detectionTag = "Player";

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            if(playerScript.lives > 0)
            {
                PlayerInArea = true;
                Player = collision.gameObject.transform;
            }
            else
            {
                PlayerInArea = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            PlayerInArea = false;
        }
    }
}
