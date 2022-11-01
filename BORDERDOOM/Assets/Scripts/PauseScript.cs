using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public SceneSequence cutscene;

    private bool isPaused = false;

    void Update()
    {
        if (cutscene.intro == false)
        {
            if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.P))
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
        }
        else
        {
            if (Input.anyKeyDown)
            {
                cutscene.StopAllCoroutines();
                for (int i = 0; i < cutscene.Audios.Length; i++) cutscene.Audios[i].SetActive(true);
                cutscene.playerCam.SetActive(true);
                cutscene.hud.SetActive(true);
                cutscene.introCam.SetActive(false);
                cutscene.intro = false;
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
    }

    public void ExitToMainmenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}