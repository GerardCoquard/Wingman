using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFinalColliders : MonoBehaviour
{

    EnemyController controller;
    IABossFinal bossfinalIA;
    
    public int state;
    
   

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponentInParent<EnemyController>();
        bossfinalIA = gameObject.GetComponentInParent<IABossFinal>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "PlayerBullet" && (bossfinalIA.GetState() == state || bossfinalIA.GetState() == 4))
        {

            controller.reduceLife(20);
            
        }
        if (collision.transform.tag == "PlayerMissile" && (bossfinalIA.GetState() == state || bossfinalIA.GetState() == 4))
        {
            controller.reduceLife(80);
        }
    }
    
}
