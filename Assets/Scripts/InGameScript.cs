using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InGameScript : MonoBehaviour
{
    GameObject player;
    PlayerScript playerScript;
    private bool gameIsFinished;
    public int enemyCounter;
    public int playerHP;
    public GameObject prefabHealth;
    public GameObject prefabUILose;
    public GameObject prefabUIWin;
    public List<GameObject> healths;
    private bool UIInstantiated;

    [SerializeField] GameObject[] enemyList;

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
            Vector3 nextPos = gameObject.transform.position + new Vector3(-300f + (healthGap * healths.Count), 125f, 0f);
            GameObject heartImage = Instantiate(prefabHealth, nextPos, Quaternion.EulerRotation(new Vector3(0, 0, 0)));
            Debug.Log(i);
            healths.Add(heartImage);
            heartImage.transform.parent = gameObject.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        enemyCounter = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerScript>();
        GettingHit();

        if(Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("MainMenu");
        }
        if(enemyCounter == 0 && UIInstantiated == false) {
            GameObject winUI = Instantiate(prefabUIWin, gameObject.transform.position, Quaternion.EulerRotation(new Vector3(0, 0, 0)));
            winUI.transform.parent = gameObject.transform;
            gameIsFinished = true;
            UIInstantiated = true;
        }

        if(gameIsFinished && Input.GetButton("Fire1")) {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void GettingHit() {
        Debug.Log(playerHP);
        Debug.Log(playerScript.lives);
        if(playerHP - playerScript.lives > 0) {
            playerHP = playerScript.lives;
            Destroy(healths[healths.Count - 1]);
            healths.RemoveAt(healths.Count - 1);
        }

        if(playerHP == 0 && UIInstantiated == false) {
            GameObject winUI = Instantiate(prefabUILose, gameObject.transform.position, Quaternion.EulerRotation(new Vector3(0, 0, 0)));
            winUI.transform.parent = gameObject.transform; 
            gameIsFinished = true;
            UIInstantiated = true;
        }
    }

    private void OnDestroy()
    {
        foreach(GameObject heart in healths)
        {
            Debug.Log(heart);
        }
    }
}
