using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{
    public Texture2D texture;
    public  Vector2 textureSize = new Vector2(2048, 2048);

    private void Start()
    {
        //Sacamos el render de la pizarra, creamos la textura y se la asignamos
        var r = GetComponent<Renderer>();
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        r.material.mainTexture = texture;
    }
    

}
