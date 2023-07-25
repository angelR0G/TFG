using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GPSEncoder;

public class generateBuild : MonoBehaviour
{
    public List<GameObject> block = new List<GameObject>();
    private GameObject levelFloor;
    private int LEVELHEIGH = 3;
    //private PeticionAPI p;
    void Start()
    {
        //Set the center of the EPS IV as (0, 0) in the scene
        SetLocalOrigin(new Vector2(38.382815304599426f, -0.5098065315781743f));

        //Get the response from the api
        PeticionAPI p = gameObject.GetComponent<PeticionAPI>();

        //Instantiate floor object
        /*Quaternion rotation = Quaternion.Euler(0, 0, 0);
        GameObject aux = block[6];
        Vector3 pos = new Vector3(0.0f, -0.0001f, 0.0f);
        Instantiate(aux, pos, rotation);*/

        //Init floor level
        int level = -1;
        p.initBuildRoom();

        while (level < 3)
        {
            Root response = p.getBuildInstancesByLevel(level);
            
            //Empty gameobject to store all data segments
            levelFloor = new GameObject();
            levelFloor.name = "Floor: " + level;

            for (int i = 0; i < response.features.Count; ++i)
            {
                //Create build border with response data
                //renderFloor(response, levelFloor, level, i);

                //Instantiate the rest
                if (response.features[i].properties.crue == "DOCENCIA" || response.features[i].properties.crue == "DESPACHOS Y SEMINARIOS")
                {
                    createClass(response, i, level, p);
                }
                else
                {
                    //Draw bathroom
                    if(response.features[i].properties.activresum == "Aseos" && response.features[i].properties.superficie > 9.5)
                    {
                        //drawInstance((float)response.features[i].properties.lat, (float)response.features[i].properties.lon, level, 7, 0, new Vector3(0, 0, 0), response.features[i].properties.codigo);
                    }
                    else if(response.features[i].properties.activresum == "Aseos" && response.features[i].properties.superficie < 9 && response.features[i].properties.superficie > 3)
                    {

                    }

                    //Draw elevator
                    if(response.features[i].properties.nombre_actividad == "Ascensor")
                    {
                        //16.3
                        drawInstance((float)response.features[i].properties.lat, (float)response.features[i].properties.lon, level, 8, 71.6f, new Vector3(-0.6f, 0.06f, 0), response.features[i].properties.codigo);
                    }
                    
                }

            }
            level++;
        }

    }


    GameObject drawInstance(float lat, float lon, int floor, int num, float rot, Vector3 offset, string name)
    {
        Quaternion rotation = Quaternion.Euler(0, rot, 0);
        float latitude = lat;
        float longitude = lon;
        Vector3 uCoords = GPSToUCS(latitude, longitude);
        if (floor == 2)
        {
            uCoords.y = (floor * 3.09f) + offset.y;
        }else if (floor == 1)
        {
            uCoords.y = (floor * 3.05f) + offset.y;
        }
        else
        {

            uCoords.y = (floor * LEVELHEIGH) + offset.y;
        }
        uCoords.x += offset.x;
        uCoords.z += offset.z;
        GameObject a = Instantiate(block[num], uCoords, rotation);

        a.name = name;
        return a;
    }

    void createClass(Root response, int i, int level, PeticionAPI p)
    {
        
        float latitude = (float)response.features[i].properties.lat;
        float longitude = (float)response.features[i].properties.lon;
        string name = response.features[i].properties.codigo;
        Debug.Log("Name: " + name); 
        GameObject aux = null;
        if (response.features[i].properties.u21 == "INFORM") {
            //GameObject desp = drawInstance(latitude, longitude, level, 2, -16.3f, new Vector3(0.0f, 0.0f, 0.0f), name);
            List<GameObject> textList = getTextObject(name, level, false, false);
            if(textList[0] != null)
            {
                TMPro.TextMeshProUGUI textUi = textList[0].GetComponent<TMPro.TextMeshProUGUI>();
                textUi.text = response.features[i].properties.denominacion;
            }
            
        }
        else if (response.features[i].properties.codigo != "0039PS054" && response.features[i].properties.superficie > 170) {
            //drawInstance(latitude, longitude, level, 1, 0, new Vector3(0.0f, 0.0f, 0.0f), name);
            List<GameObject> textList = getTextObject(name, level, true, false);
            if (textList[0] != null && textList[1] != null)
            {
                TMPro.TextMeshProUGUI textUi = textList[0].GetComponent<TMPro.TextMeshProUGUI>();
                TMPro.TextMeshProUGUI textUi1 = textList[1].GetComponent<TMPro.TextMeshProUGUI>();
                textUi.text = response.features[i].properties.denominacion;
                textUi1.text = response.features[i].properties.denominacion;
            }
        }
        else if (response.features[i].properties.codigo != "0039PS054" && response.features[i].properties.superficie > 120 && response.features[i].properties.superficie < 160) {
            //drawInstance(latitude, longitude, level, 0, -16.3f, new Vector3(0.0f, 0.0f, 0.0f), name);
            /*renderFloor(response, levelFloor, level, i);*//*aux = block[0]; rotation = Quaternion.Euler(0, 0, 0) renderFloor(response, levelFloor, level, i); */
            List<GameObject> textList = getTextObject(name, level, true, false);
            if(textList[0] != null && textList[1] != null)
            {
                TMPro.TextMeshProUGUI textUi = textList[0].GetComponent<TMPro.TextMeshProUGUI>();
                TMPro.TextMeshProUGUI textUi1 = textList[1].GetComponent<TMPro.TextMeshProUGUI>();
                textUi.text = response.features[i].properties.denominacion;
                textUi1.text = response.features[i].properties.denominacion;
            }
            
        }
        else if (response.features[i].properties.u21 == "DESP" && response.features[i].properties.superficie < 15) {
            GameObject desp = drawInstance(latitude, longitude, level, 4, 253.7f, new Vector3(0.0f, 0.0f, 0.0f), name);
            List<GameObject> textList = getTextObject(name, level, false, true);
            if (textList[0] != null)
            {
                TMPro.TextMeshProUGUI textUi = textList[0].GetComponent<TMPro.TextMeshProUGUI>();
                p = gameObject.GetComponent<PeticionAPI>();
                Person prof = p.getPersonByRoom(desp.name);
                if (prof.id != "-1")
                {
                    textUi.text = prof.apellido1 + " " + prof.apellido2 + ", " + prof.nombre;
                }
                else
                {
                    textUi.text = "Despacho";
                }
            }
            desp.transform.parent = GameObject.Find("P" + level + "Level").transform;
            
        }
        else if (response.features[i].properties.u21 == "DESP" && response.features[i].properties.superficie > 15) { 
            /*aux = block[5]; rotation = Quaternion.Euler(0, 0, 0); renderFloor(response, levelFloor, level, i);*/ 
        }
        else if(response.features[i].properties.codigo != "0039PS054" && response.features[i].properties.activresum == "Docencia")
        { 
            //Aula de teoria pequena
            if(response.features[i].properties.codigo == "0039PS018" || response.features[i].properties.codigo == "0039PB008")
            {
                //drawInstance(latitude, longitude, level, 3, -16.3f-90.0f+180.0f, new Vector3(1.0f, 0.0f, 2.0f), name);
            }
            else
            {
                //drawInstance(latitude, longitude, level, 3, -16.3f, new Vector3(0.0f, 0.0f, 0.0f), name);
            }
            List<GameObject> textList = getTextObject(name, level, false, false);
            if (textList[0] != null)
            {
                GameObject text = textList[0];
                TMPro.TextMeshProUGUI textUi = text.GetComponent<TMPro.TextMeshProUGUI>();
                textUi.text = response.features[i].properties.denominacion;
            }
            
            /*renderFloor(response, levelFloor, level, i);*//*aux = block[3]; rotation = Quaternion.Euler(0,0,0);renderFloor(response, levelFloor, level, i); */
        }else if(response.features[i].properties.codigo == "0039PS054")
        {
            //drawInstance(latitude, longitude, level, 9, -16.3f+90.0f, new Vector3(0.0f, 0.0f, 0.0f), name);
        }
        //Instantiate(aux, uCoords, rotation);
    }

    List<GameObject> getTextObject(string name, int level, bool hasTwoDoors, bool desp)
    {
        List<GameObject> gameText = new List<GameObject>();
        if(!desp)
            gameText.Add(GameObject.Find("EPSIV/" + changeLevelToText(level) + "/" + name + "/Canvas/Panel/PuertaNombre"));
        else
        {
            gameText.Add(GameObject.Find(name + "/Canvas/Panel/PuertaNombre"));
        }

        if(hasTwoDoors) gameText.Add(GameObject.Find("EPSIV/" + changeLevelToText(level) + "/" + name + "/Canvas1/Panel/PuertaNombre"));

        return gameText;
    }

    string changeLevelToText(int level)
    {
        string levelText = "";
        switch(level){
            case -1:
                levelText = "PSLevel";
                break;
            case 0:
                levelText = "PBLevel";
                break;
            case 1:
                levelText = "P1Level";
                break;
            case 2:
                levelText = "P2Level";
                break;
        }
        return levelText;
    }

    void renderFloor(Root response, GameObject levelFloor, int level, int i)
    {
        for (int j = 0; j < response.features[i].geometry.coordinates[0][0].Count - 1; j++)
        {
            float latS = (float)response.features[i].geometry.coordinates[0][0][j][1];
            float lonS = (float)response.features[i].geometry.coordinates[0][0][j][0];
            float latE = (float)response.features[i].geometry.coordinates[0][0][j + 1][1];
            float lonE = (float)response.features[i].geometry.coordinates[0][0][j + 1][0];
            Vector3 start = GPSToUCS(latS, lonS);
            Vector3 end = GPSToUCS(latE, lonE);
            //listVertices.Add(new Vector3(start.x, level * 6, start.z));

            GameObject startObj = new GameObject();
            GameObject endObj = new GameObject();
            Vector3 startV = new Vector3(start.x, level * 6, start.z);
            Vector3 endV = new Vector3(end.x, level * 6, end.z);
            startObj.transform.localPosition = startV;
            endObj.transform.localPosition = endV;
            startObj.transform.LookAt(endObj.transform.position);
            endObj.transform.LookAt(startObj.transform.position);
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            // get distance
            float distance = Vector3.Distance(startV, endV);
            Renderer wallRender = wall.GetComponentsInChildren<Renderer>()[0];
            wallRender.material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            // Set possition
            wall.name = "Wall" + latS + "-" + lonS + "|||" + latE + "-" + lonE;
            wall.transform.localScale = new Vector3(0.2f, 0.2f, distance);
            wall.transform.position = startObj.transform.position + distance / 2 * startObj.transform.forward;
            wall.transform.rotation = startObj.transform.rotation;
            wall.transform.parent = levelFloor.transform;
            Destroy(startObj);
            Destroy(endObj);
        }

    }

    /*void createFloor(Root response, GameObject levelFloor, int level, int i)
    {
        float latS = (float)response.features[i].geometry.coordinates[0][0][0][1];
        float lonS = (float)response.features[i].geometry.coordinates[0][0][0][0];
        float latE = (float)response.features[i].geometry.coordinates[0][0][0 + 1][1];
        float lonE = (float)response.features[i].geometry.coordinates[0][0][0 + 1][0];
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Renderer wallRender = wall.GetComponentsInChildren<Renderer>()[0];
        wall.name = "SUELO";
        wallRender.material.color = new Color(1.0f, 0.0f, 0.0f);
        Vector3 firstCorner = GPSToUCS(latS, lonS);
        Vector3 secondCorner = GPSToUCS(latE, lonE);
        for (int j = 2; j < response.features[i].geometry.coordinates[0][0].Count - 1; j++)
        {
            latS = (float)response.features[i].geometry.coordinates[0][0][j][1];
            lonS = (float)response.features[i].geometry.coordinates[0][0][j][0];
            Vector3 start = GPSToUCS(latS, lonS);
            Vector3 startV = new Vector3(start.x, level * 6, start.z);

            if (Vector3.Distance(firstCorner, secondCorner) < Vector3.Distance(firstCorner, startV))
            {
                secondCorner = startV;
            }

        }

        Vector3 between = firstCorner - secondCorner;
        float distance = between.magnitude;
        wall.transform.localScale = new Vector3(distance, wall.transform.localScale.y, wall.transform.localScale.z);
        wall.transform.position = firstCorner + (between / 2.0f);
        wall.transform.LookAt(secondCorner);

    }*/

}
