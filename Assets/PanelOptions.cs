using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelOptions : MonoBehaviour
{
    public GameObject barGameobject;
    [SerializeField] private TMP_Text barComponent;


    // Start is called before the first frame update
    void Start()
    {
        barComponent = barGameobject.GetComponent<TMP_Text>();
    }

    public void writeOnBar(string str)
    {
        barComponent.text = barComponent.text + " " + str;
    }

    public void cleanBar()
    {
        barComponent.text = "";
    }

}
