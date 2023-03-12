using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAFighter : MonoBehaviour
{
    EnemyController controller;
    public GameObject missilePrefab;
    public float safetyDistance;
    private bool changeRoute = false;
    private float distance;
    private float rotateSpeed;
    private float speed;
    Transform firePoint;
    Transform target;
    Rigidbody2D rb;

    //MISSILE SYSTEM
    public float missileRate;
    public float missileRateMin;
    public float missileRateMax;
    public float missileTime = 0;

    
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
        //else
        //{
        //    Vector2 direction = new Vector2(1,1);
        //    direction.Normalize();
        //    float rotate = Vector3.Cross(direction, transform.up).z;
        //    rb.angularVelocity = +rotate * rotateSpeed;
        //    rb.velocity = transform.up * speed;
        //}
            
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
        if (!(PlayerController.instance.GetPlayerDead()))
        {
            if (controller.getFollowing()) target = controller.getTargetPlayer();
            else target = controller.getNextPoint();
        }
            
    }

    private void checkCombat()
    {
        if (controller.getCombat())
        {
            fireMissile();
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
