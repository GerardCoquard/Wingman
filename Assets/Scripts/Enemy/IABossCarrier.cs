using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABossCarrier : MonoBehaviour
{
    EnemyController controller;
    
    public GameObject bulletPrefab;
    public GameObject missilePrefab;
    public GameObject vidaBoss;
    public GameObject Player;

    public bool first = true;
    BossBars bossbar;
    public GameObject avionPrefab;
    public GameObject barreraAbrir;
    public GameObject barreraAbrir2;

    AudioSource audioIntro;

    public float safetyDistance;
    private bool changeRoute = false;
    private float distance;
    private float rotateSpeed;
    public float rotationSpeedTurret;
    private float speed;
    public float maxLife;

    public bool bossMissileEvent = false;

    Transform turretTarget;
    Transform target;
    Rigidbody2D rb;

    //MISSILE SYSTEM
    Transform BatteryI1;
    Transform BatteryI2;
    Transform BatteryI3;
    Transform BatteryI4;
    Transform BatteryI5;
    Transform BatteryI6;
    Transform BatteryI7;

    Transform BatteryD1;
    Transform BatteryD2;
    Transform BatteryD3;
    Transform BatteryD4;
    Transform BatteryD5;
    Transform BatteryD6;
    Transform BatteryD7;


    public bool salvo1 = false;
    public bool salvo2 = false;

    public float missileRate;
    public float missileTime = 0;

    public float missileEventTime;

    //TURRET SYSTEM
    Transform firePoint11;
    Transform firePoint12;

    Transform firePoint21;
    Transform firePoint22;

    Transform firePoint31;
    Transform firePoint32;

    Transform firePoint41;
    Transform firePoint42;

    Transform firePoint51;
    Transform firePoint52;

    Transform firePoint61;
    Transform firePoint62;

    Transform firePoint71;
    Transform firePoint72;

    Transform bossTurret1;
    Transform bossTurret2;
    Transform bossTurret3;
    Transform bossTurret4;
    Transform bossTurret5;
    Transform bossTurret6;
    Transform bossTurret7;

    Transform Spawner;
    public float timerSpawner;
    public float timerLimit;

    public float fireRate;
    public float fireRateMin;
    public float fireRateMax;
    public float fireTime = 0;
    public float bulletForce = 1f;


    PlayerController playerContr;
    //SFX
    public AudioSource missileLaunchSFX;
    public AudioSource flakBatterySFX;
    public AudioSource JaggerIntro;

    void Start()
    {
        bossbar = vidaBoss.GetComponent<BossBars>();
        controller = GetComponent<EnemyController>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        timerSpawner = timerLimit;
        bossTurret1 = transform.Find("bossTurret1");
        bossTurret2 = transform.Find("bossTurret2");
        bossTurret3 = transform.Find("bossTurret3");
        bossTurret4 = transform.Find("bossTurret4");
        bossTurret5 = transform.Find("bossTurret5");
        bossTurret6 = transform.Find("bossTurret6");
        bossTurret7 = transform.Find("bossTurret7");


        firePoint11 = bossTurret1.Find("FirePoint1");
        firePoint12 = bossTurret1.Find("FirePoint2");

        firePoint21 = bossTurret2.Find("FirePoint1");
        firePoint22 = bossTurret2.Find("FirePoint2");

        firePoint31 = bossTurret3.Find("FirePoint1");
        firePoint32 = bossTurret3.Find("FirePoint2");

        firePoint41 = bossTurret4.Find("FirePoint1");
        firePoint42 = bossTurret4.Find("FirePoint2");

        firePoint51 = bossTurret5.Find("FirePoint1");
        firePoint52 = bossTurret5.Find("FirePoint2");

        firePoint61 = bossTurret6.Find("FirePoint1");
        firePoint62 = bossTurret6.Find("FirePoint2");

        firePoint71 = bossTurret7.Find("FirePoint1");
        firePoint72 = bossTurret7.Find("FirePoint2");

        BatteryI1 = transform.Find("BatteryI1");
        BatteryI2 = transform.Find("BatteryI2");
        BatteryI3 = transform.Find("BatteryI3");
        BatteryI4 = transform.Find("BatteryI4");
        BatteryI5 = transform.Find("BatteryI5");
        BatteryI6 = transform.Find("BatteryI6");
        BatteryI7 = transform.Find("BatteryI7");

        BatteryD1 = transform.Find("BatteryD1");
        BatteryD2 = transform.Find("BatteryD2");
        BatteryD3 = transform.Find("BatteryD3");
        BatteryD4 = transform.Find("BatteryD4");
        BatteryD5 = transform.Find("BatteryD5");
        BatteryD6 = transform.Find("BatteryD6");
        BatteryD7 = transform.Find("BatteryD7");

        Spawner = transform.Find("PosicionSpawn");

        playerContr = Player.GetComponent<PlayerController>();
        checkTarget();
        getData();

        bossbar.SetMaxHealth(maxLife);

    }

    void FixedUpdate()
    {
        checkTarget();
        movement();
        if (!(PlayerController.instance.GetPlayerDead()))
        {
            checkCombat();
        }
        
        checkDistance();
        timer();
        timer2();
        timer3();
        BossStatus();
        bossbar.SetHealth(controller.getLife());
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, safetyDistance);
    }
    private void SpawnerAviones()
    {
        if (timerSpawner<=0)
        {
            GameObject avion = Instantiate(avionPrefab, Spawner.position, Spawner.rotation);
            EnemyController enemycont = avion.GetComponent<EnemyController>();
            enemycont.SetTarget(controller.GetTarget());
            enemycont.SetNextPoint(controller.GetNextPoint());
            timerSpawner = timerLimit;
        }

        timerSpawner -= Time.deltaTime;
    }
    public void IntroMusic()
    {
        audioIntro = Instantiate(JaggerIntro);
        AudioManager.PlaySFX(audioIntro);
        
        
    }
    private void RotateTurret()
    {
        Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward, turretTarget.position - bossTurret2.position);

        TurretRotation(bossTurret1, desiredRotation);
        FirePointRotation(firePoint11, desiredRotation);
        FirePointRotation(firePoint12, desiredRotation);

        TurretRotation(bossTurret2, desiredRotation);
        FirePointRotation(firePoint21, desiredRotation);
        FirePointRotation(firePoint22, desiredRotation);

        TurretRotation(bossTurret3, desiredRotation);
        FirePointRotation(firePoint31, desiredRotation);
        FirePointRotation(firePoint32, desiredRotation);

        TurretRotation(bossTurret4, desiredRotation);
        FirePointRotation(firePoint41, desiredRotation);
        FirePointRotation(firePoint42, desiredRotation);

        TurretRotation(bossTurret5, desiredRotation);
        FirePointRotation(firePoint51, desiredRotation);
        FirePointRotation(firePoint52, desiredRotation);

        TurretRotation(bossTurret6, desiredRotation);
        FirePointRotation(firePoint61, desiredRotation);
        FirePointRotation(firePoint62, desiredRotation);

        TurretRotation(bossTurret7, desiredRotation);
        FirePointRotation(firePoint71, desiredRotation);
        FirePointRotation(firePoint72, desiredRotation);
    }

    private void TurretRotation(Transform turret, Quaternion rotation)
    {
        rotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z + 180);
        turret.rotation = Quaternion.RotateTowards(turret.rotation, rotation, rotationSpeedTurret * Time.deltaTime);
    }

    private void FirePointRotation(Transform firePoint, Quaternion rotation)
    {
        rotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z);
        firePoint.rotation = Quaternion.RotateTowards(firePoint.rotation, rotation, rotationSpeedTurret * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "PlayerBullet")
        {
            controller.reduceLife(20);
            getData();
        }
        if (collision.transform.tag == "PlayerMissile")
        {
            controller.reduceLife(80);
            getData();
        }
        if (collision.transform.tag == "Map")
        {
            controller.Dead();
            getData();
        }

    }

    private void movement()
    {
        if (!changeRoute)
        {
            Vector2 direction = (Vector2)target.position - rb.position;
            direction.Normalize();
            float rotate = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotate * rotateSpeed;
            rb.velocity = transform.up * speed;
        }
        else
        {
            Vector2 direction = (Vector2)target.position - rb.position;
            direction.Normalize();
            float rotate = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = +rotate * rotateSpeed;
            rb.velocity = transform.up * speed;
        }
    }

    private void checkTarget()
    {
        target = controller.getNextPoint();
        turretTarget = controller.getTargetPlayer();
    }

    private void checkCombat()
    {
        if (controller.getCombat())
        {
            RotateTurret();
            fireTurrets();
            SpawnerAviones();

            if (bossMissileEvent)
            {
                fireMissileSalvo();
            }

        }
    }

    private void checkDistance()
    {
        if (controller.getDistance() < safetyDistance)
        {
            changeRoute = true;
        }
        else changeRoute = false;
    }

    private void fireTurrets()
    {
        if (fireTime >= fireRate)
        {
            AudioSource audioSFX = Instantiate(flakBatterySFX);
            AudioManager.PlaySFX(audioSFX);
            fireFirePoint(firePoint11);
            fireFirePoint(firePoint12);

            fireFirePoint(firePoint21);
            fireFirePoint(firePoint22);

            fireFirePoint(firePoint31);
            fireFirePoint(firePoint32);

            fireFirePoint(firePoint41);
            fireFirePoint(firePoint42);

            fireFirePoint(firePoint51);
            fireFirePoint(firePoint52);

            fireFirePoint(firePoint61);
            fireFirePoint(firePoint62);

            fireFirePoint(firePoint71);
            fireFirePoint(firePoint72);

            fireTime = 0;
            fireRate = Random.Range(fireRateMin, fireRateMax);
        }
    }

    private void fireMissileSalvo()
    {
        if (salvo1 && missileTime >= missileRate)
        {
            AudioSource audioSFX = Instantiate(missileLaunchSFX);
            AudioManager.PlaySFX(audioSFX);
            Instantiate(missilePrefab, BatteryI1.position, BatteryI1.rotation);
            Instantiate(missilePrefab, BatteryD2.position, BatteryD2.rotation);
            Instantiate(missilePrefab, BatteryI3.position, BatteryI3.rotation);
            Instantiate(missilePrefab, BatteryD4.position, BatteryD4.rotation);
            Instantiate(missilePrefab, BatteryI5.position, BatteryI5.rotation);
            Instantiate(missilePrefab, BatteryD6.position, BatteryD6.rotation);
            Instantiate(missilePrefab, BatteryI7.position, BatteryI7.rotation);


            missileTime = 0;
        }
        
        if (salvo2 && missileTime >= missileRate)
        {
            AudioSource audioSFX = Instantiate(missileLaunchSFX);
            AudioManager.PlaySFX(audioSFX);
            Instantiate(missilePrefab, BatteryD1.position, BatteryD1.rotation);
            Instantiate(missilePrefab, BatteryI2.position, BatteryI2.rotation);
            Instantiate(missilePrefab, BatteryD3.position, BatteryD3.rotation);
            Instantiate(missilePrefab, BatteryI4.position, BatteryI4.rotation);
            Instantiate(missilePrefab, BatteryD5.position, BatteryD5.rotation);
            Instantiate(missilePrefab, BatteryI6.position, BatteryI6.rotation);
            Instantiate(missilePrefab, BatteryD7.position, BatteryD7.rotation);


            missileTime = 0;
        }
    }
    private void fireFirePoint(Transform firepoint)
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firepoint.up * bulletForce, ForceMode2D.Impulse);
    }
    
    private float timer()
    {
        fireTime += Time.deltaTime;
        return fireTime;
    }

    private void timer2()
    {
        if (bossMissileEvent)
        {
            
            missileEventTime += Time.deltaTime;
            if (missileEventTime > 0)
            {
                salvo1 = true;
                salvo2 = false;
            }

            if (missileEventTime > 5)
            {
                salvo2 = true;
                salvo1 = false;
            }

            if (missileEventTime > 10)
            {
                missileEventTime = 0;
            }
        }
    }

    private float timer3()
    {
        missileTime += Time.deltaTime;
        return missileTime;
    }

    private void BossStatus()
    {
        if(controller.getLife() <= 1500)
        {
            bossMissileEvent = true;
            
            
        }
        if (controller.getLife() <= 0)
        {
            if (first)
            {
                bossbar.destroyBossBar();
                barreraAbrir.SetActive(false);
                barreraAbrir2.SetActive(false);
                audioIntro.Stop();
                if (PlayerPrefs.GetInt("estado") + 1 > PlayerPrefs.GetInt("guardado"))
                {
                    PlayerPrefs.SetInt("guardado", 2);
                }
                PlayerPrefs.SetInt("estado", 2);
                playerContr.RegenOn();
                first = false;
            }
            
        }
    }

    private void getData()
    {
        distance = controller.getDistance();
        rotateSpeed = controller.getRotateSpeed();
        speed = controller.getSpeed();
        maxLife = controller.getLife();
    }
}
