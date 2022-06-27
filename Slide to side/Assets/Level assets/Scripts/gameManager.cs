using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject player;
    public AudioClip death;

    public Animator animator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button9))
        {
            Application.Quit();
        }
        if (SceneManager.GetActiveScene().buildIndex == 0 && Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            Invoke("levelEnd", 1);
            animator.SetTrigger("FadeOut");
        }

    }

    public void transitionToEnd()
    {
        Invoke("levelEnd", 1);
        animator.SetTrigger("FadeOut");
    }

    public void levelEnd()
    {
        int sceneIndx = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndx != 5)
        {
            SceneManager.LoadScene(sceneIndx + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void killPlayer()
    {
        Debug.Log("gamer time");
        //Do an animation
        AudioSource pSource = player.GetComponent<AudioSource>();
        float temp = player.GetComponent<AudioSource>().pitch;
        pSource.pitch = 1;
        pSource.PlayOneShot(death);
        pSource.pitch = temp;
        player.transform.position = player.GetComponent<Moverment>().lastCoords; //Returns player to last known position
    }
}
