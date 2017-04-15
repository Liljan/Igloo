using System.Collections.Generic;
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
    Texture2D mColorSwapTex;
    Color[] mSpriteColors;

    SpriteRenderer mSpriteRenderer;

    float mHitEffectTimer = 0.0f;
    const float cHitEffectTime = 0.1f;

    void Awake()
    {
        mSpriteRenderer = GetComponent<SpriteRenderer>();
        InitColorSwapTex();
        SwapDemoColors();
    }

    public void SwapDemoColors()
    {
       // SwapColor(SwapIndex.SkinPrim, Color.red);
        //SwapColor(SwapIndex.SkinSec, Color.cyan);
        SwapColor(SwapIndex.Pants, ColorFromInt(0xc4ce00));
        SwapColor(SwapIndex.SkinPrim, ColorFromInt(0x784a00));
        SwapColor(SwapIndex.Pants, ColorFromInt(0x594f00));
        mColorSwapTex.Apply();
    }

    public void StartHitEffect()
    {
        mHitEffectTimer = cHitEffectTime;
        SwapAllSpritesColorsTemporarily(Color.white);
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
            colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));

        colorSwapTex.Apply();

        mSpriteRenderer.material.SetTexture("_SwapTex", colorSwapTex);

        mSpriteColors = new Color[colorSwapTex.width];
        mColorSwapTex = colorSwapTex;
    }

    public void SwapColor(SwapIndex index, Color color)
    {
        mSpriteColors[(int)index] = color;
        mColorSwapTex.SetPixel((int)index, 0, color);
    }


    public void SwapColors(List<SwapIndex> indexes, List<Color> colors)
    {
        for (int i = 0; i < indexes.Count; ++i)
        {
            mSpriteColors[(int)indexes[i]] = colors[i];
            mColorSwapTex.SetPixel((int)indexes[i], 0, colors[i]);
        }
        mColorSwapTex.Apply();
    }

    public void ClearColor(SwapIndex index)
    {
        Color c = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        mSpriteColors[(int)index] = c;
        mColorSwapTex.SetPixel((int)index, 0, c);
    }

    public void SwapAllSpritesColorsTemporarily(Color color)
    {
        for (int i = 0; i < mColorSwapTex.width; ++i)
            mColorSwapTex.SetPixel(i, 0, color);
        mColorSwapTex.Apply();
    }

    public void ResetAllSpritesColors()
    {
        for (int i = 0; i < mColorSwapTex.width; ++i)
            mColorSwapTex.SetPixel(i, 0, mSpriteColors[i]);
        mColorSwapTex.Apply();
    }

    public void ClearAllSpritesColors()
    {
        for (int i = 0; i < mColorSwapTex.width; ++i)
        {
            mColorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));
            mSpriteColors[i] = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
        mColorSwapTex.Apply();
    }

    public void Update()
    {
        if (mHitEffectTimer > 0.0f)
        {
            mHitEffectTimer -= Time.deltaTime;
            if (mHitEffectTimer <= 0.0f)
                ResetAllSpritesColors();
        }
    }
}
