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

    public void Initiate(string name, int lives, int ammo, int bombs, Color color)
    {
        SetName(name);
        SetLives(lives);
        SetAmmo(ammo);
        SetBombs(bombs);

        SetColor(color);
    }

    public void SetAmmo(int ammo)
    {
        mAmmo.text = ammo.ToString();
    }

    public void SetBombs(int bombs)
    {
        mBombs.text = bombs.ToString();
    }

    public void SetLives(int lives)
    {
        mLives.text = lives.ToString();
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
