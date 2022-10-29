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
            if (Input.GetKeyUp(KeyCode.Escape))
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
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                cutscene.StopAllCoroutines();
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
        isPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
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