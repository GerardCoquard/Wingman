using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
    public Transform target;
    public string targetTag;
    public string shooterTag;
    public GameObject explosionPrefab;
    private Rigidbody2D rb;
    public float speed;
    public float rotateSpeed;
    ObjectLifeExpectancy objectLife;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objectLife = GetComponent<ObjectLifeExpectancy>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckTarget();
        CheckObjectLife();
        if (!(PlayerController.instance.GetPlayerDead()))
        {
            Vector2 direction = (Vector2)target.position - rb.position;
            direction.Normalize();
            float rotate = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotate * rotateSpeed;
        }



        rb.velocity = transform.up * speed;
    }

    private void CheckTarget()
    {
        if (!(PlayerController.instance.GetPlayerDead()))
        {
            target = FindClosestEnemy().transform;
        }

    }


    private void CheckObjectLife()
    {
        if (objectLife.remove) DestroyMissile();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != shooterTag)
        {
            DestroyMissile();
        }
    }

    private void DestroyMissile()
    {
        GameObject hit = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(hit, 1f);
        Destroy(gameObject);
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
