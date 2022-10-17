using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] TMP_Text hp, ammo, reload_timer;
    [SerializeField] Projectiles projectiles;
    [SerializeField] PlayerController player;

    private Image health_Stats, stamina_Stats;

    private float timer;

    void Update()
    {
        hp.text = player.hp.ToString();
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

    public void Display_HealthStats(float healthValue)
    {
        healthValue /= 100f;
        health_Stats.fillAmount = healthValue;
    }

    public void Display_StaminaStats(float staminaValue)
    {
        staminaValue /= 100f;
        stamina_Stats.fillAmount = staminaValue;
    }
}
