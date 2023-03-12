using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABomber : MonoBehaviour
{
    EnemyController controller;
    public GameObject bulletPrefab;
    public float safetyDistance;
    private bool changeRoute = false;
    private float distance;
    private float rotateSpeed;
    public float rotationSpeedTurret;
    private float speed;
    Transform firePoint;
    Transform bomberTurret;
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
    public AudioSource flakFireSFX;

    void Start()
    {
        controller = GetComponent<EnemyController>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        bomberTurret = transform.Find("enemyTurret");
        firePoint = bomberTurret.Find("FirePoint");

        checkTarget();
        getData();
    }

    void FixedUpdate()
    {
        checkTarget();
        movement();
        checkCombat();
        checkDistance();
        timer();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, safetyDistance);
    }

    public void RotateBomberTurret()
    {
        if (!(PlayerController.instance.GetPlayerDead()))
        {
            Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward, turretTarget.position - bomberTurret.position);
            desiredRotation = Quaternion.Euler(0, 0, desiredRotation.eulerAngles.z + 135);
            bomberTurret.rotation = Quaternion.RotateTowards(bomberTurret.rotation, desiredRotation, rotationSpeedTurret * Time.deltaTime);
            desiredRotation = Quaternion.Euler(0, 0, desiredRotation.eulerAngles.z - 135);
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
            RotateBomberTurret();
            fireTurrets();
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
            AudioSource audioSFX = Instantiate(flakFireSFX);
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
