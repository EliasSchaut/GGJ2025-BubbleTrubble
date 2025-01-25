using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    [SerializeField] private GameObject uiManagerGameObject;

    private int _lives = 3;
    private bool _isPaused = false;
    
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject gameManagerObj = new GameObject("GameManager");
                    _instance = gameManagerObj.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void ExitGame()
    {
        print("quit");
        Application.Quit();
    }
    
    public bool IsGamePaused()
    {
        return _isPaused;
    }
    
    public void OnGameOver()
    {
        
        Time.timeScale = 0.01f;
        uiManagerGameObject.GetComponent<UIManager>().ActivateGameOverPanel();
    }

    public void OnWin()
    {
        Time.timeScale = 0.01f;
        //OpenWinUI();
    }

    public void LoseLife()
    {
        _lives--;
        if (_lives <= 0)
        {
            uiManagerGameObject.GetComponent<UIManager>().SetLives(_lives);
            OnGameOver();
        }
    }
    
    public void EnemyKilled(int enemySelfWorth)
    {
        
    }
    
    public void TogglePause()
    {
        if (_isPaused)
        {
            _isPaused = false;
            Time.timeScale = 1f;
        }
        else
        {
            _isPaused = true;
            Time.timeScale = 0.0f;
        }
    }

    public void RestartGame()
    {
        
    }

    public void PlayerJoined(int playerNumber)
    {
        uiManagerGameObject.GetComponent<UIManager>().SetPlayerConnected(playerNumber);
    }
    
}
