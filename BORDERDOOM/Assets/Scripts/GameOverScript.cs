using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject gameOver;
    public AudioSource audio;
    public AudioClip clip;
    [SerializeField] PlayerController player;
    [SerializeField] CameraController camera;
    [SerializeField] Projectiles projectiles;
    [SerializeField] PauseScript pause;
    [SerializeField] TMP_Text crosshair, label;

    string str = "You're ded";

    public bool gameIsOver = false;
    private void Update()
    {
        if (player.hp <= 0) GameOver(str);
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
        if (text == "Ya pobedil")
        {
            audio.clip = clip;
            audio.loop = false;
            audio.Play();
        }
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
