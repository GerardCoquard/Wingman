using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLifeExpectancy : MonoBehaviour
{
    public float time;
    public bool remove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            remove = true;
        }
    }

    public bool getAutoRemove()
    {
        return remove;
    }
}
