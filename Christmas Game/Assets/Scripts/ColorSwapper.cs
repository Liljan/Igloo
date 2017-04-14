﻿using System.Collections.Generic;
using UnityEngine;
using System;

public enum SwapIndex
{
    Outline = 0,
    SkinPrim = 255,
    SkinSec = 239,
    JacketPrim = 62,
    JacketSec = 70,
    JacketThird = 255,
    Glove = 204,
    Pants = 72,
    RightShoe = 255,
    LeftShoe = 248
}

public class ColorSwapper : MonoBehaviour
{
    private Texture2D mColorSwapTex;
    private Color[] mSpriteColors;

    public SpriteRenderer mSpriteRenderer;

    public void Awake()
    {
        mSpriteRenderer = GetComponent<SpriteRenderer>();
        InitColorSwapTex();
        SwapDemoColors();
    }

    public void SwapDemoColors()
    {
        SwapColor(SwapIndex.SkinPrim, ColorFromInt(0x784a00));
        SwapColor(SwapIndex.SkinSec, ColorFromInt(0x4c2d00));
        SwapColor(SwapIndex.JacketPrim, ColorFromInt(0xc4ce00));
        SwapColor(SwapIndex.JacketSec, ColorFromInt(0x784a00));
        SwapColor(SwapIndex.Pants, ColorFromInt(0x594f00));
        mColorSwapTex.Apply();
    }

    public static Color ColorFromInt(int c, float alpha = 1.0f)
    {
        int r = (c >> 16) & 0x000000FF;
        int g = (c >> 8) & 0x000000FF;
        int b = c & 0x000000FF;

        Color ret = ColorFromIntRGB(r, g, b);
        ret.a = alpha;

        return ret;
    }

    public static Color ColorFromIntRGB(int r, int g, int b)
    {
        return new Color((float)r / 255.0f, (float)g / 255.0f, (float)b / 255.0f, 1.0f);
    }

    public void InitColorSwapTex()
    {
        Texture2D colorSwapTex = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
        colorSwapTex.filterMode = FilterMode.Point;

        for (int i = 0; i < colorSwapTex.width; ++i)
        {
            colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));
        }

        colorSwapTex.Apply();

        mSpriteRenderer.material.SetTexture("_SwapTex", colorSwapTex);

        mSpriteColors = new Color[colorSwapTex.width];
        mColorSwapTex = colorSwapTex;

        Debug.Log("Orn");
    }

    public void SwapColor(SwapIndex index, Color color)
    {
        mSpriteColors[(int)index] = color;
        mColorSwapTex.SetPixel((int)index, 0, color);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}