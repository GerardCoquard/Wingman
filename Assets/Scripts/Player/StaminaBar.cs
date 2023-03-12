using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class StaminaBar : MonoBehaviour
{
    
    public Image image;
    private float maxStamina = 50;
    private float currentStamina;
    private bool staminaEmpty;
    public bool powerupstamina = false;

    private WaitForSeconds regenTick = new WaitForSeconds(0.001f);
    private Coroutine regen;

    public static StaminaBar instance;
    Color color2 = new Color(128f / 1f, 128f / 1f, 128f / 1f);
    Color color1 = new Color(0f / 1f, 255f / 1f, 0f / 1f);
    Color color3 = new Color(255f / 1f, 153f / 1f, 0f / 1f);

    


    private void Awake()
    {
        instance = this;

    }
    void Start()
    {
        currentStamina = maxStamina;

        

        image.fillAmount = currentStamina / maxStamina;

    }

    public void UseStamina(int amount)
    {
        if (currentStamina - amount >= 2 )
        {
            currentStamina -= amount;
            image.fillAmount = currentStamina/ maxStamina;

            if(regen != null)
            { 
                StopCoroutine(regen);
            }
            
            regen = StartCoroutine(RegenStamina());

            staminaEmpty = false;
        }
        else
        {
            staminaEmpty = true;

        }
        


    }

    private void Update()
    {
        SetSliderColor();
    }

    private IEnumerator RegenStamina()
    {
        
        yield return new WaitForSeconds(2);

        while (currentStamina < maxStamina)
        {
            if(currentStamina >= 15)
            {
                staminaEmpty = false;
            }
            
            currentStamina += maxStamina / 500;
            image.fillAmount = currentStamina/maxStamina;
            yield return regenTick;
        }
        
        regen = null;
    }
    

    public bool GetStaminaEmpty() 
    {
        return staminaEmpty;
    }

    public void SetStamina()
    {
        currentStamina = maxStamina;
        image.fillAmount = currentStamina/maxStamina;
        staminaEmpty = false;
        powerupstamina = true;
        
    }
    public void SetStaminaBack()
    {
        powerupstamina = false;
    }

    public void SetSliderColor()
    {
        if (powerupstamina)
        { image.color = color3;
            
        }
        else if (!staminaEmpty)
        {
            image.color = color1;
        }
        else
        {
            image.color = color2;
        }

    }
    

    
}
