using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject hitPrefab;
    public string shooterTag;
    ObjectLifeExpectancy objectLife;

    // Start is called before the first frame update
    void Start()
    {
      objectLife = GetComponent<ObjectLifeExpectancy>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        CheckObjectLife();
    }

    private void CheckObjectLife()
    {
        if (objectLife.remove) DestroyBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != shooterTag)
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        GameObject hit = Instantiate(hitPrefab, transform.position, Quaternion.identity);
        Destroy(hit, 0.5f);
        Destroy(gameObject);
    }
}
