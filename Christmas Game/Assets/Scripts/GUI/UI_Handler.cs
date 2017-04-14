﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Handler : MonoBehaviour
{
    public PlayerUI[] playerUI;
    public Text victoryText;

    private int amountOfPlayers;

    // Use this for initialization
    void Awake()
    {
        for (int i = 0; i < playerUI.Length; i++)
        {
            playerUI[i].gameObject.SetActive(false);
        }
    }

    public void Initialize(int amountOfPlayers)
    {
        this.amountOfPlayers = amountOfPlayers;

        for (int i = 0; i < amountOfPlayers; i++)
        {
            playerUI[i].gameObject.SetActive(true);
        }
    }

    public void SetUIName(int playerID, string name)
    {
        playerUI[playerID].SetName(name);
    }

    public void SetUIColor(int playerID, Color color)
    {
        playerUI[playerID].SetColor(color);
    }

    public void SetUILives(int playerID, string lives)
    {
        playerUI[playerID].SetLives(lives);
    }

    public void SetUIAmmo(int playerID, string ammo)
    {
        playerUI[playerID].SetAmmo(ammo);
    }

    public void EnableVictory(string playerName, Color color)
    {
        victoryText.gameObject.SetActive(true);
        victoryText.color = color;

        victoryText.text = playerName + "!\nA WINNER IS YOU!";
    }

    public void EnableTie()
    {
        victoryText.gameObject.SetActive(true);
        victoryText.text = "Tie!\nIs that even possible?!";
    }

    public void DisableVictory()
    {
        victoryText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
