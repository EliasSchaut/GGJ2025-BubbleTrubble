﻿using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    
    private List<GameObject> _players = new List<GameObject>();
    [SerializeField] private GameObject[] playerSpawns;
    private int activePlayers = 0;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerJoin()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if(!player.GetComponent<Player>().IsPlayerActive())
            {
                player.GetComponent<Player>().SetPlayerActive(true, playerSpawns[activePlayers].transform);
                _players.Add(player);
                
                activePlayers++;
                GameManager.Instance.PlayerJoined(activePlayers);
                Debug.Log("Player " + activePlayers + " joined!");
            }
        }
    }
}
