using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientChanger : MonoBehaviour
{
    public AudioSource musicOn;
    public AudioSource musicOff;
    bool transition = false;
    float time = 0;
    public float timeLimit;

    void FixedUpdate()
    {
        if (transition)
        {
            checkStopMusic(time);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log("FIESTAA");
            AudioManager.PlayMusic(musicOn);
            transition = true;
        }
    }

    private void checkStopMusic(float time)
    {
        timer();
        if(time > timeLimit)
        {
            AudioManager.StopMusic(musicOff);
            transition = false;
        }
    }

    private float timer()
    {
        time += Time.deltaTime;
        return time;
    }
}
