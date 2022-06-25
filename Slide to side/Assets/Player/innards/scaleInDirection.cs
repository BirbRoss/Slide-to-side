using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scaleInDirection : MonoBehaviour
{
    [Header("Expand")]
    public int startCount = 1;
    public int startMax = 1;

    [Header("Expand")]
    public int expandCount = 1;
    public int expandMax = 1; //WHEN INCREASING MAX ALSO INCREASE THE COUNT TOO
    public TMP_Text expandText;
    public bool inverse;
    [SerializeField] bool sidePicked = false;
    public Moverment moveScript;
    Vector3 gOffset;
    Vector3 gDist;

    [Header("Side selection")]
    public TMP_Text sideText;
    [SerializeField] int sideSelect = 0;
    [SerializeField] bool inUse = false;
    public float resizeFactor = 1.0f;
    public TMP_Text warning; 
    bool running = false;

    void Start()
    {
        moveScript = gameObject.GetComponent<Moverment>();
        gOffset = moveScript.groundOffset;
        gDist = moveScript.groundDistance;
        warning.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Resize") > 0 && expandCount > 0 && !inUse) //if the value is up and you can expand
        {

                expandCount--;
                expandText.text = expandCount.ToString();
                inUse = true;

                inverse = false;
                sidePicked = true;
                resizeSide();
        }
        else if (Input.GetAxis("Resize") < 0 && expandCount < expandMax && !inUse) //if the value is down and not 0
        {
                expandCount++; //WHEN INCREASING MAX ALSO INCREASE THE COUNT TOO
                expandText.text = expandCount.ToString();
                inUse = true;

                inverse = true;

                if (expandCount == expandMax)
                {
                    sidePicked = false; //Resets side picked when the player hasn't expanded any side
                }

                resizeSide();
        }


        if (Input.GetAxis("changeSide") > 0 && sideSelect < 4 && !inUse) //if the value is right
        {
            if (!sidePicked)
            {
                sideSelect++;
                changeToSide();
            }
            else
            {
                if (!running)
                {
                    StartCoroutine(flashWarning());
                }
            }
        }
        else if (Input.GetAxis("changeSide") < 0 && sideSelect > 0 && !inUse) // if the value is left
        {
            if (!sidePicked)
            {
                sideSelect--;
                changeToSide();
                
            }
            else
            {
                if (!running)
                {
                    StartCoroutine(flashWarning());
                }
            }
        }
        
        if (Input.GetAxis("changeSide") == 0 && Input.GetAxis("Resize") == 0 && inUse) //Can only reset if neither of the buttons are being pressed to prevent fuckery
        {
            inUse = false;
        }
    }

    void changeToSide()
    {
        inUse = true;
        switch(sideSelect) //Switches to the side of the value swapped to
        {
            case 0: //front
                sideText.text = "Front";
                break;
            case 1: //back
                sideText.text = "Back";
                break;
            case 2: //left
                sideText.text = "Left";
                break;
            case 3: //right
                sideText.text = "Right";
                break;
            case 4: //up
                sideText.text = "Up";
                break;
            /*case 5: //down
                sideText.text = "Down"; //REMOVED BECAUSE IT BROKE THE GAME AND ISN@
                break;*/
        }
    }

    public void resizeSide()
    {
        Vector3 posShift = transform.position;
        Vector3 resizeAmount = transform.localScale;

        if (!inverse)
        {
            switch (sideSelect) //Switches to the side of the value swapped to
            {
                case 0: //front
                    posShift.z += (resizeFactor / 2);
                    resizeAmount.z += resizeFactor;
                    gDist.z += 0.5f;
                    break;
                case 1: //back
                    posShift.z += -(resizeFactor / 2);
                    resizeAmount.z += resizeFactor;
                    gDist.z += 0.5f;
                    break;
                case 2: //left
                    posShift.x += -(resizeFactor / 2);
                    resizeAmount.x += resizeFactor;
                    gDist.x += 0.5f;
                    break;
                case 3: //right
                    posShift.x += (resizeFactor / 2);
                    resizeAmount.x += resizeFactor;
                    gDist.x += 0.5f;
                    break;
                case 4: //up
                    posShift.y += (resizeFactor / 2); //Not sure if needed but keeping just in case
                    resizeAmount.y += resizeFactor;
                    gOffset.y -= 0.5f;
                    break;
                    /*case 5: //down
                        posShift.y += (resizeFactor / 2); //REMOVED BECAUSE DOWN ISN'T USEFUL FOR PUZZLES AND IT JUST BREAKS THINGS
                        resizeAmount.y += resizeFactor;
                        break;*/
                }
            }
        else
        {
            switch (sideSelect) //Switches to the side of the value swapped to
            {
                case 0: //front
                    posShift.z += (resizeFactor / 2);
                    resizeAmount.z -= resizeFactor;
                    gDist.z -= 0.5f;
                    break;
                case 1: //back
                     posShift.z += -(resizeFactor / 2);
                     resizeAmount.z -= resizeFactor;
                     gDist.z -= 0.5f;
                    break;
                case 2: //left
                    posShift.x += -(resizeFactor / 2);
                    resizeAmount.x -= resizeFactor;
                    gDist.x -= 0.5f;
                    break;
                case 3: //right
                    posShift.x += (resizeFactor / 2);
                    resizeAmount.x -= resizeFactor;
                    gDist.x -= 0.5f;
                    break;
                case 4: //up
                    posShift.y -= (resizeFactor / 2);
                    resizeAmount.y -= resizeFactor;
                    gOffset.y += 0.5f;
                    break;
                /*case 5: //down
                    posShift.y -= -(resizeFactor / 2);
                    resizeAmount.y -= resizeFactor;
                    break;*/
            }
        }

        //Chances the ground check ray cast and then moves the player to the correct position
        moveScript.groundOffset = gOffset;
        moveScript.groundDistance = gDist;
        transform.position = posShift;
        transform.localScale = resizeAmount;

        
    }

    public void updateText()
    {
        expandText.text = expandCount.ToString();
    }

    public void resetCount()
    {

        for (int i = expandCount; i < expandMax; i++)
        {
            inverse = true;
            resizeSide(); //Resize in the opposite direction to shrink back
        }

        expandMax = startMax;
        expandCount = startCount;
    }

    private IEnumerator flashWarning()
    {
        running = true;

        warning.enabled = true;
        yield return new WaitForSeconds(0.75f);
        warning.enabled = false;
        yield return new WaitForSeconds(0.75f);
        warning.enabled = true;
        yield return new WaitForSeconds(0.75f);
        warning.enabled = false;
        yield return new WaitForSeconds(0.75f);
        warning.enabled = true;
        yield return new WaitForSeconds(0.75f);
        warning.enabled = false;
        yield return new WaitForSeconds(0.75f);

        running = false;
    }
}
