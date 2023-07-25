using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectPlayer : MonoBehaviour
{
    GameObject player;
    public Lights lights;
    public GameObject sw;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("XR Origin");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider playerCollider)
    {
        GameObject menu = GameObject.Find("HandMenu");
        for(int i = 0; i < sw.GetComponent<Lights>().lights.Count; i++)
        {
            if(menu.GetComponent<Lights>().lights.Count < sw.GetComponent<Lights>().lights.Count)
                menu.GetComponent<Lights>().lights.Add(sw.GetComponent<Lights>().lights[i]);
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject menu = GameObject.Find("HandMenu");
        menu.GetComponent<Lights>().lights.Clear();
    }
}
