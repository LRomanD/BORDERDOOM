using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSequence : MonoBehaviour
{
    public GameObject introCam;
    public GameObject playerCam;
    public GameObject hud;

    public GameObject[] Audios;

    public bool intro = false;
    void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        for (int i = 0; i < Audios.Length; i++) Audios[i].SetActive(false);
        introCam.SetActive(true);
        playerCam.SetActive(false);
        hud.SetActive(false);
        intro = true;
        StartCoroutine(FinishCut());
        /*this.gameObject.GetComponent<BoxCollider>().enabled = false;
        introCam.SetActive(true);
        playerCam.SetActive(false);
        hud.SetActive(false);
        intro = true;
        StartCoroutine(FinishCut());*/
    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(15);
        playerCam.SetActive(true);
        for (int i = 0; i < Audios.Length; i++) Audios[i].SetActive(true);
        hud.SetActive(true);
        introCam.SetActive(false);
        intro = false;
        /*yield return new WaitForSeconds(15);
        playerCam.SetActive(true);
        hud.SetActive(true);
        introCam.SetActive(false);
        intro = false;*/
    }
}