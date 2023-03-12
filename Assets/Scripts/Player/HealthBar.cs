using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image image;
    private float maxhealth;
    

    public void SetMaxHealth(float health)
    {
        maxhealth = health;
        image.fillAmount = health / maxhealth;
    }
    
    public void SetHealth(float health)
    {
        image.fillAmount = health/maxhealth;

    }
    
    
}
