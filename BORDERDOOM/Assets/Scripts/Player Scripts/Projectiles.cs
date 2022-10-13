using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Projectiles : MonoBehaviour
{
    public GameObject bullet;

    public float shootForce, upwardForce;


    public float timebetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletPerTap;
    public bool allowButtonHold;

    int bulletsLeft, bulletsShot;
    public int bulletsRemain
    {
        get { return bulletsLeft; }
    }

    bool shooting, readyToShoot, reloading;

    public bool isReloading
    {
        get { return reloading; }
    }

    public Camera fpsCam;
    public Transform attackPoint;

    //графика

    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;



    public bool allowInvoke = true;

    private void Awake()
    {
        //убеждение, что магазин полон
        bulletsLeft = magazineSize;
        readyToShoot = true;

    }//Awake

    private void Update()
    {
        MyInput();

        //установление дисплея патронов, если таковые присутствуют
        if (ammunitionDisplay != null) ammunitionDisplay.SetText(bulletsLeft / bulletPerTap + " / " + magazineSize / bulletPerTap);
    }//Update

    private void MyInput() 
    {
        //проверка, можно ли держать кнопку выстрела
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);//можно
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);//нельзя

        //стрельба
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;

            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();
    }//MyInput

    private void Shoot()
    {
        readyToShoot = false;

        //нахождеие точного попадания с помощью raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));//лучь сквозь центр экрана
        RaycastHit hit;

        //проверка, если луч попал во что-нибудь
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit)) targetPoint = hit.point;
        else targetPoint = ray.GetPoint(75);//если стреляешь далеко (например в небо)

        //расчёт расстояния от точки атаки до точки цели
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //расчёт разброса
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //расчёт нового расстояния с разбросом
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        //инициализация пули/снаряда
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);//заспавненная пуля будет храниться в этом объекте

        //поворот пули в сторону, в которую стреляем
        currentBullet.transform.forward = directionWithoutSpread.normalized;

        //добавление силы пуле
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);//upwardForce - для гранат

        //огонёк из дула, если есть
        if (muzzleFlash != null) 
        { 
            GameObject effect = Instantiate(muzzleFlash, attackPoint.position, transform.rotation);
            Destroy(effect, 0.1f);
        }

        

        bulletsLeft--;
        bulletsShot++;

        //вызывает ResetShot (если уже не вызван)
        if (allowInvoke)
        {
            Invoke("ResetShot", timebetweenShooting);//стрельба со скоростью timebetweenShooting
            allowInvoke = false;
        }

        if (bulletsShot < bulletPerTap && bulletsLeft > 0) Invoke("Shoot", timeBetweenShots);







    }//Shoot

    


    private void ResetShot()
    {
        //позволяет снова стрелять и вызывать
        readyToShoot = true;
        allowInvoke = true;
    }//ResetShot

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

}//class
