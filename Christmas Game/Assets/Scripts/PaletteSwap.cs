using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PaletteSwap : MonoBehaviour
{
    private Material _mat;

    public Color skin_1;
    public Color skin_2;
    public Color jacket_1;
    public Color jacket_2;
    public Color pants;

    void OnEnable()
    {
        Shader shader = Shader.Find("Sprites/Palette-Swapper");

        if (_mat == null)
        {
            _mat = new Material(shader);
        }

        GetComponent<SpriteRenderer>().material = _mat;

        _mat.SetColor("skin_1", skin_1);
        _mat.SetColor("skin_2", skin_2);
        _mat.SetColor("jacket_1", jacket_1);
        _mat.SetColor("jacket_2", jacket_2);
        _mat.SetColor("pants", pants);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        //_mat.SetColor("_IN0", _IN0);
        // _mat.SetColor("_OUT0", _OUT0);

        /* _mat.SetColor("skin_1", skin_1);
         _mat.SetColor("skin_2", skin_2);
         _mat.SetColor("jacket_1", jacket_1);
         _mat.SetColor("jacket_2", jacket_2);
         _mat.SetColor("pants", pants);*/

        Graphics.Blit(src, dst, _mat);
    }

    void Start()
    {
        _mat.SetColor("skin_1", skin_1);
        _mat.SetColor("skin_2", skin_2);
        _mat.SetColor("jacket_1", jacket_1);
        _mat.SetColor("jacket_2", jacket_2);
        _mat.SetColor("pants", pants);
    }

    public void Initiate(Color skin, Color jacket, Color pants, float darken)
    {
        skin_1 = skin;
        skin_2 = new Color(skin.r - darken, skin.g - darken, skin.b - darken, 1.0f);
        jacket_1 = jacket;
        jacket_2 = new Color(jacket.r - darken, jacket.g - darken, jacket.b - darken, 1.0f);
        this.pants = pants;

        _mat.SetColor("skin_1", skin_1);
        _mat.SetColor("skin_2", skin_2);
        _mat.SetColor("jacket_1", jacket_1);
        _mat.SetColor("jacket_2", jacket_2);
        _mat.SetColor("pants", pants);
    }
}
