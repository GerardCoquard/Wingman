using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllyAnimation : MonoBehaviour
{
    public Image image;
    public static AllyAnimation instance;
    public float value;
    public bool pw = false;

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;

    }

    private void Update()
    {

        if (!pw)
        {
            if (value <= 1)
            {
                GetComponent<Image>().color = new Color(0.5f, 0.5f, 255f / 1f);
                if (image.fillAmount != value)
                {
                    image.fillAmount = Mathf.Lerp(image.fillAmount, value, 2.5f * Time.deltaTime);
                }

            }
            else
            {
                GetComponent<Image>().color = new Color(0.3f, 255f / 1f, 0.3f);
                image.fillAmount = 1;
            }
        }
        else
        {
            GetComponent<Image>().color = new Color(1f, 0.3f, 0.3f);
            image.fillAmount = 1;
        }

    }
    public void ReturnValue(float time, bool powerup)
    {
        value = time;
        pw = powerup;
    }

}
