using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMirilla : MonoBehaviour
{

    public Texture2D cursorMirilla;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorMirilla, new Vector2(cursorMirilla.width/2,cursorMirilla.height/2), CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
