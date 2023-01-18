using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScript : MonoBehaviour
{
    GameObject player;
    PlayerScript playerScript;
    public int playerHP;
    public GameObject prefabHealth;
    public List<GameObject> healths;

    //Script Settings
    public float healthGap;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerScript>();
        playerHP = playerScript.lives;

        for (int i = 0; i < playerHP; i++)
        {
            Vector3 nextPos = gameObject.transform.position + new Vector3(-371f + (healthGap * healths.Count), 150f, 0f);
            GameObject heartImage = Instantiate(prefabHealth, nextPos, Quaternion.Euler(0, 0, 0));
            //GameObject heartImage = Instantiate(prefabHealth, nextPos, Quaternion.EulerRotation(new Vector3(0, 0, 0)));
            //GameObject peluru = Instantiate(projectile, launchPoint2.position, Quaternion.Euler(0, 0, 0));
            Debug.Log(i);
            healths.Add(heartImage);
            heartImage.transform.parent = gameObject.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        playerHP = playerScript.lives;
        
    }

    private void OnDestroy()
    {
        foreach(GameObject heart in healths)
        {
            Debug.Log(heart);
        }
    }
}
