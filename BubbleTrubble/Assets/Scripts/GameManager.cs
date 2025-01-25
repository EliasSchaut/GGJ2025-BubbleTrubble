using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private bool _isPaused = false;

    private int _lives = 3;
    
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        //OpenDeathUI();
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
            OnGameOver();
        }
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
            Time.timeScale = 0.01f;
        }

    }
    
}
