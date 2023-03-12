using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBars : MonoBehaviour
{
    public Image image;

    public GameObject BossBar;
    public GameObject rage;
    public bool haveRage;
    public bool bossFinal;
    private float maxhealth;
    
    




    private void Start()
    {
        
        SetVisible(rage, false);
    }
    public void SetVisible(GameObject gameObj , bool visible)
    {
        gameObj.SetActive(visible);
    }
    public void SetMaxHealth(float health)
    {
        maxhealth = health;
        image.fillAmount = health/maxhealth;
    }
    
   
    public void SetHealth(float health)
    {
        image.fillAmount = health / maxhealth;
    }
    private void Update()
    {
        
        if (haveRage)
        {
            CheckRage();
        }

        if (!bossFinal)
        {
            if (image.fillAmount <= 0f)
            {
                Destroy(gameObject);

            }
        }
       

    }
    public void destroyBossBar()
    {
        Destroy(BossBar);
    }
    
    private void CheckRage()
    {
        if (image.fillAmount <= 0.5f)
        {
            SetVisible(rage, true);
        }
    }
    public void ActivateRage()
    {
        rage.SetActive(true);
    }
    
}
