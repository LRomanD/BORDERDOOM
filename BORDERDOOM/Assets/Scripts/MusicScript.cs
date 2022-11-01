using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip clip;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        audio.spatialBlend = 0.0f;
        //audio.Stop();
        audio.clip = clip;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        Debug.Log(other.tag);
        audio.Play();
        /* StartCoroutine(FinishCut());
     }

     IEnumerator FinishCut()
     {
         yield return new WaitForSeconds(30);
     */
    }
}