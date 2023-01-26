using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] AudioSource startSound;
    public void ExitGame() {
        Application.Quit();
    }

    public void StartGame() {
        startSound.Play();
        SceneManager.LoadScene("Level 1");
    }
}
