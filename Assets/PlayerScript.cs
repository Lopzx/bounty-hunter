using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{   
    public float movementSpeed;
    public GameObject characterSprite;
    

    // Start is called before the first frame update
    void Start()
    {
        characterSprite = transform.Find("CharacterSprite").gameObject;
    }

    void move()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            characterSprite.transform.localRotation = new Quaternion(0, 180, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            characterSprite.transform.localRotation = new Quaternion(0, 0, 0, 0);
        }
        Vector2 newPos = new Vector2(movementSpeed * Input.GetAxisRaw("Horizontal") * Time.deltaTime, 0);
        gameObject.transform.Translate(newPos);
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
}
