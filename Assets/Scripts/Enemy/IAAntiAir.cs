using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAAntiAir : MonoBehaviour
{
    EnemyController controller;
    public GameObject missilePrefab;
    public bool isStop;
    private float distance;
    private float rotateSpeed;
    private float speed;
    Transform firePoint;
    Transform target;
    Transform missileTarget;
    Rigidbody2D rb;

    //MISSILE SYSTEM
    public float missileRate;
    public float missileRateMin;
    public float missileRateMax;
    public float missileTime = 0;

    //SFX
    
    void Start()
    {
        controller = GetComponent<EnemyController>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        firePoint = transform.Find("FirePoint");
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
        missileTarget = controller.getTargetPlayer();
    }

    private void checkCombat()
    {
        if (controller.getCombat())
        {
            speed = 0;
            fireMissile();
        }

        else speed = controller.getSpeed();
    }

    private void fireMissile()
    {
        if (missileTime >= missileRate)
        {
            
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
        distance = controller.getDistance();
        rotateSpeed = controller.getRotateSpeed();
        speed = controller.getSpeed();
    }
}
