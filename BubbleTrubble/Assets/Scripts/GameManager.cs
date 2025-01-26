using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    [SerializeField] private GameObject uiManagerGameObject;
    
    [SerializeField] private InputActionReference playAgainActionReference;
    [SerializeField] private InputActionReference exitGameActionReference;


    private int _lives = 3;
    private bool _isPaused = false;

    private int totalScore = 0;
    private int currentWave = 0;
    
    private bool gameOver = false;
    
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameManager>();
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
        uiManagerGameObject.GetComponent<UIManager>().ActivateGameOverPanel();
        gameOver = true;
    }

    public void OnWin()
    {
        Time.timeScale = 0.01f;
        //OpenWinUI();
    }

    public void LoseLife()
    {
        _lives--;
        uiManagerGameObject.GetComponent<UIManager>().SetLives(_lives);

        if (_lives <= 0)
        {
            OnGameOver();
        }
    }
    
    public void EnemyKilled(int enemySelfWorth)
    {
        totalScore += enemySelfWorth;
        uiManagerGameObject.GetComponent<UIManager>().SetScore(totalScore);
    }

    public void SetWave(int wave)
    {
        currentWave = wave;
        uiManagerGameObject.GetComponent<UIManager>().SetWave(currentWave);
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
        Debug.Log("RestartGame");
        gameOver = false;
        
        
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void PlayerJoined(int playerNumber)
    {
        uiManagerGameObject.GetComponent<UIManager>().SetPlayerConnected(playerNumber);
    }
    
    public void OnPlayAgainClick(InputAction.CallbackContext context)
    {
        if (gameOver) RestartGame();
    } 
    
    public void OnExitGameClick(InputAction.CallbackContext context)
    {
        if (gameOver) ExitGame();
    } 
    
    
    private void OnEnable()
    {
        playAgainActionReference.action.performed += OnPlayAgainClick;
        playAgainActionReference.action.Enable();
        
        exitGameActionReference.action.performed += OnExitGameClick;
        exitGameActionReference.action.Enable();
    }

    private void OnDisable()
    {
        playAgainActionReference.action.performed -= OnPlayAgainClick;
        playAgainActionReference.action.Disable();
        
        exitGameActionReference.action.performed -= OnExitGameClick;
        exitGameActionReference.action.Disable();
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }
}
