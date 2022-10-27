using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] TMP_Text hp, ammo, reload_timer;
    //public float health = 100;
    [SerializeField] Projectiles projectiles;
    [SerializeField] PlayerController player;
    [SerializeField] EnemyKilling enemyKilling;


    //public static bool isHidden = false;
    //public GameObject scriptUI;

    private float timer;


    private void Start()
    {
        //enemyKilling = GetComponent<EnemyKilling>();
    }

    void Update()
    {

        hp.text = player.hp.ToString();
        //hp.text = enemyKilling.health.ToString();
        ammo.text = string.Format("{0} / {1}", projectiles.bulletsRemain, projectiles.magazineSize);
        if (projectiles.isReloading)
        {
            //reload_timer.text = ((int)timer+1).ToString();
            reload_timer.text = string.Format("{0:F2}", timer);
            timer -= Time.deltaTime;
        }
        else
        {
            reload_timer.text = "";
            timer = projectiles.reloadTime;
        }
    }

    /*public void Show()
    {
        scriptUI.SetActive(false);
        isHidden = false;
    }

    public void Hide()
    {
        scriptUI.SetActive(true);
        isHidden = true;
    }*/
}
