using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class YNScript : MonoBehaviour
{
    public GameObject menu;
    public GameObject cutscene_cam;
    public GameObject hud;
    public GameObject continue_cam;
    public GameObject player_cam;

    public VideoPlayer video;

    [SerializeField] GameOverScript gameOverScript;
    private void Awake()
    {
        Time.timeScale = 1f;
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        continue_cam.SetActive(true);
        cutscene_cam.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        video.Play();
        menu.SetActive(false);
        StartCoroutine(ShowMushGoOn());
    }

    public void Done()
    {
        Time.timeScale = 1f;
        player_cam.SetActive(true);
        hud.SetActive(true);
        cutscene_cam.SetActive(false);
        continue_cam.SetActive(false);
        menu.SetActive(false);
        gameOverScript.GameOver("Ya pobedil");
    }

    IEnumerator ShowMushGoOn()
    {
        yield return new WaitForSeconds(61);
        Done();
        /*player_cam.SetActive(true);
        hud.SetActive(true);
        continue_cam.SetActive(false);
        gameOverScript.GameOver("Ya, pobedil");*/
        /*yield return new WaitForSeconds(15);
        playerCam.SetActive(true);
        hud.SetActive(true);
        introCam.SetActive(false);
        intro = false;*/
    }
}
