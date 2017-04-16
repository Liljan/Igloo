using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteSwap : MonoBehaviour
{
    public Material _mat;

    public Color _IN0;
    public Color _OUT0;

    void OnEnable()
    {
        Shader shader = Shader.Find("Sprites/Palette-Swapper");

        /*
        if (_mat == null)
        {
            _mat = new Material(shader);
        }
        */
        Debug.Log(_IN0.r);
        _mat.SetColor("_IN0", _IN0);
        _mat.SetColor("_OUT0", _OUT0);
       
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        //_mat.SetColor("_IN0", _IN0);
       // _mat.SetColor("_OUT0", _OUT0);

        Graphics.Blit(src, dst, _mat);
    }

    void Start()
    {
        Debug.Log(_mat.GetColor("_IN0"));
        Debug.Log(_mat.GetColor("_OUT0"));
    }
}
