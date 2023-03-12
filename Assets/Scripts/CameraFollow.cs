using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 3;
    public Transform Player;
    public Vector3 offset;
    private Vector3 velocity;

        

    private void LateUpdate()
    {
        if (!(PlayerController.instance.GetPlayerDead()))
        {
            SimpleFollow();
        }
        
    }
    void SimpleFollow()
    {
            Vector3 desiredPosition = Player.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed * Time.deltaTime);
    }
}