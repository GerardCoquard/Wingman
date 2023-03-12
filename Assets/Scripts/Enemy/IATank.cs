using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATank : MonoBehaviour
{
    EnemyController controller;
    public GameObject bulletPrefab;
    public bool isStop;
    private float distance;
    private float rotateSpeed;
    public float rotationSpeedTurret;
    private float speed;
    Transform firePoint;
    Transform tankTurret;
    Transform turretTarget;
    Transform target;
    Rigidbody2D rb;

    //TURRET SYSTEM
    public float fireRate;
    public float fireRateMin;
    public float fireRateMax;
    public float fireTime = 0;
    public float bulletForce = 1f;

    //SFX
    public AudioSource cannonFireSFX;

    void Start()
    {
        controller = GetComponent<EnemyController>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        tankTurret = transform.Find("enemyTurret");
        firePoint = tankTurret.Find("FirePoint");

        checkTarget();
        getData();
    }

    void FixedUpdate()
    {
        checkTarget();
        checkCombat();
        movement();
        timer();
    }

    public void RotateTankTurret()
    {
        if (!(PlayerController.instance.GetPlayerDead()))
        {
            Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward, turretTarget.position - tankTurret.position);
            desiredRotation = Quaternion.Euler(0, 0, desiredRotation.eulerAngles.z);
            tankTurret.rotation = Quaternion.RotateTowards(tankTurret.rotation, desiredRotation, rotationSpeedTurret * Time.deltaTime);
            desiredRotation = Quaternion.Euler(0, 0, desiredRotation.eulerAngles.z);
            firePoint.rotation = Quaternion.RotateTowards(firePoint.rotation, desiredRotation, rotationSpeedTurret * Time.deltaTime);
        }

            
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
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotate = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotate * rotateSpeed;
        rb.velocity = transform.up * speed;
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
            speed = 0;
            RotateTankTurret();
            fireTurrets();
        }

        else speed = controller.getSpeed();
    }

    private void fireTurrets()
    {
        if (fireTime >= fireRate)
        {
            AudioSource audioSFX = Instantiate(cannonFireSFX);
            AudioManager.PlaySFX(audioSFX);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

            fireTime = 0;
            fireRate = Random.Range(fireRateMin, fireRateMax);
        }
    }

    private float timer()
    {
        fireTime += Time.deltaTime;
        return fireTime;
    }

    private void getData()
    {
        distance = controller.getDistance();
        rotateSpeed = controller.getRotateSpeed();
        speed = controller.getSpeed();
    }
}
