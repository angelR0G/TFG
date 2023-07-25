using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using Microsoft.MixedReality.Toolkit.Experimental.UI;

public class ActivateKeyBoard : MonoBehaviour
{
    private TMP_InputField inputField;

    //Variables for keyboard positioning 
    public float distance = 0.5f;
    public float verticalOffset = -0.5f;
    public Transform positionSource;

    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener(x => OpenKeyboard());
    }

    public void OpenKeyboard()
    {
        //Get keyboard reference
        NonNativeKeyboard keyboard = NonNativeKeyboard.Instance;
        //Assign hand input field to the keyboard
        keyboard.InputField = inputField;
        //Show keyboard
        keyboard.PresentKeyboard(inputField.text);

        //Calculate final keyboard position
        Vector3 dir = positionSource.forward;
        dir.y = 0;
        dir.Normalize();
        Vector3 targetPos = positionSource.position + dir * distance + Vector3.up * verticalOffset;

        keyboard.RepositionKeyboard(targetPos);

        SetCursorColorAlpha(1);
        keyboard.OnClosed += Instance_OnClosed;

    }

    void Instance_OnClosed(object sender, System.EventArgs e)
    {
        SetCursorColorAlpha(0);
        NonNativeKeyboard.Instance.OnClosed -= Instance_OnClosed;
    }

    public void SetCursorColorAlpha(float value)
    {
        inputField.customCaretColor = true;
        Color cursorColor = inputField.caretColor;
        cursorColor.a = value;
        inputField.caretColor = cursorColor;
    }
}
