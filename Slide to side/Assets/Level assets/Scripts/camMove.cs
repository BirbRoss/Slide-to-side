using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMove : MonoBehaviour
{

    public float rotSpeed;
    public float maxRotLeft = 45.0f;
    public float maxRotRight = 180.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("CamHorizontal") > 0 && transform.rotation.eulerAngles.y < maxRotRight)
        {
            transform.Rotate(Vector3.up, rotSpeed * Input.GetAxis("CamHorizontal") * Time.deltaTime);
        }
        else if (Input.GetAxis("CamHorizontal") < 0 && transform.rotation.eulerAngles.y > maxRotLeft)
        {
            transform.Rotate(-Vector3.up, -rotSpeed * Input.GetAxis("CamHorizontal") * Time.deltaTime);
        }
    }
}
