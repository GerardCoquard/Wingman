using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;


    public bool sonLosMenus;
    public GameObject pauseMenuUI; 
    public GameObject optionsMenuUI; 
    public GameObject controlMenuUI;
    public GameObject finalScreenUI;
    public GameObject AchivmentsMenuUI;
    public GameObject Chapter1;
    public GameObject Chapter2;
    public GameObject Chapter3;
    public GameObject Chapter4;
    public GameObject Chapter5;
    public bool notMainMenu;
    private bool optionsMenuOn = false;
    private bool achivmentsMenuOn = false;
    private bool controlMenuOn = false;
    private bool goIn;
    public AudioMixer volMixer; 
    public Toggle fullScreenToggle;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider SFXSlider;  
    public Dropdown resolutionDropdown;
    public GameObject checkControls;
    private int screenInt;
    public bool isFullScreen = true;
    const string prefName = "optionValue";
    const string resName = "resolutionOption";
    public GameObject fade;

    Resolution[] resolutions;

    // Update is called once per frame 
    private void Awake()
    {
        
        screenInt = PlayerPrefs.GetInt("toggleState",1);

        if(screenInt == 0)
        {
           
            isFullScreen = false;
            fullScreenToggle.isOn = false;
        }
        else
        {
            isFullScreen = true;
            fullScreenToggle.isOn = true;
        }

        resolutionDropdown.onValueChanged.AddListener(new UnityAction<int>(index => 
        { 
            
            PlayerPrefs.SetInt(resName, resolutionDropdown.value); 
            PlayerPrefs.Save(); 
        }));
        

    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        GameIsPaused = false;
        masterSlider.value = PlayerPrefs.GetFloat("MVolume", 0.75f);
        volMixer.SetFloat("master", PlayerPrefs.GetFloat("MVolume"));

        musicSlider.value = PlayerPrefs.GetFloat("MuVolume", 0.5f);
        volMixer.SetFloat("music", PlayerPrefs.GetFloat("MuVolume"));

        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        volMixer.SetFloat("SFX01", PlayerPrefs.GetFloat("SFXVolume"));
        volMixer.SetFloat("SFX02", PlayerPrefs.GetFloat("SFXVolume"));
        volMixer.SetFloat("SFX03", PlayerPrefs.GetFloat("SFXVolume"));

        resolutions = GetResolutions().ToArray();

        resolutionDropdown.ClearOptions();


        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].width == Screen.currentResolution.width)
            {
                currentResolutionIndex = i;
            }


        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt(resName, currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();

        if (sonLosMenus)
        {
            CheckChapters();
            if (PlayerPrefs.GetInt("firstTime", 0) == 0)
            {
                checkControls.SetActive(true);
            }
        }

        

    }



    void Update()
    {
        if (sonLosMenus)
        {
            CheckChapters();
        }
        
        if (Input.GetKeyDown(KeyCode.Escape) && notMainMenu && !optionsMenuOn && !controlMenuOn && !goIn && !achivmentsMenuOn)
        {
            if (GameIsPaused)
            {

                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && optionsMenuOn)
        {
            QuitOptionsMenu();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && controlMenuOn) 
        {
            QuitControlMenu(); 
        }
        if (Input.GetKeyDown(KeyCode.Escape) && achivmentsMenuOn)
        {
            QuitAchivmentsMenu();
        }

    }
    public void SetControlMenu(bool setter)
    {
        goIn = setter;
        if (goIn)
        {
            GameIsPaused = true;
        }
        else
        {
            GameIsPaused = false;
        }
    }
    public void CheckChapters()
    {
        switch (PlayerPrefs.GetInt("guardado"))
        {
            case 0:
                Chapter1.SetActive(false);
                Chapter2.SetActive(false);
                Chapter3.SetActive(false);
                Chapter4.SetActive(false);
                Chapter5.SetActive(false);
                break;
            case 1:
                Chapter1.SetActive(true);
                Chapter2.SetActive(false);
                Chapter3.SetActive(false);
                Chapter4.SetActive(false);
                Chapter5.SetActive(false);
                break;
            case 2:
                Chapter1.SetActive(true);
                Chapter2.SetActive(true);
                Chapter3.SetActive(false);
                Chapter4.SetActive(false);
                Chapter5.SetActive(false);
                break;
            case 3:
                Chapter1.SetActive(true);
                Chapter2.SetActive(true);
                Chapter3.SetActive(true);
                Chapter4.SetActive(false);
                Chapter5.SetActive(false);
                break;
            case 4:
                Chapter1.SetActive(true);
                Chapter2.SetActive(true);
                Chapter3.SetActive(true);
                Chapter4.SetActive(true);
                Chapter5.SetActive(false);
                break;
            case 5:
                Chapter1.SetActive(true);
                Chapter2.SetActive(true);
                Chapter3.SetActive(true);
                Chapter4.SetActive(true);
                Chapter5.SetActive(true);
                break;
            default:
                Chapter1.SetActive(false);
                Chapter2.SetActive(false);
                Chapter3.SetActive(false);
                Chapter4.SetActive(false);
                Chapter5.SetActive(false);
                break;
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
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
    public void LoadOptionsMenu() 
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
        optionsMenuOn = true;


    }
    public void QuitOptionsMenu() 
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        optionsMenuOn = false;

    }
    public void LoadAchivmentsMenu()
    {
        pauseMenuUI.SetActive(false);
        AchivmentsMenuUI.SetActive(true);
        achivmentsMenuOn = true;


    }
    public void QuitAchivmentsMenu()
    {
        pauseMenuUI.SetActive(true);
        AchivmentsMenuUI.SetActive(false);
        achivmentsMenuOn = false;

    }
    public void LoadControlMenu()
    { 
        pauseMenuUI.SetActive(false);
        controlMenuUI.SetActive(true);
        controlMenuOn = true;
        PlayerPrefs.SetInt("firstTime", 1);
        checkControls.SetActive(false);
        


    }
    public void LoadFinalScreen()
    {
        SceneManager.LoadScene("FinalScene");
        Debug.Log("FINALSCREEN");

    }

    public void QuitControlMenu()
    {
        controlMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        controlMenuOn = false;
         
    }
    public void LoadMainGameScene()  
    { 
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(1);
        

    }

    public void SetResolution(int index) 
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen); 
    }

    public void SetFullScreen(bool isfullscreen)
    {
        Screen.fullScreen = isfullscreen;

        if (isfullscreen == false)
        {
            PlayerPrefs.SetInt("toggleState", 0);
        }
        else
        {
            isfullscreen = true;
            PlayerPrefs.SetInt("toggleState", 1);
        }


    }

    public void SetVolumeMaster(float volume) 
    {
        PlayerPrefs.SetFloat("MVolume", volume);
        volMixer.SetFloat("master", PlayerPrefs.GetFloat("MVolume"));
    }
    public void SetVolumeMusic(float volume)
    {
        PlayerPrefs.SetFloat("MuVolume", volume);
        volMixer.SetFloat("music", PlayerPrefs.GetFloat("MuVolume")); 
    }
    public void SetVolumeSFX(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume me", volume);
        volMixer.SetFloat("SFX01", PlayerPrefs.GetFloat("SFXVolume"));

        PlayerPrefs.SetFloat("SFXVolume", volume);
        volMixer.SetFloat("SFX02", PlayerPrefs.GetFloat("SFXVolume")-10);

        PlayerPrefs.SetFloat("SFXVolume", volume);
        volMixer.SetFloat("SFX03", PlayerPrefs.GetFloat("SFXVolume"));

    }

    public static bool GetIsPaused()
    {
        return GameIsPaused;
    }
    List<Resolution> GetResolutions()
    {
        //Filters out all resolutions with low refresh rate:
        Resolution[] resolutions = Screen.resolutions;
        HashSet<System.ValueTuple<int, int>> uniqResolutions = new HashSet<System.ValueTuple<int, int>>();
        Dictionary<System.ValueTuple<int, int>, int> maxRefreshRates = new Dictionary<System.ValueTuple<int, int>, int>();
        for (int i = 0; i < resolutions.GetLength(0); i++)
        {
            //Add resolutions (if they are not already contained)
            System.ValueTuple<int, int> resolution = new System.ValueTuple<int, int>(resolutions[i].width, resolutions[i].height);
            uniqResolutions.Add(resolution);
            //Get highest framerate:
            if (!maxRefreshRates.ContainsKey(resolution))
            {
                maxRefreshRates.Add(resolution, resolutions[i].refreshRate);
            }
            else
            {
                maxRefreshRates[resolution] = resolutions[i].refreshRate;
            }
        }
        //Build resolution list:
        List<Resolution> uniqResolutionsList = new List<Resolution>(uniqResolutions.Count);
        foreach (System.ValueTuple<int, int> resolution in uniqResolutions)
        {
            Resolution newResolution = new Resolution();
            newResolution.width = resolution.Item1;
            newResolution.height = resolution.Item2;
            if (maxRefreshRates.TryGetValue(resolution, out int refreshRate))
            {
                newResolution.refreshRate = refreshRate;
            }
            uniqResolutionsList.Add(newResolution);
        }
        return uniqResolutionsList;
    }
}
