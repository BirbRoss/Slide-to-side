using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (SceneManager.GetActiveScene().buildIndex == 0 && Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            levelEnd();
        }

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
        player.transform.position = player.GetComponent<Moverment>().lastCoords; //Returns player to last known position
    }
}
