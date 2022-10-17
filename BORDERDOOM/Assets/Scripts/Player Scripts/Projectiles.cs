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

    public float damage = 20f;

    int bulletsLeft, bulletsShot;

    GameObject currentBullet;
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

    public GameObject muzzleFlash;
    //public TextMeshProUGUI ammunitionDisplay;



    public bool allowInvoke = true;

    private void Start()
    {
        Invoke("DestroyProjectile", 5f);
    }

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();
        //if (ammunitionDisplay != null) ammunitionDisplay.SetText(bulletsLeft / bulletPerTap + " / " + magazineSize / bulletPerTap);
    }

    private void MyInput() 
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();
    }

    private void Shoot()
    {
        readyToShoot = false;

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        { 
            if(hit.transform.tag == Tags.ENEMY_TAG)
            {
                /*enemyKilling = */hit.transform.GetComponent<EnemyKilling>().ApplyDamage(damage);
                //enemyKilling.health--;
                Debug.Log("Est probitie");
            }
            targetPoint = hit.point; 
        }
        else targetPoint = ray.GetPoint(75);

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        currentBullet.transform.forward = directionWithoutSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        if (muzzleFlash != null) 
        { 
            GameObject effect = Instantiate(muzzleFlash, attackPoint.position, transform.rotation);
            Destroy(effect, 0.1f);
        }

        

        bulletsLeft--;
        bulletsShot++;

        if (allowInvoke)
        {
            Invoke("ResetShot", timebetweenShooting);
            allowInvoke = false;
        }

        if (bulletsShot < bulletPerTap && bulletsLeft > 0) Invoke("Shoot", timeBetweenShots);
    }

    void DestroyProjectile()
    {
        Destroy(currentBullet);
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

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

}
