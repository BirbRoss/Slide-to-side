using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endLevel : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.name == "Player")
        {
            Debug.Log("End level");
        }
    }
}
