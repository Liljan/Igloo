﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public GameObject PREFAB_PLAYER;

    public string[] playerNames;
    public Transform[] spawnPoints;

    private Color skinColor = new Color(1.0f, 212.0f / 255.0f, 168.0f / 255.0f, 1.0f);
    private Color pantsColor = new Color(72F / 255F, 62F / 255F, 62F / 255F, 1F);
    public Color[] playerColors;

    public int NUMBER_OF_PLAYERS = 1;
    private int alivePlayers;

    private UI_Handler UI_HANDLER;

    private bool isPaused = false;

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

        PaletteSwap palette = playerObj.GetComponent<PaletteSwap>();
        palette.SetColors(skinColor, playerColors[playerIndex], pantsColor, 0.1f);
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
                StartCoroutine(Victory(4.0f));
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
            if (amountOfLives[i] > mostLives)
            {
                winnerID = i;
            }
        }

        if (winnerID != -1)
            UI_HANDLER.EnableVictory(playerNames[winnerID], playerColors[winnerID]);
        else
            UI_HANDLER.EnableTie();

        yield return new WaitForSeconds(dt);
        UI_HANDLER.DisableVictory();

        // Restart, deprrecated at the moment
        Application.LoadLevel(Application.loadedLevel);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            UI_HANDLER.ShowPause(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            UI_HANDLER.ShowPause(false);
            Time.timeScale = 1.0f;
        }
    }
}
