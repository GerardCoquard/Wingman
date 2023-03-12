using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAAllyFighter : MonoBehaviour
{
    AllyController controller;
    public GameObject missilePrefab;
    public float safetyDistance;
    private float rotateSpeed;
    private float speed;
    Transform firePoint;
    Transform targetFollow;
    Rigidbody2D rb;

    //MISSILE SYSTEM
    public float missileRate;
    public float missileRateMin;
    public float missileRateMax;
    public float missileTime = 0;

    //SFX
    public AudioSource missileLaunchSFX;
    void Start()
    {
        controller = GetComponent<AllyController>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        firePoint = transform.Find("FirePoint");
        checkTarget();
        getData();
    }

    void FixedUpdate()
    {
        getData();
        movement();
        checkCombat();
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
            Vector2 direction = (Vector2)targetFollow.position - rb.position;
            direction.Normalize();
            float rotate = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotate * rotateSpeed;
            rb.velocity = transform.up * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "EnemyBullet")
        {
            controller.reduceLife(20);
            
        }
        if (collision.transform.tag == "EnemyMissile")
        {
            controller.reduceLife(80);
            

        }
        if (collision.transform.tag == "Map")
        {
            controller.reduceLife(250);

        }

    }

    private void checkTarget()
    {
        targetFollow = controller.getPlayer();
    }

    private void checkCombat()
    {
        if (controller.getCombat())
        {
            fireMissile();
        }
    }

    private void fireMissile()
    {
        if (missileTime >= missileRate)
        {
            AudioSource audioSFX = Instantiate(missileLaunchSFX);
            AudioManager.PlaySFX(audioSFX);
            GameObject missile = Instantiate(missilePrefab, firePoint.position, firePoint.rotation);
            missileTime = 0;
            missileRate = Random.Range(missileRateMin, missileRateMax);
            
        }
    }

    private float timer()
    {
        missileTime += Time.deltaTime;
        return missileTime;
    }

    private void getData()
    {
        rotateSpeed = controller.getRotateSpeed();
        speed = controller.getSpeed();
    }
}

