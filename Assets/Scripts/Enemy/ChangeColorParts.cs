using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorParts : MonoBehaviour
{
    IABossFinal bossfinalIA;
    public int state;
    float timeLeft = 1;
    Color colorObjetivo = new Color(1f, 0.5f, 0.5f); //red
    Color colorActual = new Color(255 / 255, 255 / 255, 255 / 255);//white
    private bool changecolor = true;
    // Start is called before the first frame update
    void Start()
    {
        bossfinalIA = gameObject.GetComponentInParent<IABossFinal>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossfinalIA.GetState() == state)
        {
            ChangeColor();
        }
        else
        {
            GetComponent<SpriteRenderer>().color = colorActual;
        }
    }
    private void ChangeColor()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {

            if (changecolor)
            {
                changecolor = false;
            }
            else
            {
                changecolor = true;
            }

            timeLeft = 1f;

        }

        if (changecolor)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(colorActual, colorObjetivo, Time.deltaTime / timeLeft);
            

        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(colorObjetivo, colorActual, Time.deltaTime / timeLeft);
            
        }
    }
    
}
