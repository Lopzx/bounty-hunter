using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int lives;
    public float movementSpeed;
    public float jumpForce;
    public GameObject characterSprite;

    //State
    bool jumping = false;
    

    // Start is called before the first frame update
    void Start()
    {
        characterSprite = transform.Find("CharacterSprite").gameObject;
    }

    void move()
    {
        Animation anim = characterSprite.GetComponent<Animation>();
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();

        if (Input.GetKeyDown(KeyCode.A))
        {
            foreach(SpriteRenderer sprite in sprites)
            {
                sprite.flipX = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.flipX = false;
            }
        }
        Vector2 newPos = new Vector2(movementSpeed * Input.GetAxisRaw("Horizontal") * Time.deltaTime, 0);
        gameObject.transform.Translate(newPos);
    }

    void jump()
    {
        jumping = true;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));

    }

    void checkJump()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, gameObject.transform.localScale.y);
        GameObject hitObj = hit.transform.gameObject;
        Debug.DrawRay(gameObject.transform.position, Vector2.down, Color.red, 0.01f, false);
        Debug.Log(hitObj);
        if (hitObj.tag == "Ground")
        {
            jumping = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        move();
        checkJump();
        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            jump();
            Debug.Log("Jump");
        }
    }
}
