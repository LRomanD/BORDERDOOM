using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneTrigger : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject hud;
    public GameObject cutsceneCam;
    public VideoPlayer video;
    public AudioSource audio;
    public GameObject ynScript;

    private void Start()
    {
        //gameOverScript = GetComponent<GameOverScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        cutsceneCam.SetActive(true);
        thePlayer.SetActive(false);
        hud.SetActive(false);
        video.Play();
        audio.Stop();
        StartCoroutine(FinishCut());
    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(40);
        video.Pause();
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ynScript.SetActive(true);
        /*thePlayer.SetActive(true);
        hud.SetActive(true);
        cutsceneCam.SetActive(false);
        gameOverScript.GameOver("Ya, pobedil");*/
    }
}
