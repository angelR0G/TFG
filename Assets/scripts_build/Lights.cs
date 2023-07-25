using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{

    public List<Light> lights; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ToggleLights()
    {
        foreach(Light l in lights){
            l.enabled = !l.enabled;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
