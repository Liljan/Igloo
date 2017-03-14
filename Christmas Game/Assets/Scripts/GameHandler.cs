using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject[] PREFAB_PLAYERS;
    public PlayerUI[] playerUI;
    public Color[] playerColors;
    public String[] playerNames;
    public Transform[] spawnPoints;

    public int NUMBER_OF_PLAYERS = 1;

    // gameplay stats

    // Temporary solution, bad
    private int[] amountOfLives = { 0, 0, 0, 0 };

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < playerUI.Length; i++)
        {
            playerUI[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < NUMBER_OF_PLAYERS; i++)
        {
            SpawnPlayer(i);
            amountOfLives[i] = 3;
            InitGUI(i);
        }
    }

    private void SpawnPlayer(int playerIndex)
    {
        GameObject playerObj = Instantiate(PREFAB_PLAYERS[playerIndex], spawnPoints[playerIndex].position, Quaternion.identity);
        Player player = playerObj.GetComponent<Player>();

        player.Init(this, playerIndex, 20, 6);
    }

    private void InitGUI(int playerIndex)
    {
        playerUI[playerIndex].gameObject.SetActive(true);
        playerUI[playerIndex].Initiate(playerNames[playerIndex], amountOfLives[playerIndex], 20, 6, playerColors[playerIndex]);
    }

    public void SetAmountOfBombs(int playerIndex, int n)
    {
        playerUI[playerIndex].SetBombs(n);
    }

    public void SetAmountOfAmmo(int playerIndex, int n)
    {
        playerUI[playerIndex].SetAmmo(n);
    }

    public void RemovePlayer(int playerID, int attackerID)
    {
        // wait some time...
        if (attackerID >= 0)
            Debug.Log("Player " + attackerID + " fraged player " + playerID);

        // then...
        --amountOfLives[playerID];

        if (amountOfLives[playerID] >= 0)
            playerUI[playerID].SetLives(amountOfLives[playerID]);

        if (amountOfLives[playerID] > 0)
            SpawnPlayer(playerID);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
