using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeColorMenu : MonoBehaviour
{
    
    Color newColor;
    Color colorObjetivo = new Color(1f, 1f, 1f); //white

    Color colorTrans = new Color(0f, 0f, 0f, 0.1f); //transparent

    public Text text;
    public bool ok = true;
    public float fadeTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        text.color = colorObjetivo;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColr();
    }
    private void ChangeColr()
    {


        if (fadeTime >= 1)
        {
            ok = true;
        }
        if (fadeTime <= 0)
        {
            ok = false;
        }
        if (ok)
        {
            newColor = text.color;
            newColor.a = fadeTime;
            fadeTime -= 0.01f;
            text.color = newColor;




        }
        else

        {
            newColor = text.color;
            newColor.a = fadeTime;
            fadeTime += 0.01f;
            text.color = newColor;


        }

    }
}
