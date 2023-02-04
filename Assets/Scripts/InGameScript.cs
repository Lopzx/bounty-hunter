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
    public GameObject healthBar;
    public GameObject prefabHealth;
    public GameObject prefabUILose;
    public GameObject prefabUIWin;
    public List<GameObject> healths;
    private bool UIInstantiated;

    [SerializeField] GameObject[] enemyList;

    //Script Settings
    public float healthGap;

    void DrawHealth() {
        healths.ForEach((GameObject x) => { Destroy(x); });
        healths.Clear();
        RectTransform rTrans = gameObject.GetComponent<RectTransform>();

        for (int i = 0; i < playerHP; i++) {
            Vector3 nextPos =new Vector3(i * healthGap, 0,0);
            Debug.Log(nextPos);
            GameObject heartImage = Instantiate(prefabHealth, nextPos, Quaternion.EulerRotation(new Vector3(0, 0, 0)));
            heartImage.transform.SetParent(healthBar.transform, false);
            healths.Add(heartImage);
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerScript>();
        playerHP = playerScript.lives;
        DrawHealth();

    }

    void ReSetValue() {
        healths.ForEach((GameObject x) => {
            x.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);
            /* x.transform.localPosition.Set(0, 0, 0);*/
        });
    }
    // Update is called once per frame
    void Update()
    {
        /*prefabHealth = GameObject.Find("HP");*/
        /*DrawHealth();*/
       /* ReSetValue();*/
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
