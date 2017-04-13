using System;
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

    private UI_Handler UI_HANDLER;

    // gameplay stats

    // Temporary solution, bad
    private int[] amountOfLives = { 3, 3, 3, 3 };

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
        }
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
            UI_HANDLER.SetLivesUI(playerID, amountOfLives[playerID].ToString());

        if (amountOfLives[playerID] > 0)
            SpawnPlayer(playerID);
    }

    // Update is called once per frame
    void Update()
    {
        // Is there only one player? -> Trigger victory for that player
    }
}
