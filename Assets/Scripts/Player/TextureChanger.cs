using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureChanger : MonoBehaviour
{
    public Image image;
    public Sprite[] frames;
    public int arrayPos = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        image.sprite = frames[arrayPos];
        arrayPos++;
        if (arrayPos>frames.Length)
        {
            arrayPos = 0;
        }
    }
}
