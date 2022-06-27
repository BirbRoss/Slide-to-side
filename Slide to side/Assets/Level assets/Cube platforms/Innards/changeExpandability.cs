using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeExpandability : MonoBehaviour
{
    [Header("Point changing")]
    public int pointsToChange = 1;
    public bool invert;
    public int sideToShrink;
    public bool resetBarrier = false;
    [SerializeField] bool removeAfter;
    public AudioClip up;
    public AudioClip down;
    bool triggered;

    [Header("Handles reactivating old cubes when a expand count is subtracted to prevent soft locking")]
    public GameObject[] cubeToActivate;

    private void OnEnable()
    {
        triggered = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        scaleInDirection scaleScript = col.gameObject.GetComponent<scaleInDirection>();

        if (scaleScript != null && !triggered)
        {

            if (!resetBarrier && !invert)
            {
                scaleScript.expandCount += pointsToChange;
                scaleScript.expandMax += pointsToChange;

                gameObject.GetComponent<AudioSource>().PlayOneShot(up);
            }
            else if (!resetBarrier && invert)
            {
                int curSide = scaleScript.sideSelect;

                scaleScript.sideSelect = sideToShrink;
                scaleScript.inverse = true;
                scaleScript.resizeSide();
                scaleScript.sideSelect = curSide;

                gameObject.GetComponent<AudioSource>().PlayOneShot(down);
            }
            else
            {
                scaleScript.resetCount();
                gameObject.GetComponent<AudioSource>().PlayOneShot(down);
            }

            scaleScript.updateText();

            if (removeAfter)
            {
                triggered = true;
                Invoke("delayDeactivate", 0.496f);
            }

        }
        else
        {
            Debug.Log("Error - No scale script found");
        }

        if (cubeToActivate != null)
        {
            for (int i = 0; i < cubeToActivate.Length; i++)
            {
                cubeToActivate[i].SetActive(true);
            }
        }

        
    }

    void delayDeactivate()
    {
        gameObject.SetActive(false);
    }
}
