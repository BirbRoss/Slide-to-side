using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject player;

    public void levelEnd()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void killPlayer()
    {
        Debug.Log("gamer time");
        //Do an animation
        player.transform.position = player.GetComponent<Moverment>().lastCoords; //Returns player to last known position
    }
}
