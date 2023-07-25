using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    public Transform    head;
    public float        menuSpawnDist = 2;
    public GameObject   menu;
    public InputActionProperty showButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            //Change menu state
            menu.SetActive(!menu.activeSelf);

            //Locate menu in fron fo the person
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * menuSpawnDist;
        }
        
        //Menu always looking at the person
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;

    }
}
