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

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < NUMBER_OF_PLAYERS; i++)
        {
            SpawnPlayer(i);
            InitGUI(i);
        }
    }

    private void SpawnPlayer(int playerIndex)
    {
        GameObject playerObj = Instantiate(PREFAB_PLAYERS[playerIndex], spawnPoints[playerIndex].position, Quaternion.identity);
    }

    private void InitGUI(int playerIndex)
    {
        playerUI[playerIndex].Initiate(playerNames[playerIndex], 10, 20, 6, playerColors[playerIndex]);
    }

    public void RemoveLife(int playerIndex)
    {

    }

    public void RemovePlayer(int playerID)
    {
        // wait some time...

        // then...
        SpawnPlayer(playerID);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
