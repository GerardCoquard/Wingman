using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IACruiser : MonoBehaviour
{
    EnemyController controller;
    public GameObject bulletPrefab;
    public float safetyDistance;
    private bool changeRoute = false;
    private float distance;
    private float rotateSpeed;
    public float rotationSpeedTurret;
    private float speed;
    Transform firePoint11;
    Transform firePoint12;
    Transform firePoint21;
    Transform firePoint22;
    Transform cruiserTurret1;
    Transform cruiserTurret2;
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
    public AudioSource FlakBatterySFX;
    void Start()
    {
        controller = GetComponent<EnemyController>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        cruiserTurret1 = transform.Find("enemyTurret1");
        cruiserTurret2 = transform.Find("enemyTurret2");
        firePoint11 = cruiserTurret1.Find("FirePoint1");
        firePoint12 = cruiserTurret1.Find("FirePoint2");
        firePoint21 = cruiserTurret2.Find("FirePoint1");
        firePoint22 = cruiserTurret2.Find("FirePoint2");

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

    private void RotateCruiserTurret()
    {
        if (!(PlayerController.instance.GetPlayerDead()))
        {
            Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward, turretTarget.position - cruiserTurret1.position);

            TurretRotation(cruiserTurret1, desiredRotation);
            FirePointRotation(firePoint11, desiredRotation);
            FirePointRotation(firePoint12, desiredRotation);

            TurretRotation(cruiserTurret2, desiredRotation);
            FirePointRotation(firePoint21, desiredRotation);
            FirePointRotation(firePoint22, desiredRotation);
        }
    }

    private void TurretRotation(Transform turret, Quaternion rotation)
    {
        rotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z);
        turret.rotation = Quaternion.RotateTowards(turret.rotation, rotation, rotationSpeedTurret * Time.deltaTime);
    }

    private void FirePointRotation(Transform firePoint, Quaternion rotation)
    {
        rotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z - 90);
        firePoint11.rotation = Quaternion.RotateTowards(firePoint.rotation, rotation, rotationSpeedTurret * Time.deltaTime);
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
            RotateCruiserTurret();
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
            AudioSource audioSFX = Instantiate(FlakBatterySFX);
            AudioManager.PlaySFX(audioSFX);
            fireFirePoint(firePoint11);
            fireFirePoint(firePoint12);
            fireFirePoint(firePoint21);
            fireFirePoint(firePoint22);
            fireTime = 0;
            fireRate = Random.Range(fireRateMin, fireRateMax);
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

    private void getData()
    {
        distance = controller.getDistance();
        rotateSpeed = controller.getRotateSpeed();
        speed = controller.getSpeed();
    }
}
