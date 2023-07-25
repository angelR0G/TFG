using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresencePhysics : MonoBehaviour
{
    public Transform target;
    private Rigidbody rigidBody;

    public Renderer hand;
    public float showHandDistance = 0.05f;

    //Array with hand collisions
    private Collider[] handColliders;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        handColliders = GetComponentsInChildren<Collider>();
    }

    public void EnableHandColliders()
    {
        /*
        foreach(var collider in handColliders)
        {
            collider.enabled = true;
        }*/
    }
    public void DisableHandColliders()
    {
        /*
        foreach (var collider in handColliders)
        {
            collider.enabled = false;
        }*/
    }
    public void EnableHandCollidersDelay(float delay)
    {
        //Invoke("EnableHandColliders", delay);
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if(distance > showHandDistance)
        {
            hand.enabled = true;
        }
        else
        {
            hand.enabled = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Apply target position to physical hands
        rigidBody.velocity = (target.position - transform.position) / Time.fixedDeltaTime;

        //Calculate rotation
        Quaternion rotationDiff = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDiff.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);

        Vector3 rotationDifferenceDegree = angleInDegree * rotationAxis;

        //Apply rotation
        rigidBody.angularVelocity = (rotationDifferenceDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }
}
