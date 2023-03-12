using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    
    public GameObject barreraElectrica;
    BarrerasElectricas barrera;
    public bool created;
    // Start is called before the first frame update
    void Start()
    {
       created = false;
       barrera = barreraElectrica.GetComponent<BarrerasElectricas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            //if (!created)
            //{
                barrera.SetVisibleTrue();
                created = true;
                Debug.Log("barrera");
            Destroy(gameObject);
            //}
            
        }
    }
    
}
