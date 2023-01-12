using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLaunch : MonoBehaviour
{
    public GameObject projectile;
    public Transform launchPoint;
    public PlayerScript playerScript;
    public bool facingRight;

    public float shootTime;
    public float shootCounter;

    // Start is called before the first frame update
    void Start()
    {
        shootCounter = shootTime;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        facingRight = playerScript.facingRight;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && shootCounter <= 0)
        {
            if (facingRight)
            {
                Instantiate(projectile, launchPoint.position, Quaternion.Euler(0, 0, -90));
            }
            else
            {
                //doesnt work dont know why
                Instantiate(projectile, launchPoint.position, Quaternion.Euler(0, 0, -90));
            }
            shootCounter = shootTime;
        }
        shootCounter -= Time.deltaTime;
    }
}
