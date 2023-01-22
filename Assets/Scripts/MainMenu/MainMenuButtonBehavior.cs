using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonBehavior : MonoBehaviour
{
    public GameObject button;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = button.GetComponent<Animator>();    
    }

    // Update is called once per frame
    public void OnMouseOver() {
        anim.Play("HoverOn");
        Debug.Log("Play Hover On");
    }

    public void OnMouseExit() {
        anim.Play("HoverOff");
        Debug.Log("Play Hover Off");
    }
}
