using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.AI;

public class ProcessInputMenu : MonoBehaviour
{
    public GameObject walk;
    public GameObject wheelChair;
    public NavigationSystem navSys;
    public void ChangeMovement(int index)
    {
        if(index == 0)
        {
            //Walking movement
            walk.SetActive(true);
            wheelChair.SetActive(false);
            walk.transform.localPosition = wheelChair.transform.localPosition;
            walk.transform.localRotation = wheelChair.transform.localRotation;

        }
        else if(index == 1)
        {
            //Wheelchair movement
            walk.SetActive(false);
            wheelChair.SetActive(true);
            wheelChair.transform.localPosition = walk.transform.localPosition;
            wheelChair.transform.localRotation = walk.transform.localRotation;

        }
    }

    public void changeToWalk()
    {
        //walk.transform.Translate(wheelChair.transform.position);
        walk.transform.localPosition = wheelChair.transform.localPosition;
        walk.transform.localRotation = wheelChair.transform.localRotation;
        walk.SetActive(true);
        wheelChair.SetActive(false);
    }

    public void changeToWheelChair()
    {
        wheelChair.transform.localPosition = walk.transform.localPosition;
        wheelChair.transform.localRotation = walk.transform.localRotation;
        walk.SetActive(false);
        wheelChair.SetActive(true);
    }
}
