using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("GameObjects")]
    public GameObject gameLoseUI;
    public GameObject gameWinUI;
    [Header("Text")]
    public Text WinCountText;
    public Text highScoreText;
    public Text balanceTempText;
    public Text balancePermText;
    public Player player;
    public int highScore;

    bool gameIsOver;
    bool gameIsStartable;

    void Start() {
        instance = this;
        Player.balancePerm = PlayerPrefs.GetInt("balancePerm");
        balancePermText.text = Player.balancePerm.ToString();
        player.winCount = PlayerPrefs.GetInt("winCount");
        highScore = PlayerPrefs.GetInt("highScore");
        highScoreText.text = highScore.ToString();
        Guard.OnGuardHasSpottedPlayer += ShowGameLoseUI;
        FindObjectOfType<Player>().OnReachedExit += ShowGameWinUI;
    }

    void Update() {
        if (gameIsOver) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene(1);
            }
        }
    }

    void ShowGameWinUI() {
        player.winCount++;
        Player.balancePerm += 2; //Win Reward
        Player.balancePerm += Player.balanceTemp;
        Player.balanceTemp = 0;
        PlayerPrefs.SetInt("balancePerm", Player.balancePerm);
        PlayerPrefs.SetInt("balanceTemp", Player.balanceTemp);
        PlayerPrefs.SetInt("winCount", player.winCount);

        balancePermText.text = Player.balancePerm.ToString();
        WinCountText.text = player.winCount.ToString();
        OnGameOver(gameWinUI);
    }

    void ShowGameLoseUI() {
        player.winCount = 0;
        Player.balanceTemp = 0;
        PlayerPrefs.SetInt("balanceTemp", Player.balanceTemp);
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

    private void OnDestroy() {
        Guard.OnGuardHasSpottedPlayer -= ShowGameLoseUI;
    }
}
