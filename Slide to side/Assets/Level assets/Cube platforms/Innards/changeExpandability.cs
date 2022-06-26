using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeExpandability : MonoBehaviour
{
    [Header("Point changing")]
    public int pointsToChange = 1;
    public bool resetBarrier = false;
    [SerializeField] bool removeAfter;

    [Header("Handles reactivating old cubes when a expand count is subtracted to prevent soft locking")]
    public GameObject[] cubeToActivate;

    private void OnTriggerEnter(Collider col)
    {
        scaleInDirection scaleScript = col.gameObject.GetComponent<scaleInDirection>();

        if (scaleScript != null)
        {
            if (!resetBarrier)
            {
                scaleScript.expandCount += pointsToChange;
                scaleScript.expandMax += pointsToChange;
            }
            else
            {
                scaleScript.resetCount();
            }

            scaleScript.updateText();

            if (removeAfter)
            {
                gameObject.SetActive(false);
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
}
