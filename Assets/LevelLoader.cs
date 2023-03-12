using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen; 
    public Image image;
    

    
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
        
        
        PlayerPrefs.SetInt("estado", 0);
    }
    public void LoadLevel1(int sceneIndex)
    {
        if (PlayerPrefs.GetInt("guardado") >= 1)
        {
            StartCoroutine(LoadAsynchronously(sceneIndex));
            PlayerPrefs.SetInt("estado", 1);
        }
       
    }
    
    public void LoadLevel2(int sceneIndex)
    {
        if (PlayerPrefs.GetInt("guardado") >= 2)
        {
            StartCoroutine(LoadAsynchronously(sceneIndex));
            PlayerPrefs.SetInt("estado", 2);
        }
        
    }
    public void LoadLevel3(int sceneIndex)
    {
        if (PlayerPrefs.GetInt("guardado") >= 3)
        {
            StartCoroutine(LoadAsynchronously(sceneIndex));
            PlayerPrefs.SetInt("estado", 3);
        }
       
    }
    public void LoadLevel4(int sceneIndex)
    {
        if (PlayerPrefs.GetInt("guardado") >= 4)
        {
            StartCoroutine(LoadAsynchronously(sceneIndex));
            PlayerPrefs.SetInt("estado", 4);
        }
        
    }
    public void LoadLevel5(int sceneIndex)
    {
        if (PlayerPrefs.GetInt("guardado") >= 5)
        {
            StartCoroutine(LoadAsynchronously(sceneIndex));
            PlayerPrefs.SetInt("estado", 5);
        }
        
    }
    public void RestartGuardado()
    {
        PlayerPrefs.SetInt("guardado", 0);
        PlayerPrefs.SetInt("estado", 0);
        PlayerPrefs.SetInt("primeraVez", 0);
        PlayerPrefs.SetInt("firstTime", 0);
        Application.Quit();

    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            image.fillAmount = progress;
            

            yield return null;
        }
    }


}
