using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABossFinal : MonoBehaviour
{
    EnemyController controller;
    
    public GameObject bulletPrefab;
    public GameObject missilePrefab;
    public GameObject fireExplosion;
    public GameObject pasuyopciones;
    public GameObject fade;
    public GameObject vidaBoss;
    
    BossBars bossbar;
    public GameObject motor1;
    public GameObject motor2;
    public GameObject motor3;
    public GameObject FinishHim;

    public bool ok1 = true;
    private float timetoend = 0;
    private bool startimer = false;
    private bool changeRoute = false;
    private float distance;
    private float rotateSpeed;
    public float rotationSpeedTurret;
    private float speed;
    private float maxLife;
    public float safetyDistance;
    private int state = 1;



    Transform turretTarget;
    Rigidbody2D rb;
    Transform target;
    

    //Missiles
    Transform MissilD1;
    Transform MissilD2;
    Transform MissilI1;
    Transform MissilI2;

    public float missilRateNormal;
    private float missilRateRage;
    public float missileTime = 0;

    //Torretas
    Transform TorretaD1;
    Transform TorretaD2;
    Transform TorretaD3;
    
    Transform TorretaI1;
    Transform TorretaI2;
    Transform TorretaI3;
    //Fire points torretas
    Transform firePointD1A;
    Transform firePointD1B;

    Transform firePointD2A;
    Transform firePointD2B;

    Transform firePointD3A;
    Transform firePointD3B;

    Transform firePointI1A;
    Transform firePointI1B;

    Transform firePointI2A;
    Transform firePointI2B;

    Transform firePointI3A;
    Transform firePointI3B;

    Transform explosionPoint1;
    Transform explosionPoint11;
    Transform explosionPoint12;
    Transform explosionPoint2;
    Transform explosionPoint21;
    Transform explosionPoint22;
    Transform explosionPoint31;
    Transform explosionPoint32;

    //AAAAAAAAAAAAAAAAAAAAA
    public float fireRate;
    public float fireRateMin;
    public float fireRateMax;
    public float fireTime = 0;
    public float bulletForce = 1f;
    private bool ok = true;

    //SFX
    public AudioSource missileLaunchSFX;
    public AudioSource flakBatterySFX;
    public AudioSource JaggerIntro;

    AudioSource audioIntro;

    // Start is called before the first frame update
    void Start()
    {
        bossbar = vidaBoss.GetComponent<BossBars>();
        controller = GetComponent<EnemyController>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        missilRateRage = missilRateNormal / 2;
        explosionPoint1 = transform.Find("explosionPoint1");
        explosionPoint2 = transform.Find("explosionPoint2");

        explosionPoint11 = transform.Find("explosionPoint11");
        explosionPoint21 = transform.Find("explosionPoint21");

        explosionPoint12 = transform.Find("explosionPoint12");
        explosionPoint22 = transform.Find("explosionPoint22");

        explosionPoint31 = transform.Find("explosionPoint31");
        explosionPoint32 = transform.Find("explosionPoint32");

        TorretaD1 = transform.Find("TorretaD1");
        TorretaD2 = transform.Find("TorretaD2");
        TorretaD3 = transform.Find("TorretaD3");
        TorretaI1 = transform.Find("TorretaI1");
        TorretaI2 = transform.Find("TorretaI2");
        TorretaI3 = transform.Find("TorretaI3");

        firePointD1A = TorretaD1.Find("firePointD1A");
        firePointD1B = TorretaD1.Find("firePointD1B");

        firePointD2A = TorretaD2.Find("firePointD2A");
        firePointD2B = TorretaD2.Find("firePointD2B");

        firePointD3A = TorretaD3.Find("firePointD3A");
        firePointD3B = TorretaD3.Find("firePointD3B");

        firePointI1A = TorretaI1.Find("firePointI1A");
        firePointI1B = TorretaI1.Find("firePointI1B");

        firePointI2A = TorretaI2.Find("firePointI2A");
        firePointI2B = TorretaI2.Find("firePointI2B");

        firePointI3A = TorretaI3.Find("firePointI3A");
        firePointI3B = TorretaI3.Find("firePointI3B");

        MissilD1 = transform.Find("MissilD1");
        MissilD2 = transform.Find("MissilD2");
        MissilI1 = transform.Find("MissilI1");
        MissilI2 = transform.Find("MissilI2");

        checkTarget();
        getData();
        maxLife = controller.getLife();
        bossbar.SetMaxHealth(maxLife);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDeath();
        checkTarget();
        movement();
        if (!(PlayerController.instance.GetPlayerDead()))
        {
            checkCombat();
        }
        checkDistance();
        timer();
        timer2();
        bossbar.SetHealth(controller.getLife());
        
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, safetyDistance);
    }
    public void IntroMusic()
    {
        audioIntro = Instantiate(JaggerIntro);
        AudioManager.PlaySFX(audioIntro);
    }
    public void CheckDeath()
    {
        if (controller.getLife() <= 0 && ok1)
        {
            switch (state)
            {
                case 1:
                    state++;
                    controller.rellenarLife();
                    GameObject explosion1 = Instantiate(fireExplosion, explosionPoint1.position, Quaternion.identity, this.transform);
                    GameObject explosion3 = Instantiate(fireExplosion, explosionPoint11.position, Quaternion.identity, this.transform);
                    GameObject explosion5 = Instantiate(fireExplosion, explosionPoint12.position, Quaternion.identity, this.transform);
                    Destroy(motor1);
                    break;
                case 2:
                    state++;
                    controller.rellenarLife();
                    GameObject explosion2 = Instantiate(fireExplosion, explosionPoint2.position, Quaternion.identity, this.transform);
                    GameObject explosion4 = Instantiate(fireExplosion, explosionPoint21.position, Quaternion.identity, this.transform);
                    GameObject explosion6 = Instantiate(fireExplosion, explosionPoint22.position, Quaternion.identity, this.transform);
                    Destroy(motor2);
                    bossbar.ActivateRage();
                    break;
                case 3:
                    Time.timeScale = 1f;
                    bossbar.destroyBossBar();
                    audioIntro.Stop();
                    
                    controller.Dead();
                    FinishHim.SetActive(false);
                    fade.SetActive(true);
                    controller.StartTimerTrue();
                    ok1 = false;





                    break;

                
                default:
                    break;
            }
            

        }
        if (controller.getLife() <=maxLife/10 && state==3 && ok)
        {
            GameObject explosion2 = Instantiate(fireExplosion, explosionPoint31.position, Quaternion.identity, this.transform);
            GameObject explosion4 = Instantiate(fireExplosion, explosionPoint32.position, Quaternion.identity, this.transform);
            Destroy(motor3);
            Time.timeScale = 0.25f;
            ok = false;
            FinishHim.SetActive(true);
            
        }
        
    }
    private void RotateTurretRight()
    {
        Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward, turretTarget.position - TorretaD3.position);

        TurretRotation(TorretaD1, desiredRotation);
        FirePointRotation(firePointD1A, desiredRotation);
        FirePointRotation(firePointD1B, desiredRotation);

        TurretRotation(TorretaD2, desiredRotation);
        FirePointRotation(firePointD2A, desiredRotation);
        FirePointRotation(firePointD2B, desiredRotation);

        TurretRotation(TorretaD3, desiredRotation);
        FirePointRotation(firePointD3A, desiredRotation);
        FirePointRotation(firePointD3B, desiredRotation);
        
    }
    private void RotateTurretLeft()
    {
        Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward, turretTarget.position - TorretaI2.position);

        TurretRotation(TorretaI1, desiredRotation);
        FirePointRotation(firePointI1A, desiredRotation);
        FirePointRotation(firePointI1B, desiredRotation);

        TurretRotation(TorretaI2, desiredRotation);
        FirePointRotation(firePointI2A, desiredRotation);
        FirePointRotation(firePointI2B, desiredRotation);

        TurretRotation(TorretaI3, desiredRotation);
        FirePointRotation(firePointI3A, desiredRotation);
        FirePointRotation(firePointI3B, desiredRotation);
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
    private void getData()
    {
        distance = controller.getDistance();
        rotateSpeed = controller.getRotateSpeed();
        speed = controller.getSpeed();
        
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
    
    public int GetState()
    {
        return state;
    }
    private void checkTarget()
    {
        target = controller.getNextPoint();
        turretTarget = controller.getTargetPlayer();
    }
    private void fireTurrets()
    {
        if (fireTime >= fireRate)
        {
            AudioSource audioSFX = Instantiate(flakBatterySFX);
            AudioManager.PlaySFX(audioSFX);
            fireFirePoint(firePointD1A);
            fireFirePoint(firePointD1B);

            fireFirePoint(firePointD2A);
            fireFirePoint(firePointD2B);

            fireFirePoint(firePointD3A);
            fireFirePoint(firePointD3B);

            fireFirePoint(firePointI1A);
            fireFirePoint(firePointI1B);

            fireFirePoint(firePointI2A);
            fireFirePoint(firePointI2B);

            fireFirePoint(firePointI3A);
            fireFirePoint(firePointI3B);

            fireTime = 0;
            fireRate = Random.Range(fireRateMin, fireRateMax);
        }
    }
    private void fireMissiles()
    {
        if (state == 3)
        {
            if (missileTime >= missilRateRage)
            {
                AudioSource audioSFX = Instantiate(missileLaunchSFX);
                AudioManager.PlaySFX(audioSFX);
                Instantiate(missilePrefab, MissilD1.position, MissilD1.rotation);
                Instantiate(missilePrefab, MissilD2.position, MissilD2.rotation);
                Instantiate(missilePrefab, MissilI1.position, MissilI1.rotation);
                Instantiate(missilePrefab, MissilI2.position, MissilI2.rotation);

                missileTime = 0;
            }
        }
        else
        {


            if (missileTime >= missilRateNormal)
            {
                AudioSource audioSFX = Instantiate(missileLaunchSFX);
                AudioManager.PlaySFX(audioSFX);
                Instantiate(missilePrefab, MissilD1.position, MissilD1.rotation);
                Instantiate(missilePrefab, MissilD2.position, MissilD2.rotation);
                Instantiate(missilePrefab, MissilI1.position, MissilI1.rotation);
                Instantiate(missilePrefab, MissilI2.position, MissilI2.rotation);

                missileTime = 0;
            }
        }
    }
    private void fireFirePoint(Transform firepoint)
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firepoint.up * bulletForce, ForceMode2D.Impulse);
    }
    private void checkCombat()
    {
        if (controller.getCombat())
        {
            RotateTurretRight();
            RotateTurretLeft();
            fireTurrets();
            fireMissiles();
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
    private float timer()
    {
        fireTime += Time.deltaTime;
        return fireTime;
    }
    private float timer2()
    {
        missileTime += Time.deltaTime;
        return missileTime;
    }
}
