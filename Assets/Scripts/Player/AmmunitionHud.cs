using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AmmunitionHud : MonoBehaviour
{
    public Image image;
    public GameObject reloadIcon;
    public float maxAmmo;
    public float reloadTime;
    public float maxTime;
    public float ammoCount;
    public bool reloading = false;
    public float time = 0;
    public float actualAmmo;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        maxTime = reloadTime/maxAmmo;
        
        reloadIcon.SetActive(false);
        ammoCount = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (!reloading)
        {
            image.fillAmount = ammoCount / maxAmmo;
            time = 0;
            reloadIcon.SetActive(false);
            image.color = new Color(0.9f, 0.9f, 0.9f);
        }
        else
        {
            image.color = new Color(1f, 0.5f, 0.5f);
            reloadIcon.SetActive(true);
            timer();
            if (time >=maxTime)
            {
                image.fillAmount += 1 / maxAmmo;
                time = 0;

                
            }
            
            
        }
        
    }
    public void SetAmmo(float ammo)
    {
        ammoCount = ammo;
    }
    public void SetReloading(bool reload)
    {
        reloading = reload;
    }
    //public void SetReloadAmmo(float ammo)
    //{
    //    actualAmmo = ammo;
        
    //}
    public void timer()
    {
        time += Time.deltaTime;
    }
}
