using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public bool isStaminaOn=false;
    public bool isShieldOn=false;
    public bool isAllyOn=false;
    PWBar pw;
    public GameObject pw1a;
    public GameObject pw1b;
    public GameObject pw2a;
    public GameObject pw2b;
    public GameObject pw3a;
    public GameObject pw3b;

    public GameObject PWBar;

    [SerializeField]
    private float staminaTime;
    [SerializeField]
    private float shieldTime;
    [SerializeField]
    private float allyTime;
    [SerializeField]
    private bool pwOnUse;

    public float eliminiteAllyTime=0;
    public float allyLimit;
    public float shieldLimit;
    public float staminaLimit;
    public float allyActivateCounter;
    public float shieldActivateTime;
    public float staminaActivateTime;
    

    public int allyCounter;


    Transform planeBody;
    Transform allyFirePoint;
    public GameObject shieldPrefab;
    public GameObject allyPrefab;
    private GameObject allyObject;

    PlayerMovement playerMovement;

    void Start()
    {
        pw = PWBar.GetComponentInChildren<PWBar>();
        planeBody = transform.Find("Plane");
        allyFirePoint = planeBody.transform.Find("AllyFirePoint");
        playerMovement = GetComponent<PlayerMovement>();
        pwOnUse = false;
    }
    void FixedUpdate()
    {
        CheckAllyAlive();
        CheckPowerUp();
        shieldTime = ShieldTimer();
        staminaTime = StaminaTimer();
        allyTime = AllyTimer();
        eliminiteAllyTime = eliminateTimer();
        CheckAniamtionsTimers();
        

    }

    private void CheckAniamtionsTimers()
    {
        if (pwOnUse)
        {
            ShieldAnim.instance.ReturnValue(0, true);
            NitroAnimation.instance.ReturnValue(0, true);
            AllyAnimation.instance.ReturnValue(0, true);
        }
        else {
            ShieldAnim.instance.ReturnValue(shieldTime / shieldActivateTime, false);
            NitroAnimation.instance.ReturnValue(staminaTime / staminaActivateTime, false);
            AllyAnimation.instance.ReturnValue(allyCounter / allyActivateCounter, false);
             }
        
    }
    private void CheckAllyAlive()
    {
        if (allyObject == null && isAllyOn)
        {
            isAllyOn = false;
            pwOnUse = false;
            allyTime = 0;
            staminaTime = 0;
            shieldTime = 0;
            allyCounter = 0;
            eliminiteAllyTime = 0;
            pw1a.SetActive(true);
            pw1b.SetActive(true);
            pw2a.SetActive(true);
            pw2b.SetActive(true);
            Destroy(allyObject);
        }
    }
    


private float StaminaTimer() 
    { 
        staminaTime += Time.deltaTime;
        return staminaTime;
    }
    private float AllyTimer()
    {
        allyTime += Time.deltaTime;
        return allyTime;
    }
    private float ShieldTimer() 
    {
        shieldTime += Time.deltaTime;
        return shieldTime;
    }
    private float eliminateTimer()
    {
        if (isAllyOn)
        {
            eliminiteAllyTime += Time.deltaTime;
        }
        return eliminiteAllyTime;
    }
    public void AllyCounter()
    {
        if (!pwOnUse)
        {
            allyCounter++;
        }
    }

    public void GotBoost()
    {
        if (!pwOnUse)
        {
            staminaTime = 0;
        }
        
    }

    public void GotHit()
    {
        if (!pwOnUse)
        {
            shieldTime = 0;
        }  
    }
    private void CheckPowerUp()
    {
        EliminateAlly();
        StaminaPowerUp();
        ShieldPowerUp();
        AllyPowerUp();
        
    }
    private void EliminateAlly()
    {
        if (isAllyOn && Input.GetKey("f") && eliminiteAllyTime>=2)
        {
            isAllyOn = false;
            pwOnUse = false;
            allyTime = 0;
            staminaTime = 0;
            shieldTime = 0;
            allyCounter = 0;
            eliminiteAllyTime = 0;
            Destroy(allyObject);
            pw1a.SetActive(true);
            pw1b.SetActive(true);
            pw2a.SetActive(true);
            pw2b.SetActive(true);
            

        }
    }

    private void AllyPowerUp()
    {
        
        if (!pwOnUse && !isAllyOn && Input.GetKey("f") && allyCounter >= allyActivateCounter)
        {
            isAllyOn = true;
            pwOnUse = true;
            allyTime = 0;
            staminaTime = 0;
            shieldTime = 0;
            allyCounter = 0;
            eliminiteAllyTime = 0;
            pw1a.SetActive(false);
            pw1b.SetActive(false);
            pw2a.SetActive(false);
            pw2b.SetActive(false);

            allyObject = Instantiate(allyPrefab, allyFirePoint.position - new Vector3(0,0), allyFirePoint.rotation);
            
        }
        else if (isAllyOn && allyTime >= allyLimit)
        {
            isAllyOn = false;
            pwOnUse = false;
            allyTime = 0;
            staminaTime = 0;
            shieldTime = 0;
            allyCounter = 0;
            eliminiteAllyTime = 0;
            pw1a.SetActive(true);
            pw1b.SetActive(true);
            pw2a.SetActive(true);
            pw2b.SetActive(true);

            Destroy(allyObject);


        }
    }
    private void StaminaPowerUp()
    {
        if (!pwOnUse&&!isStaminaOn && Input.GetKey("e") && staminaTime >= staminaActivateTime)
        {
            isStaminaOn = true;
            pwOnUse = true;
            allyTime = 0;
            staminaTime = 0;
            shieldTime = 0;
            allyCounter = 0;
            pw1a.SetActive(false);
            pw1b.SetActive(false);
            pw3a.SetActive(false);
            pw3b.SetActive(false);
            PWBar.SetActive(true);
            pw.RestartTime();
            StaminaBar.instance.SetStamina();
        }

        else if (isStaminaOn && staminaTime >= staminaLimit)
        {
            isStaminaOn = false;
            pwOnUse = false;
            allyTime = 0;
            staminaTime = 0;
            shieldTime = 0;
            allyCounter = 0;
            
            pw1a.SetActive(true);
            pw1b.SetActive(true);
            pw3a.SetActive(true);
            pw3b.SetActive(true);
            PWBar.SetActive(false);

            StaminaBar.instance.SetStaminaBack();
        }
    }

    private void ShieldPowerUp()
    {
        if (!pwOnUse&&!isShieldOn && Input.GetKey("q") && shieldTime >= shieldActivateTime)
        {
            isShieldOn = true;
            pwOnUse = true;
            allyTime = 0;
            staminaTime = 0;
            shieldTime = 0;
            allyCounter = 0;
            pw2a.SetActive(false);
            pw2b.SetActive(false);
            pw3a.SetActive(false);
            pw3b.SetActive(false);
            PWBar.SetActive(true);
            pw.RestartTime();

            GameObject shieldObject = Instantiate(shieldPrefab, planeBody.position, planeBody.rotation, planeBody);
            Destroy(shieldObject, shieldLimit);
        }

        else if (isShieldOn && shieldTime >= shieldLimit)
        {
            isShieldOn = false;
            pwOnUse = false;
            allyTime = 0;
            staminaTime = 0;
            shieldTime = 0;
            allyCounter = 0;
            pw2a.SetActive(true);
            pw2b.SetActive(true);
            pw3a.SetActive(true);
            pw3b.SetActive(true);
            PWBar.SetActive(false);
        }
    }
    public bool GetInfStamina()
    {
        return isStaminaOn;
    }

    public bool GetShield() 
    { 
        return isShieldOn;
    }
    
}
