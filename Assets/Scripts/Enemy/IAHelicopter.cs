using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAHelicopter : MonoBehaviour
{
    EnemyController controller;
    public GameObject bulletPrefab;
    public float safetyDistance;
    private bool changeRoute = false;
    private float distance;
    private float rotateSpeed;
    private float speed;
    Transform firePoint1;
    Transform firePoint2;
    Transform target;
    Rigidbody2D rb;

    //TURRET SYSTEM
    public float fireRate;
    public float fireRateMin;
    public float fireRateMax;
    public float fireTime = 0;
    public float bulletForce = 1f;

    //SFX
    public AudioSource heliMinigunSFX;

    void Start()
    {
        controller = GetComponent<EnemyController>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        firePoint1 = transform.Find("FirePoint1");
        firePoint2 = transform.Find("FirePoint2");

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
    private void movement()
    {
        if (!(PlayerController.instance.GetPlayerDead()))
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

    private void checkTarget()
    {
        if (controller.getFollowing()) target = controller.getTargetPlayer();
        else target = controller.getNextPoint();
    }

    private void checkCombat()
    {
        if (controller.getCombat())
        {
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
            AudioSource audioSFX = Instantiate(heliMinigunSFX);
            AudioManager.PlaySFX(audioSFX);
            GameObject bullet1 = Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
            Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
            rb1.AddForce(firePoint1.up * bulletForce, ForceMode2D.Impulse);

            GameObject bullet2 = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
            rb2.AddForce(firePoint2.up * bulletForce, ForceMode2D.Impulse);

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
