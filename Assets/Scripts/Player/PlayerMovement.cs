using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float boost;
    public float slow;
    public float boostRate;
    private float time;



    public int rotationSpeedPlane;
    public int rotationSpeedTurret;
    public int movementSpeed;
    public Animator animator;
    public bool boostactivated;
    public bool gotBoost;

    PowerUps powerUps;

    Transform planeTurret;
    Transform firePoint;
    Transform planeBody;
    Rigidbody2D rb;

    //SFX
    public AudioSource boosterSFX;
    private bool playingBoostSFX;

    void Start()
    {
        powerUps = GetComponent<PowerUps>();
        rb = GetComponent<Rigidbody2D>();
        planeBody = transform.Find("Plane");
        planeTurret = planeBody.Find("Turret");
        firePoint = planeTurret.Find("FirePoint");
    }  
    
    public bool GetBoost()
    {
        if (boostactivated)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void MovePlayer()
    {      
        if (boostactivated && !playingBoostSFX)
        {
            AudioManager.PlaySFX(boosterSFX);
            playingBoostSFX = true;

        }

        if ((Input.GetKey("s") || Input.GetKey("down")) && !(StaminaBar.instance.GetStaminaEmpty()))
        {
            //if (movementSpeed > 100)
            //{
            //    movementSpeed--;
            //}
            //Time.timeScale = 0.25f;
            movementSpeed = 100;

        }
        else
        {
            //Time.timeScale = 1f;
            movementSpeed = 200;
        }

        if ((Input.GetKey("w") || Input.GetKey("up")) && !(StaminaBar.instance.GetStaminaEmpty()))
        {
            rb.AddForce(planeBody.up * movementSpeed * Time.deltaTime * boost);
            boostactivated = true;
            gotBoost = false;
            powerUps.GotBoost();

            if (!(powerUps.GetInfStamina()))    
            {

                StaminaBar.instance.UseStamina(1);
                

            }
        }
        else
        {
            playingBoostSFX = false;
            boostactivated = false;
            gotBoost = true;
            rb.AddForce(planeBody.up * movementSpeed * Time.deltaTime);
        }
    }

    public bool GetGotBoost()  
    {
        return gotBoost; 
    }

    public void RotatePlayer(float inputValue)
    {
        float rotation = -inputValue * rotationSpeedPlane * Time.deltaTime;
        planeBody.Rotate(Vector3.forward * rotation);
    }

    public void RotatePlaneTurret(Vector3 endpoint)
    {
        Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward, endpoint - planeTurret.position);
        desiredRotation = Quaternion.Euler(0, 0, desiredRotation.eulerAngles.z + 90);
        planeTurret.rotation = Quaternion.RotateTowards(planeTurret.rotation, desiredRotation, rotationSpeedTurret * Time.deltaTime);
        desiredRotation = Quaternion.Euler(0, 0, desiredRotation.eulerAngles.z - 90);
        firePoint.rotation = Quaternion.RotateTowards(firePoint.rotation, desiredRotation, rotationSpeedTurret * Time.deltaTime);

    }

    public void SetSpeed(int speed)
    {
        movementSpeed = speed;
    }
    public void SetRotaionSpeed(int rotationSpeed)
    {
        rotationSpeedPlane = rotationSpeed;
    }


}
