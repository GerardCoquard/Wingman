using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PWBar : MonoBehaviour
{
    public Image image;
    public float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        image.fillAmount = (3f-time) / 3f;
        time += Time.deltaTime;
        
    }
    public void RestartTime()
    {
        time = 0f;
    }
}
