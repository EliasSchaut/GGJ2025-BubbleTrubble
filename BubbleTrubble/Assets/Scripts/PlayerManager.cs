using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    
    private List<GameObject> _players = new List<GameObject>();
    [SerializeField] private GameObject[] playerSpawns;
    private int activePlayers = 0;

    public void OnPlayerJoin()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if(!player.GetComponent<Player>().IsPlayerActive())
            {
                player.GetComponent<Player>().SetPlayerActive(true, playerSpawns[activePlayers].transform);
                var bubbleManager = GameObject.FindGameObjectWithTag("BubbleManager");
                player.GetComponent<Player>().SetBubbleManager(bubbleManager);
                _players.Add(player);
                
                activePlayers++;
                GameManager.Instance.PlayerJoined(activePlayers);
                Debug.Log("Player " + activePlayers + " joined!");
            }
        }
    }
}
