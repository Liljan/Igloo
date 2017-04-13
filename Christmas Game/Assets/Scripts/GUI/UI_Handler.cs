using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Handler : MonoBehaviour
{
    public PlayerUI[] playerUI;
    public string[] playerNames;

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

    // Update is called once per frame
    void Update()
    {

    }

}
