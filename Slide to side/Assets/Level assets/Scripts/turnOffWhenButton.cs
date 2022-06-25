using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOffWhenButton : MonoBehaviour
{
    public buttonHandle button;

    // Update is called once per frame
    void Update()
    {
        if (button.buttonOn)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        else if (!button.buttonOn)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
