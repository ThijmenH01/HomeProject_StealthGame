using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject PauseMenuUI;

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) PauseUI(false, 1, false);
            else PauseUI(true, 0, true);
        }
    }

    public void PauseUI(bool isActive, int timeScale, bool paused) {
        PauseMenuUI.SetActive(isActive);
        Time.timeScale = timeScale;
        isPaused = paused;
    }

    public void Resume() {
        PauseUI(false, 1, false);
    }

    public void Quit() {
        Application.Quit();
    }

    public void GoToMainMenu() {
        PauseUI(false, 1, false);
        SceneManager.LoadScene(0);
    }
}
