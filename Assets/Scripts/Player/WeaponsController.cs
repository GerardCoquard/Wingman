using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponsController : MonoBehaviour
{
    private bool reloading = false;
    private bool relB =  false;
    private bool relM = false;
    //TURRET
    public float fireRate = 0.1f;
    public float bulletForce = 1f;
    public GameObject bulletPrefab;
    public GameObject missilePrefab;
    public float maxAmmo = 100;
    public float ammoCount;
    public Text ammoDisplay;
    private float fireTime;
    private bool reloadOnB= false;
    private bool reloadOnM = false;
    private bool ok1 = true;
    private bool ok2 = true;
    private bool ok3 = true;
    private float missilesTime;
    private float bulletsTime;



    [SerializeField]
    private float reloadTurret;
    public float reloadMaxTimeTurret;

    //MISSILES 
    public float missileRate = 5f;
    public float maxMissileAmmo = 30;
    public float ammoMissileCount;
    public Text ammoMissileDisplay;
    private float missileTime;

    [SerializeField]
    private float reloadMissile;
    public float reloadMaxTimeMissile;

    //TRANSFORMS
    Transform planeBody;
    Transform planeTurret;
    Transform firePoint;
    Transform missileFirePoint;

    //SFX
    public AudioSource missileFireSFX;
    public AudioSource cannonFireSFX;

    public GameObject Balas;
    public GameObject Missiles;

    AmmunitionHud ammoBalas;
    AmmunitionHud ammoMissiles;
    // Start is called before the first frame update
    void Start()
    {
        ammoCount = maxAmmo;
        ammoMissileCount = maxMissileAmmo;
        ammoBalas = Balas.GetComponent<AmmunitionHud>();
        ammoMissiles = Missiles.GetComponent<AmmunitionHud>();
        planeBody = transform.Find("Plane");
        planeTurret = planeBody.Find("Turret");
        missileFirePoint = planeBody.Find("MissileFirePoint");
        firePoint = planeTurret.Find("FirePoint");
    }

    // Update is called once per frame
    void Update()
    {
        timer();
        timer2();
        if (ammoCount<=0)
        {
            ReloadBullets();
        }
        if (ammoMissileCount <= 0)
        {
            ReloadMissiles();
        }
        CheckReload();
        DisplayAmmo();
    }

    private void DisplayAmmo()
    {
        
        ammoBalas.SetAmmo(ammoCount);
        ammoMissiles.SetAmmo(ammoMissileCount); 
    }


    public void TurretFire()
    {
        if (ammoCount > 0 && fireTime >= fireRate && !reloadOnB)
        {
            AudioSource audioSFX = Instantiate(cannonFireSFX);
            AudioManager.PlaySFX(audioSFX);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            ammoCount = ammoCount - 1;
            fireTime = 0;
        }
    }
    public void MissileFire()
    {
        if (ammoMissileCount > 0 && missileTime >= missileRate && !reloadOnM)
        {
            AudioSource audioSFX = Instantiate(missileFireSFX);
            AudioManager.PlaySFX(audioSFX);
            GameObject missile = Instantiate(missilePrefab, firePoint.position, firePoint.rotation);
            ammoMissileCount = ammoMissileCount - 1;
            missileTime = 0;
        }

    }
    public void ReloadBullets()
    {
        relB = true;
        reloading = true;
        if (ok2)
        {
            if (ammoCount <=0)
            {
                reloadMaxTimeTurret = 2f;
            }
            else
            {
                reloadMaxTimeTurret = (maxAmmo - ammoCount) / maxAmmo * 2f;
            }
            
            ok2 = false;
        }
        ammoBalas.SetReloading(true);
        if (reloadTimerTurret() >= reloadMaxTimeTurret)
        {
            ammoCount = maxAmmo;
            reloadTurret = 0;
            reloadOnB = false;
            ammoBalas.SetReloading(false);
            reloading = false;
            relB = false;
            ok2 = true;
        }
    }
    public void ReloadMissiles()
    {
        relM = true;
        reloading = true;
        if (ok3)
        {
            if (ammoMissileCount <= 0) 
            {
                reloadMaxTimeMissile = 2f;
            }
            else
            {
                reloadMaxTimeMissile = (maxMissileAmmo - ammoMissileCount) / maxMissileAmmo * 2f;
            }
            
            ok3 = false;
        }
        ammoMissiles.SetReloading(true);
        if (reloadTimerMissile() >= reloadMaxTimeMissile)
        {
            ammoMissileCount = maxMissileAmmo;
            reloadMissile = 0;
            ammoMissiles.SetReloading(false);
            reloading = false;
            reloadOnM = false;
            relM = false;
            ok3 = true;
        }

    }

    private void CheckReload()
    {
        
        if ((Input.GetKeyDown("r") && !relB && ammoCount!=maxAmmo) || (reloadOnB &&  ammoCount != maxAmmo))
        {
            reloading = true;
            reloadOnB= true;
            ReloadBullets();
            
            
        }
        if ((Input.GetKeyDown("r") && !relM && ammoMissileCount != maxMissileAmmo) || (reloadOnM && ammoMissileCount != maxMissileAmmo))
        {
            reloading = true;
            reloadOnM = true;
            ReloadMissiles();
            

        }

    }

    private float timer()
    {
        missileTime += Time.deltaTime;
        return missileTime;
    }

    private float timer2()
    {
        fireTime += Time.deltaTime;
        return fireTime;
    }

    private float reloadTimerTurret()
    {
        reloadTurret += Time.deltaTime;
        return reloadTurret;
    }

    private float reloadTimerMissile()
    {
        reloadMissile += Time.deltaTime;
        return reloadMissile;
    }
    
}