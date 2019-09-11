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
    public Text balancePermText;
    public Player player;
    public int highScore;

    bool gameIsOver;
    bool gameIsStartable;

    void Start() {
        instance = this;
        Player.balancePerm = PlayerPrefs.GetInt("balancePerm");
        balancePermText.text = Player.balancePerm.ToString();
        Guard.OnGuardHasSpottedPlayer += ShowGameLoseUI;
        FindObjectOfType<Player>().OnReachedExit += ShowGameWinUI;
    }

    void Update() {
        if (gameIsOver) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene(1);
            }
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            Player.balancePerm += 50;
            balancePermText.text = Player.balancePerm.ToString();
            PlayerPrefs.SetInt("balancePerm", Player.balancePerm);
        }
    }

    void ShowGameWinUI() {
        Player.balancePerm += 5; //Win Reward
        PlayerPrefs.SetInt("balancePerm", Player.balancePerm);
        balancePermText.text = Player.balancePerm.ToString();
        OnGameOver(gameWinUI);
    }

    void ShowGameLoseUI() {
        OnGameOver(gameLoseUI);
    }

    void OnGameOver(GameObject gameOverUI) {
        gameOverUI.SetActive(true);
        gameIsOver = true;
        Guard.OnGuardHasSpottedPlayer -= ShowGameLoseUI;
        FindObjectOfType<Player>().OnReachedExit -= ShowGameWinUI;
    }

    private void OnDestroy() {
        Guard.OnGuardHasSpottedPlayer -= ShowGameLoseUI;
    }
}
