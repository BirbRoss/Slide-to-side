using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOffWhenButton : MonoBehaviour
{
    public buttonHandle button;
    public bool flip = false;

    // Update is called once per frame
    void Update()
    {
        if (button.buttonOn)
        {
            gameObject.GetComponent<Collider>().enabled = flip;
            gameObject.GetComponent<MeshRenderer>().enabled = flip;
        }
        else if (!button.buttonOn)
        {
            gameObject.GetComponent<Collider>().enabled = !flip;
            gameObject.GetComponent<MeshRenderer>().enabled = !flip;
        }
    }
}
