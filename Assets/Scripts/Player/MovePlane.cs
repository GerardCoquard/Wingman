using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MovePlane : MonoBehaviour
{
    
    public float speed;
    public float speed2;
    public float maxSpeed2;
    public float minSpeed2;
    public float maxSpeed;
    public float minSpeed;
    Vector3 posicion;
    public bool up = false;
    public bool right = true;
    // Start is called before the first frame update
    void Start()
    {
        minSpeed2 = maxSpeed2 * 0.15f;
        minSpeed = maxSpeed * 0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        posicion = gameObject.GetComponent<RectTransform>().position;

        if (posicion.y >650f)
        {
            up = false;

        }
        else if(posicion.y < 480f)
        {
            up = true;
            
        }
        if (posicion.x < 670f)
        {
            right = true;

        }
        else if (posicion.x > 1240f)
        {
            right = false;

        }


        if (up && (posicion.y < 481f || posicion.y > 649f))
        {
            speed = minSpeed;
        }
        else if (!up && (posicion.y < 481f || posicion.y > 649f))
        {
            speed = -minSpeed;
        }
        else if (up && posicion.y >= 481f && posicion.y <= 649f)
        {
            speed = maxSpeed;
        }
        else if (!up && posicion.y >= 481f && posicion.y <= 649f)
        {
            speed = -maxSpeed;
        }


        if (right && (posicion.x < 672f || posicion.x > 1238f))
        {
            speed2 = minSpeed2;
            
        }
        else if (!right && (posicion.x < 672f || posicion.x > 1238f))
        {
            speed2 = -minSpeed2;
            
        }
        else if (right && posicion.x >= 672f && posicion.x <= 1238f)
        {
            speed2 = maxSpeed2;
            
        }
        else if (!right && posicion.x >= 672f && posicion.x <= 1238f)
        {
            speed2 = -maxSpeed2;
            
        }
        

        gameObject.GetComponent<RectTransform>().position += new Vector3 (speed2,speed,0f);
        
    }
}
