using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    
    private GameObject[] _players; 
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
        Debug.Log("new player joined");
        activePlayers++;
    }
}
