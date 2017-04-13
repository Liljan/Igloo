using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Handler : MonoBehaviour
{
    public PlayerUI[] playerUI;
    public string[] playerNames;

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

    public void SetLivesUI(int playerID, string lives)
    {
        playerUI[playerID].SetLives(lives);
    }

    public void SetAmmoUI(int playerID, string ammo)
    {
        playerUI[playerID].SetAmmo(ammo);
    }

    public void EnableVictory(int playerID)
    {
        victoryText.gameObject.SetActive(true);
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
