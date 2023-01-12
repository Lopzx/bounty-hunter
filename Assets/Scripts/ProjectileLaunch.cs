using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLaunch : MonoBehaviour
{
    public GameObject projectile;
    public Transform launchPoint;
    public Transform launchPoint2;
    public Transform launchPoint3;
    public PlayerScript playerScript;
    public bool facingRight;

    public float shootTime;
    public float shootCounter;

    // Start is called before the first frame update
    void Start()
    {
        shootCounter = shootTime;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        facingRight = playerScript.facingRight;

        if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.E) && shootCounter <= 0)
        {
            GameObject peluru = Instantiate(projectile, launchPoint2.position, Quaternion.Euler(0, 0, 0));
            peluru.GetComponent<Projectile>().SetDirection(Vector2.up);
            shootCounter = shootTime;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.E) && shootCounter <= 0)
        {
            GameObject peluru = Instantiate(projectile, launchPoint3.position, Quaternion.Euler(0, 0, 0));
            peluru.GetComponent<Projectile>().SetDirection(Vector2.down);
            shootCounter = shootTime;
        }
        else if (Input.GetKeyDown(KeyCode.E) && shootCounter <= 0)
        {
            if (facingRight)
            {
                GameObject peluru = Instantiate(projectile, launchPoint.position, Quaternion.Euler(0, 0, -90));
                peluru.GetComponent<Projectile>().SetDirection(Vector2.right);
            }
            else {
                GameObject peluru = Instantiate(projectile, launchPoint.position, Quaternion.Euler(0, 180, 90));
                peluru.GetComponent<Projectile>().SetDirection(Vector2.left);
            }
            shootCounter = shootTime;
        }
        shootCounter -= Time.deltaTime;
    }
}
