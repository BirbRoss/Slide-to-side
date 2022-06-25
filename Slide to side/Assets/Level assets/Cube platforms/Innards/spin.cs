using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{

    public float rotSpeed = 90.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
    }
}
