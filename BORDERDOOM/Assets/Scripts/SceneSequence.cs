using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSequence : MonoBehaviour
{
    public GameObject introCam;
    public GameObject playerCam;
    //public GameObject outroCam;

    

    void Start()
    {
        StartCoroutine(TheSequence());
        
    }

    

    IEnumerator TheSequence ()
    {
        yield return new WaitForSeconds(26);
        introCam.SetActive(true);
        playerCam.SetActive(false);

        yield return new WaitForSeconds(0);
        playerCam.SetActive(true);
        introCam.SetActive(false);
    }

}
