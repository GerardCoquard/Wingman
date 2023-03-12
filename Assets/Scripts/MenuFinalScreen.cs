using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFinalScreen : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego ");

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
