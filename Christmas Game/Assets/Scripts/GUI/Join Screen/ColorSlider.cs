using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSlider : MonoBehaviour
{
    public Image sliderBackground;
    private Slider slider;
    private Color[] colors;

    // Use this for initialization
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void Initiate(Color[] colors)
    {
        this.colors = colors;
        slider.maxValue = colors.Length - 1;
        // sliderBackground.color = colors[(int)slider.value];
        SetColor();
    }

    public void SetColor()
    {
        sliderBackground.color = colors[(int)slider.value];
    }
}
