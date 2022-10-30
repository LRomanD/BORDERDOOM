using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject hud;
    public GameObject cutsceneCam;

    [SerializeField] GameOverScript gameOverScript;

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
        StartCoroutine(FinishCut());


    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(30);
        thePlayer.SetActive(true);
        hud.SetActive(true);
        cutsceneCam.SetActive(false);
        gameOverScript.GameOver("Ya, pobedil");
    }
}
