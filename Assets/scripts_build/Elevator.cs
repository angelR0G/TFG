using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    //public int floor;
    private Transform playerTransform;
    private Transform wheelchairTransform;
    private int selectedFloor, actualFloor;
    private MovementType movType;
    // Start is called before the first frame update
    void Awake()
    {
        movType = GameObject.Find("EPSIV").GetComponent<MovementType>();
        playerTransform = GameObject.Find("XR Origin").transform;
        wheelchairTransform = GameObject.Find("XR Origin Silla").transform;
    }

    public void SelectFloor(int floor)
    {
        selectedFloor = floor;
        /*if(selectedFloor > actualFloor)
        {
            playerTransform.position += new Vector3(0, 6, 0);
        }
        else if(selectedFloor < actualFloor)
        {
            playerTransform.position -= new Vector3(0, 6, 0);
        }*/
        /*if(selectedFloor != actualFloor)
        {

        }*/
        if (movType.getMovementType())
        {
            playerTransform.position = new Vector3(playerTransform.position.x, (selectedFloor*3)+0.3f, playerTransform.position.z);
        }
        else
        {

            GameObject phys = wheelchairTransform.GetChild(3).gameObject;
            Rigidbody r = phys.transform.GetChild(0).GetComponent<Rigidbody>();
            r.position = new Vector3(r.position.x, (selectedFloor * 3) + 0.84f, r.position.z);
            /*r = phys.transform.GetChild(1).GetComponent<Rigidbody>();
            r.position = new Vector3(r.position.x, (selectedFloor * 3) + 0.5f, r.position.y);
            r = phys.transform.GetChild(2).GetComponent<Rigidbody>();
            r.position = new Vector3(r.position.x, (selectedFloor * 3) + 0.5f, r.position.y);
            r = phys.transform.GetChild(3).GetComponent<Rigidbody>();
            r.position = new Vector3(r.position.x, (selectedFloor * 3) + 0.5f, r.position.y);
            r = phys.transform.GetChild(4).GetComponent<Rigidbody>();
            r.position = new Vector3(r.position.x, (selectedFloor * 3) + 0.5f, r.position.y);*/
            /*phys.transform.GetChild(0).GetComponent<Rigidbody>().
            phys.transform.GetChild(2).GetComponent<Rigidbody>().
            phys.transform.GetChild(3).GetComponent<Rigidbody>().
            phys.transform.GetChild(4).GetComponent<Rigidbody>().*/
            wheelchairTransform.localPosition = new Vector3(wheelchairTransform.localPosition.x, (wheelchairTransform.localPosition.y + selectedFloor * 3) + 0.5f, wheelchairTransform.localPosition.z);


        }
        actualFloor = selectedFloor;
        Debug.Log(selectedFloor);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
