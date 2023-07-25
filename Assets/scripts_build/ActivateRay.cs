using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateRay : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    public InputActionProperty button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (button.action.enabled)
        {
            leftHand.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRRayInteractor>().enabled = true;
            rightHand.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRRayInteractor>().enabled = true;
        }
        else
        {
            leftHand.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRRayInteractor>().enabled = false;
            rightHand.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRRayInteractor>().enabled = false;
        }
    }
}
