using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text playerConnectedText;
    [SerializeField] private GameObject gameOverPanel;

    public void OnPlayAgain()
    {
        GameManager.Instance.RestartGame();
    }

    public void OnQuitGame()
    {
        GameManager.Instance.ExitGame();
    }

    public void SetLives(int lives)
    {
        livesText.text = lives.ToString() + " Lives";
    }

    public void SetPlayerConnected(int connectedPlayers)
    {
        playerConnectedText.text = connectedPlayers.ToString() + "/4 Players connected - Press A to join!";

        if (connectedPlayers >= 4)
        {
            playerConnectedText.text = "";
        }
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString() + " Points";
    }

    public void SetWave(int wave)
    {
        waveText.text = "Wave " + wave.ToString();
    }

    public void ActivateGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}
