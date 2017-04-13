using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text mPlayerName;
    public Text mLives;
    public Text mAmmo;
    public Text mBombs;

    public Color mColor;

    public void Initiate(string name, string lives, string ammo, string bombs, Color color)
    {
        SetName(name);
        SetLives(lives);
        SetAmmo(ammo);
        SetBombs(bombs);

        SetColor(color);
    }

    public void SetAmmo(string ammo)
    {
        mAmmo.text = ammo;
    }

    public void SetBombs(string bombs)
    {
        mBombs.text = bombs;
    }

    public void SetLives(string lives)
    {
        mLives.text = lives;
    }

    public void SetName(string name)
    {
        mPlayerName.text = name;
    }

    public void SetColor(Color color)
    {
        mColor = color;
        mPlayerName.color = color;
    }
}
