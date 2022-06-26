using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathPlane : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.name == "Player")
        {
            FindObjectOfType<gameManager>().killPlayer();
        }
    }
}
