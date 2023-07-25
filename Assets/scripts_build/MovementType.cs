using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementType : MonoBehaviour
{
    [SerializeField]
    private bool isWalking = true;
    public GameObject walk;
    public GameObject chair;

    public void Start()
    {
        chair.SetActive(false);
    }
    public void setMovementType(bool type)
    {
        isWalking = type;
    }

    public bool getMovementType()
    {
        return isWalking;
    }
}
