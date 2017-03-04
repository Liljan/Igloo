using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    private TextMesh textMesh;
    // Use this for initialization
    void Start()
    {
        textMesh = GetComponent<TextMesh>();
    }

    public void SetText(string text)
    {
        textMesh.text = text;
    }
}
