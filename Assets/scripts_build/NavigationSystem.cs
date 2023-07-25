using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class NavigationSystem : MonoBehaviour
{

    public LineRenderer line;
    public Transform target;
    public NavMeshAgent agent;
    public TMP_InputField inputField;
    public RobotController robotController;

    [Header("Private values")]
    [SerializeField] private bool followLine = false;


    private void Start()
    {
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        getPath();
        if (followLine && gameObject.tag != "Dog")
            gameObject.GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
    }


    private void LateUpdate()
    {
        //transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
    }

    void getPath()
    {
        line.SetPosition(0, transform.position);
        agent.SetDestination(target.position);
        //if(!robotController.GetUseRobotAsGuide())
        DrawPath(agent.path);
        agent.isStopped = !followLine; //Don't move the agent object
    }

    //Render the calculated path
    void DrawPath(NavMeshPath path)
    {
        if (path.corners.Length < 2) //if the path has 1 or no corners, there is no need
            return;
        line.positionCount = path.corners.Length;

        for (var i = 1; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]); //go through each corner and set that to the line renderer's position
        }
    }

    //Search a class by a code provided
    //Used for buttons with a class code
    public void searchClassByCode(string code)
    {

        GameObject cl = GameObject.Find(code + "/TargetAI");
        GameObject cl1 = GameObject.Find(code + "/TargetAI1");

        //Get the closest door to the player
        float distance = 0, distance1 = 0;
        if (cl != null)
            distance = Vector3.Distance(cl.transform.position, target.position);
        if (cl1 != null)
            distance1 = Vector3.Distance(cl1.transform.position, target.position);

        if (distance != 0 && distance > distance1)
            target.position = cl.transform.position;
        if (distance1 != 0 && distance1 > distance)
            target.position = cl1.transform.position;

    }

    //Search a class by the code written at the textfield
    public void searchClass()
    {
        string code = inputField.text = "0039" + inputField.text.ToUpper();
        GameObject cl = GameObject.Find(code + "/TargetAI");
        GameObject cl1 = GameObject.Find(code + "/TargetAI1");
        //Get the closest door to the player
        float distance = 0, distance1 = 0;
        if (cl != null)
            distance = Vector3.Distance(cl.transform.position, target.position);
        if (cl1 != null)
            distance1 = Vector3.Distance(cl1.transform.position, target.position);

        if (distance != 0 && distance > distance1)
            target.position = cl.transform.position;
        if (distance1 != 0 && distance1 > distance)
            target.position = cl1.transform.position;

    }

    //Toggle if the player follows the line or not
    //Called with the "seguir" ui button at the menu
    public void changeFollowLine()
    {
        followLine = !followLine;
    }
}