using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrerasElectricas : MonoBehaviour
{
    public bool barreraActivaDePrimeras;
    // Start is called before the first frame update
    void Start()
    {
        if (barreraActivaDePrimeras)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetVisibleTrue()
    {
        gameObject.SetActive(true);
    }
    public void SetVisibleFalse()
    {
        gameObject.SetActive(false);
    }
}
