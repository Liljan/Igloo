﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject PREFAB_PLAYER;
    public Color[] playerColors;
    public string[] playerNames;
    public Transform[] spawnPoints;

    public int NUMBER_OF_PLAYERS = 1;
    private int alivePlayers;

    private UI_Handler UI_HANDLER;

    // gameplay stats

    // Temporary solution, bad
    private int[] amountOfLives = { 0, 0, 0, 0 };

    void Awake()
    {
        UI_HANDLER = GameObject.FindObjectOfType<UI_Handler>();
    }

    // Use this for initialization
    void Start()
    {
        UI_HANDLER.Initialize(NUMBER_OF_PLAYERS);

        for (int i = 0; i < NUMBER_OF_PLAYERS; i++)
        {
            SpawnPlayer(i);
            amountOfLives[i] = 1;

            UI_HANDLER.SetUILives(i, amountOfLives[i].ToString());
            UI_HANDLER.SetUIName(i, playerNames[i]);
            UI_HANDLER.SetUIColor(i, playerColors[i]);
        }

        alivePlayers = NUMBER_OF_PLAYERS;
    }

    private void SpawnPlayer(int playerIndex)
    {
        GameObject playerObj = Instantiate(PREFAB_PLAYER, spawnPoints[playerIndex].position, Quaternion.identity);
        Player player = playerObj.GetComponent<Player>();

        player.Initialize(this, playerIndex);
    }

    public void RemovePlayer(int playerID, int attackerID)
    {
        // wait some time...
        if (attackerID >= 0)
            Debug.Log("Player " + attackerID + " fragged player " + playerID);

        // then...
        --amountOfLives[playerID];

        if (amountOfLives[playerID] >= 0)
            UI_HANDLER.SetUILives(playerID, amountOfLives[playerID].ToString());

        if (amountOfLives[playerID] > 0)
        {
            SpawnPlayer(playerID);
        }
        else
        {
            alivePlayers--;
            if (alivePlayers == 1)
            {
                StartCoroutine(Victory(2.0f));
            }
        }
    }

    private IEnumerator Victory(float dt)
    {
        // find the player that won -> has the most lives
        int winnerID = -1;
        int mostLives = 0;

        for (int i = 0; i < NUMBER_OF_PLAYERS; i++)
        {
            if(amountOfLives[i] > mostLives)
            {
                winnerID = i;
            }
        }

        UI_HANDLER.EnableVictory(winnerID, playerColors[winnerID]);
        yield return new WaitForSeconds(dt);
        UI_HANDLER.DisableVictory();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
