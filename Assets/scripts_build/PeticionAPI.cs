using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

//Classes to store API response
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

//Get build data
[System.Serializable]
public class Feature
{
    public string type;
    public Geometry geometry;
    public Properties properties;
}

[System.Serializable]
public class Geometry
{
    public string type;
    public List<List<List<List<double>>>> coordinates;
}

[System.Serializable]
public class Properties
{
    public string codigo;
    public int id_actividad;
    public string nombre_actividad;
    public double superficie;
    public double lon;
    public double lat;
    public string id_departamentosigua;
    public string nombre_departamentosigua;
    public string denominacion;
    public object ubicaciones;
    public string observaciones;
    public string crue;
    public string u21;
    public string activresum;
}

[System.Serializable]
public class Root
{
    public string type;
    public List<Feature> features;
}

//Get 
[System.Serializable]
public class BuildPerson
{
    public string id;
    public string plaza;
    public string categoria;
    public string id_departamentosigua;
    public string codigo;
}
[System.Serializable]
public class BuildPersonRoot
{
    public BuildPerson[] items;
}

[System.Serializable]
public class Person
{
    public string id;
    public string apellido1;
    public string apellido2;
    public string nombre;
    public int destinos;
    public int destinos_pas;
    public int destinos_pdi;
    public int destinos_cargo;
}

[System.Serializable]
public class PersonRoot
{
    public Person[] items;
}

public class PeticionAPI : MonoBehaviour
{
    private BuildPersonRoot responseAllRooms;
    public static T ImportJson<T>(string path)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        return JsonUtility.FromJson<T>(textAsset.text);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    public void initBuildRoom()
    {
        //Get all instances form the EPSiV and the person id
        var siguaAPI = "http://www.sigua.ua.es/api/pub/ubicacion_persona/edificio/0039/items";
        var request = (HttpWebRequest)WebRequest.Create(siguaAPI);
        request.Method = "GET";

        var response = (HttpWebResponse)request.GetResponse();
        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        response.Close();
        //Debug.Log(responseString);

        //Root objectResponse = JsonUtility.FromJson<Root>(responseString);
        string newResponse = "{\"items\":" + responseString + "}";
        responseAllRooms = JsonConvert.DeserializeObject<BuildPersonRoot>(newResponse);
    }

    //Get all instances from the EPS IV by the level
    //Input: Level
    //Output: Response estructure
    public Root getBuildInstancesByLevel(int l)
    {
        string level = getLevel(l);
        var siguaAPI = "http://www.sigua.ua.es/api/pub/estancia/edificio/0039/planta/" + level + "/items";
        var request = (HttpWebRequest)WebRequest.Create(siguaAPI);
        request.KeepAlive = true;
        request.Timeout = 50000;
        request.Method = "GET";

        var response = (HttpWebResponse)request.GetResponse();
        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        //Debug.Log(responseString);

        //Root objectResponse = JsonUtility.FromJson<Root>(responseString);
        Root objectResponse = JsonConvert.DeserializeObject<Root>(responseString);
        response.Close();
        return objectResponse;
    }

    public Person getPersonByRoom(string room)
    {
        string personID = "";
        bool end = false;
        for (int i = 0; i < responseAllRooms.items.Length && !end; i++)
        {
            if (responseAllRooms.items[i].codigo == room)
            {
                personID = responseAllRooms.items[i].id;
                end = true;
            }
        }
        PersonRoot personRoom;
        if (personID != "")
        {
            var siguaAPI = "http://www.sigua.ua.es/api/pub/persona/" + personID;
            var request = (HttpWebRequest)WebRequest.Create(siguaAPI);
            request.KeepAlive = true;
            request.Timeout = 50000;
            request.Method = "GET";

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var newResponse = "{\"items\":" + responseString + "}";
            //Debug.Log(responseString);
            response.Close();
            //Root objectResponse = JsonUtility.FromJson<Root>(responseString);
            personRoom = JsonConvert.DeserializeObject<PersonRoot>(newResponse);
        }
        else
        {
            string defaultString = "\n[{\"id\":\"-1\",\"apellido1\":\"null\",\"apellido2\":\"null\",\"nombre\":\"DESPACHO\",\"destinos\":1,\"destinos_pas\":0,\"destinos_pdi\":1,\"destinos_cargo\":0}]";
            string newdefaultString = "{\"items\":" + defaultString + "}";
            personRoom = JsonConvert.DeserializeObject<PersonRoot>(newdefaultString);
        }

        return personRoom.items[0];
    }

    public Person getPersonByRoomLocal(string room)
    {
        BuildPersonRoot objectResponse = ImportJson<BuildPersonRoot>("Assets/scripts_build/JSON/IDPersonRoom");
        string personID = "";
        bool end = false;
        for (int i = 0; i < objectResponse.items.Length && !end; i++)
        {
            if (objectResponse.items[i].codigo == room)
            {
                personID = objectResponse.items[i].id;
                end = true;
            }
        }
        PersonRoot personRoom;
        if (personID != "")
        {
            var siguaAPI = "http://www.sigua.ua.es/api/pub/persona/" + personID;
            var request = (HttpWebRequest)WebRequest.Create(siguaAPI);
            request.KeepAlive = true;
            request.Timeout = 5000;
            request.Method = "GET";

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var newResponse = "{\"items\":" + responseString + "}";
            //Debug.Log(responseString);

            //Root objectResponse = JsonUtility.FromJson<Root>(responseString);
            personRoom = JsonConvert.DeserializeObject<PersonRoot>(newResponse);
        }
        else
        {
            string defaultString = "\n[{\"id\":\"-1\",\"apellido1\":\"null\",\"apellido2\":\"null\",\"nombre\":\"DESPACHO\",\"destinos\":1,\"destinos_pas\":0,\"destinos_pdi\":1,\"destinos_cargo\":0}]";
            string newdefaultString = "{\"items\":" + defaultString + "}";
            personRoom = JsonConvert.DeserializeObject<PersonRoot>(newdefaultString);
        }

        return personRoom.items[0];
    }

    private string getLevel(int level)
    {
        string levelS = "PB";
        switch (level)
        {
            case -1:
                levelS = "PS";
                break;
            case 0:
                levelS = "PB";
                break;
            default:
                levelS = "P" + level;
                break;
        }
        return levelS;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
