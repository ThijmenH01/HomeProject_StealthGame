using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public GameObject gameLoseUI;
    public GameObject gameWinUI;
    public Text WinCountText;
    public Text highScoreText;
    public Player player;
    public int highScore;

    bool gameIsOver;
    bool gameIsStartable;

    void Start() {
        player.winCount = PlayerPrefs.GetInt("winCount");
        highScore = PlayerPrefs.GetInt("highScore");
        WinCountText.text = player.winCount.ToString();
        highScoreText.text = highScore.ToString();
        Guard.OnGuardHasSpottedPlayer += ShowGameLoseUI;
        FindObjectOfType<Player>().OnReachedExit += ShowGameWinUI;
    }

    void Update() {
        if (gameIsOver) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene(0);
            }
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            player.winCount++;
            Debug.Log(player.winCount.ToString());
        }
    }

    void ShowGameWinUI() {
        player.winCount++;
        PlayerPrefs.SetInt("winCount", player.winCount);
        WinCountText.text = player.winCount.ToString();
        OnGameOver(gameWinUI);
    }

    void ShowGameLoseUI() {
        player.winCount = 0;
        PlayerPrefs.SetInt("winCount", player.winCount);
        WinCountText.text = player.winCount.ToString();
        OnGameOver(gameLoseUI);
    }

    void OnGameOver(GameObject gameOverUI) {
        gameOverUI.SetActive(true);
        gameIsOver = true;
        Guard.OnGuardHasSpottedPlayer -= ShowGameLoseUI;
        FindObjectOfType<Player>().OnReachedExit -= ShowGameWinUI;
        SetHighScore();
    }

    void SetHighScore() {
        if(player.winCount > highScore) {
            highScore = player.winCount;
            PlayerPrefs.SetInt("highScore", highScore);
            highScoreText.text = highScore.ToString();
        }
    }
}
