using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    
    public Animator animator;
    Transform planeBody;

    // Start is called before the first frame update
    void Start()
    {
        planeBody = transform.Find("Plane");
    }
    public void PlayerDead(bool dead)
    {
        animator.SetBool("Dead", dead);
    }
    public void BossRage(bool rage)
    {
        animator.SetBool("Rage", rage);
        
    }
    public void PlayerBoostSprite(bool boost)
    {
        animator.SetBool("Boost", boost);
    }
    public void PlayerMovementSprites()
    {
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            if (Input.GetKey("d") || Input.GetKey("right"))
            {
                animator.SetFloat("Rotation", 0);
            }
            else
            {
            animator.SetFloat("Rotation", 1);
            }
        }
        else if (Input.GetKey("d") || Input.GetKey("right"))
        {
            if (Input.GetKey("a") || Input.GetKey("left"))
            {
                animator.SetFloat("Rotation", 0);
            }
            else
            {
                animator.SetFloat("Rotation", -1);
            }
            
        }
        else
        {
            animator.SetFloat("Rotation", 0);
        }
        
    }
}
