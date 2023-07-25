using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using TMPro;

public class RobotController : MonoBehaviour
{
    public GameObject robotDogModel = null;
    public TMP_Text triggerText = null;
    public InputActionProperty buttonInteract;
    public Transform target;
    private bool useRobot = false;
    private bool useRobotAsGuide = false;

    public bool GetUseRobot()
    {
        return useRobot;
    }

    public bool GetUseRobotAsGuide()
    {
        return useRobotAsGuide;
    }

    void Start()
    {
        //triggerText.enabled = false;
    }

    public void toggleactivateRobot()
    {
        useRobot = !useRobot;
    }
    public void activateRobot(bool value)
    {
        useRobot = value;
    }

    public void activateRobotGuide()
    {
        useRobotAsGuide = !useRobotAsGuide;
    }

    void Update()
    {
        if(useRobot)
            robotDogModel.GetComponent<NavMeshAgent>().SetDestination(transform.position);
        if(useRobotAsGuide)
            robotDogModel.GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
    }
}
