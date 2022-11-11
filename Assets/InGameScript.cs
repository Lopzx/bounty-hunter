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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerScript>();
        playerHP = playerScript.lives;
        for (int i = 0; i < playerHP; i++)
        {
            Vector3 nextPos = gameObject.transform.position + new Vector3(-200f + (50 * healths.Count), 100f, 0f);
            GameObject heartImage = Instantiate(prefabHealth, nextPos, Quaternion.EulerRotation(new Vector3(0, 0, 0)));

            healths.Add(heartImage);
            heartImage.transform.parent = gameObject.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        playerHP = playerScript.lives;
        Debug.Log(gameObject);
       
    }
}
