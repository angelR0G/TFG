using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WhiteboardMarker : MonoBehaviour
{
    //Posición y grosor de la punta
    [SerializeField] private Transform _tip;
    [SerializeField] private int _penSize = 5;

    //El componente Render, colores y altura de la punta
    private Renderer _render;
    private Color[] _colors;
    private float _tipHeight;

    //Donde tocas, la pizarra y las coordenadas de la textura de la pizarra
    private RaycastHit _touch;
    private Whiteboard _whiteboard;
    private Vector2 _touchPos, _lastTouchedPos;
    private bool _touchedlastFrame;
    private Quaternion _lastTouchedRot;
    
    void Start()
    {
        //Asignamos el render, los colores y la altura de la punta
        _render = _tip.GetComponent<Renderer>();
        _colors = Enumerable.Repeat(_render.material.color, _penSize*_penSize).ToArray();
        _tipHeight = _tip.localScale.y;
    }
    
    void Update()
    {
        Draw();
    }

    private void Draw()
    {
        //Si la punta toca algo
        if (Physics.Raycast(_tip.position, transform.up, out _touch, _tipHeight))
        {
            //Si es la pizarra
            if (_touch.transform.CompareTag("Whiteboard"))
            {
                //Si la pizarra está vacia lo rellenamos con el scrip de la pizarra tocada
                if (_whiteboard == null)
                {
                    _whiteboard = _touch.transform.GetComponent<Whiteboard>();
                }
                
                //Nos guardamos las coordenadas de donde hemos tocado
                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);
                
                //Como las coordenadas van de 0 a 1, lo pasamos a pixeles de la textura
                var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (_penSize / 2));
                var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (_penSize / 2));

                //Nos salimos si pintamos fuera de la pantalla
                if (x < 0 || x > _whiteboard.textureSize.x || y < 0 || y > _whiteboard.textureSize.y) return;
                
                if (_touchedlastFrame)
                {
                    //Pintamos justo donde está el rotulador
                    _whiteboard.texture.SetPixels(x, y, _penSize, _penSize, _colors);

                    //Básicamente pintamos una "raya" entre el punto anterior y este
                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchedPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchedPos.y, y, f);
                        _whiteboard.texture.SetPixels(lerpX, lerpY, _penSize, _penSize, _colors);
                    }

                    transform.rotation = _lastTouchedRot;
                    _whiteboard.texture.Apply();
                }

                _lastTouchedPos = new Vector2(x, y);
                _lastTouchedRot = transform.rotation;
                _touchedlastFrame = true;
                return;

            }
        }

        _whiteboard = null;
        _touchedlastFrame = false;
    }
}
