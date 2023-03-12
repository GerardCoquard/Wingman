using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABossTanque : MonoBehaviour
{
    EnemyController controller;

    public bool first = true;
    public GameObject bulletPrefab;
    public GameObject missilePrefab;
    public GameObject baleTanquePrefab;
    public GameObject vidaBoss;
    public GameObject Player;

    BossBars bossbar;
    public GameObject barreraAbrir;
    public GameObject barreraAbrir2;

    AudioSource audioIntro;

    private bool changeRoute = false;
    private float distance;
    private float rotateSpeed;
    public float rotationSpeedTurret;
    private float speed;
    private float maxLife;
    public float safetyDistance;
    



    Transform turretTarget;
    Rigidbody2D rb;
    Transform target;


    //Missiles
    Transform MissilD1;
    
    Transform MissilI1;
    

    public float missilRateNormal;
    
    public float missileTime = 0;

    //Torretas
    Transform TorretaD1;
    
    Transform TorretaTanque;

    Transform TorretaI1;
    
    //Fire points torretas
    Transform firePointD1A;
    

    

    

    Transform firePointI1A;
    

    

    Transform firePointTorretaTanque1;
    Transform firePointTorretaTanque2;
    Transform firePointTorretaTanque3;

    //AAAAAAAAAAAAAAAAAAAAA
    public float fireRate;
    public float fireRateMin;
    public float fireRateMax;
    public float fireTime = 0;

    public float fireRateTurretTank;
    
    public float fireRateTurretTank1;
    public float fireTimeTurretTank1 = 0;
    public float fireRateTurretTank2;
    public float fireTimeTurretTank2 = 0;
    public float fireRateTurretTank3;
    public float fireTimeTurretTank3 = 0;
    public float fireRateTurretGeneral = 0;
    public float fireTimeTurretGeneral = 0;


    public float bulletForce = 1f;
    public float bulletTankForce = 1.4f;
    private bool ok = true;


    PlayerController playerContr;
    //SFX
    public AudioSource missileLaunchSFX;
    public AudioSource flakBatterySFX;
    public AudioSource JaggerIntro;
    public AudioSource BalaTanque;


    // Start is called before the first frame update
    void Start()
    {
        bossbar = vidaBoss.GetComponent<BossBars>();
        fireRateTurretTank1 = fireRateTurretTank;
        fireRateTurretTank2 = fireRateTurretTank;
        fireRateTurretTank3 = fireRateTurretTank;
        controller = GetComponent<EnemyController>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        
        
        TorretaD1 = transform.Find("TorretaD1");
        
        TorretaTanque = transform.Find("TorretaTanque");
        TorretaI1 = transform.Find("TorretaI1");
        

        firePointD1A = TorretaD1.Find("firePointD1A");
        

        

        firePointI1A = TorretaI1.Find("firePointI1A");
        

        

        firePointTorretaTanque1 = TorretaTanque.Find("firePoint1");
        firePointTorretaTanque2 = TorretaTanque.Find("firePoint2");
        firePointTorretaTanque3 = TorretaTanque.Find("firePoint3");

        MissilD1 = transform.Find("MissilD1");
        
        MissilI1 = transform.Find("MissilI1");

        playerContr = Player.GetComponent<PlayerController>();

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
        Timers();
        
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
        if (controller.getLife() < maxLife / 2 && ok)
        {
            fireRateMax /= 2;
            fireRateMin /= 2;
            missilRateNormal /= 2;
            fireRateTurretTank1 /= 2;
            fireRateTurretTank2 /= 2;
            fireRateTurretTank3 /= 2;
            ok = false;
        }
        if (controller.getLife() <= 0)
        {
            if (first)
            {
                getData();
                bossbar.destroyBossBar();
                audioIntro.Stop();
                barreraAbrir.SetActive(false);
                barreraAbrir2.SetActive(false);
                if (PlayerPrefs.GetInt("estado") + 1 > PlayerPrefs.GetInt("guardado"))
                {
                    PlayerPrefs.SetInt("guardado", 4);
                }
                PlayerPrefs.SetInt("estado", 4);
                playerContr.RegenOn();
                first = false;
            }
            
        }
    }
    private void RotateTurretRight()
    {
        Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward, turretTarget.position - TorretaD1.position);

        TurretRotation(TorretaD1, desiredRotation);
        FirePointRotation(firePointD1A, desiredRotation);
        

        

    }
    private void RotateTurretLeft()
    {
        Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward, turretTarget.position - TorretaI1.position);

        TurretRotation(TorretaI1, desiredRotation);
        FirePointRotation(firePointI1A, desiredRotation);
          
    }
    private void RotateTurretTank()
    {
        Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward, turretTarget.position - TorretaTanque.position);

        TurretRotation(TorretaTanque, desiredRotation);
        FirePointRotation(firePointTorretaTanque1, desiredRotation);
        FirePointRotation(firePointTorretaTanque2, desiredRotation);
        FirePointRotation(firePointTorretaTanque3, desiredRotation);

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

   
    private void checkTarget()
    {
        target = controller.getNextPoint();
        turretTarget = controller.getTargetPlayer();
    }
    private void FireAllTurrets()
    {
        fireTurrets();
        fireTurretTank1();
        fireTurretTank2();
        fireTurretTank3();
    }
    private void fireTurrets()
    {
        if (fireTime >= fireRate)
        {
            AudioSource audioSFX = Instantiate(flakBatterySFX);
            AudioManager.PlaySFX(audioSFX);
            fireFirePoint(firePointD1A);
            

            

            fireFirePoint(firePointI1A);
            

            

            fireTime = 0;
            fireRate = Random.Range(fireRateMin, fireRateMax);
        }
    }
    private void fireTurretTank1()
    {
        if (fireTimeTurretTank1 >= fireRateTurretTank)
        {
            AudioSource audioSFX = Instantiate(BalaTanque);
            AudioManager.PlaySFX(audioSFX);
            fireFirePointTurretTank(firePointTorretaTanque1);
            


            




            fireTimeTurretTank1 = 0;
            
        }
    }
    private void fireTurretTank2()
    {
        if (fireTimeTurretTank2 >= fireRateTurretTank)
        {
            AudioSource audioSFX = Instantiate(BalaTanque);
            AudioManager.PlaySFX(audioSFX);
            
            fireFirePointTurretTank(firePointTorretaTanque2);
            







            fireTimeTurretTank2 = 0;

        }
    }
    private void fireTurretTank3()
    {
        if (fireTimeTurretTank3 >= fireRateTurretTank)
        {
            AudioSource audioSFX = Instantiate(BalaTanque);
            AudioManager.PlaySFX(audioSFX);
            
            fireFirePointTurretTank(firePointTorretaTanque3);







            fireTimeTurretTank3 = 0;

        }
    }
    private void fireMissiles()
    {
        

            if (missileTime >= missilRateNormal)
            {
                AudioSource audioSFX = Instantiate(missileLaunchSFX);
                AudioManager.PlaySFX(audioSFX);
                Instantiate(missilePrefab, MissilD1.position, MissilD1.rotation);
                
                Instantiate(missilePrefab, MissilI1.position, MissilI1.rotation);
                

                missileTime = 0;
            }
        
    }
    private void fireFirePoint(Transform firepoint)
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firepoint.up * bulletForce, ForceMode2D.Impulse);
    }
    private void fireFirePointTurretTank(Transform firepoint)
    {
        GameObject bulletTank = Instantiate(baleTanquePrefab, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bulletTank.GetComponent<Rigidbody2D>();
        rb.AddForce(firepoint.up * bulletTankForce, ForceMode2D.Impulse);
    }
    private void checkCombat()
    {
        if (controller.getCombat())
        {
            RotateTurretRight();
            RotateTurretLeft();
            RotateTurretTank();
            FireAllTurrets();
            fireMissiles();
        }
        else
        {
            fireTimeTurretTank1 = 0;
            fireTimeTurretTank2 = 0;
            fireTimeTurretTank3 = 0;
            fireTimeTurretGeneral = 0;
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
    private void Timers()
    {
        timer1();
        timer2();
        if (controller.getCombat())
        {
            if (!(fireTimeTurretGeneral > fireRateTurretGeneral))
            {
                timer6();
            }
            
            if (fireTimeTurretGeneral > 0)
            {
                timer3();
                
            }
            if (fireTimeTurretGeneral > fireRateTurretGeneral/2)
            {
                
                timer4();
                
            }
            if (fireTimeTurretGeneral > fireRateTurretGeneral)
            {
                
                timer5();
            }


        }
       
        
        
    }
    private float timer1()
    {
        fireTime += Time.deltaTime;
        return fireTime;
    }
    private float timer2()
    {
        missileTime += Time.deltaTime;
        return missileTime;
    }
    private float timer3()
    {
        fireTimeTurretTank1 += Time.deltaTime;
        return fireTimeTurretTank1;
    }
    private float timer4()
    {
        fireTimeTurretTank2 += Time.deltaTime;
        return fireTimeTurretTank2;
    }
    private float timer5()
    {
        fireTimeTurretTank3 += Time.deltaTime;
        return fireTimeTurretTank3;
    }
    private float timer6()
    {
        fireTimeTurretGeneral += Time.deltaTime;
        return fireTimeTurretGeneral;
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
}
