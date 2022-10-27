using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject gameOver;
    [SerializeField] PlayerController player;
    [SerializeField] CameraController camera;
    [SerializeField] Projectiles projectiles;
    [SerializeField] PauseScript pause;
    [SerializeField] TMP_Text crosshair, label;

    string text = "You're ded";

    public bool gameIsOver = false;
    private void Update()
    {
        if (player.hp <= 0) GameOver(text);
    }
    public void GameOver(string text)
    {
        label.text = text;
        gameIsOver = true;
        camera.enabled = false;
        projectiles.enabled = false;
        pause.enabled = false;
        crosshair.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartButton()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene_A");
    }

    public void ExitToMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
