using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSetter : MonoBehaviour
{
    
    
    IABossCarrier iaBoss1;
    IABossTanque iaBoss2;
    IABossFinal iaBoss3;
    Transform planeBody;

    PlayerController playerContr;
    public Texture2D cursorMirillaNegro;
    public Texture2D cursorMirillaBlanco;
    public GameObject TriggerMirilla;

    public GameObject controlesInstruc;
    public GameObject bossbar1;
    public GameObject bossbar2;
    public GameObject bossbar3;

    public GameObject Sala0;
    public GameObject Sala1;
    public GameObject Sala2;
    public GameObject Sala3;
    public GameObject Sala4;
    public GameObject Sala5;

    public GameObject Boss1;
    public GameObject Boss2;
    public GameObject Boss3;

    private bool startIntro = false;
    public float time = 0f;
    

    public GameObject FinishHim;

    public GameObject barreraElectrica1;
    BarrerasElectricas barrera1;
    public GameObject trigger1;

    public GameObject barreraElectrica2;
    BarrerasElectricas barrera2;


    public GameObject barreraElectrica3;
    BarrerasElectricas barrera3;
    public GameObject trigger3;

    public GameObject barreraElectrica4;
    BarrerasElectricas barrera4;


    public GameObject barreraElectrica5;
    BarrerasElectricas barrera5;
    public GameObject trigger5;

    public GameObject pausemenu;
    PauseMenu pausemenus;
    public int state;
    // Start is called before the first frame update
    void Start()
    {
        iaBoss1 = Boss1.GetComponent<IABossCarrier>();
        iaBoss2 = Boss2.GetComponent<IABossTanque>();
        iaBoss3 = Boss3.GetComponent<IABossFinal>();
        state = PlayerPrefs.GetInt("estado");
        SetState(PlayerPrefs.GetInt("estado"));
        barrera1 = barreraElectrica1.GetComponent<BarrerasElectricas>();
        barrera2 = barreraElectrica2.GetComponent<BarrerasElectricas>();
        barrera3 = barreraElectrica3.GetComponent<BarrerasElectricas>();
        barrera4 = barreraElectrica4.GetComponent<BarrerasElectricas>();
        barrera5 = barreraElectrica5.GetComponent<BarrerasElectricas>();

        planeBody = transform.Find("Plane");
        playerContr = GetComponent<PlayerController>();

        
        pausemenus = pausemenu.GetComponent <PauseMenu>();
        Time.timeScale = 1f;
        FinishHim.SetActive(false);
        if(PlayerPrefs.GetInt("primeraVez",0) == 0)
        {
            startIntro = true;
            
        }
        else
        {
            controlesInstruc.SetActive(false);
        }


    }

    // Update is called once per frame
    void Update()
    {
        state = PlayerPrefs.GetInt("estado");
        if (startIntro)
        {
            time += Time.deltaTime;
        }
        if (time >=2f)
        {
            controlesInstruc.SetActive(true);
            Time.timeScale = 0f;
            startIntro = false;
            time = 0;
            pausemenus.SetControlMenu(true);
        }
        
    }
    public void SetState(int actualstate)
    {
        state = actualstate;
        
    }
    
    public void MapState()
    {
        state = PlayerPrefs.GetInt("estado");
        switch (state)

        {
            case 0:
                barrera1.SetVisibleFalse();
                barrera2.SetVisibleFalse();
                barrera3.SetVisibleFalse();
                barrera4.SetVisibleFalse();
                barrera5.SetVisibleFalse();

                bossbar1.SetActive(false);
                bossbar2.SetActive(false);
                bossbar3.SetActive(false);

                gameObject.transform.position = new Vector3(0.2f, -3.5f, -160f);
                planeBody.transform.rotation = new Quaternion(0,0,0,1);

                Cursor.SetCursor(cursorMirillaBlanco, new Vector2(cursorMirillaBlanco.width / 2, cursorMirillaBlanco.height / 2), CursorMode.ForceSoftware);

                break;
            case 1:
                barrera1.SetVisibleTrue();
                barrera2.SetVisibleTrue();
                barrera3.SetVisibleFalse();
                barrera4.SetVisibleFalse();
                barrera5.SetVisibleFalse();

                bossbar1.SetActive(true);
                bossbar2.SetActive(false);
                bossbar3.SetActive(false);
                iaBoss1.IntroMusic();
                Destroy(trigger1);
                gameObject.transform.position = new Vector3(7.62f, 23.95f, -160f);
                planeBody.transform.rotation = new Quaternion(0f,0f,-120/360f,1);
                Sala0.SetActive(false);

                Cursor.SetCursor(cursorMirillaBlanco, new Vector2(cursorMirillaBlanco.width / 2, cursorMirillaBlanco.height / 2), CursorMode.ForceSoftware);
                break;
            case 2:
                barrera1.SetVisibleFalse();
                barrera2.SetVisibleFalse();
                barrera3.SetVisibleFalse();
                barrera4.SetVisibleFalse();
                barrera5.SetVisibleFalse();

                bossbar1.SetActive(false);
                bossbar2.SetActive(false);
                bossbar3.SetActive(false);

                Destroy(trigger1);
                gameObject.transform.position = new Vector3(24.35f, 25.89f, -160f);
                planeBody.transform.rotation = new Quaternion(0f, 0f, -350f / 360f, 1);
                Sala0.SetActive(false);
                Sala1.SetActive(false);
                Boss1.SetActive(false);

                Cursor.SetCursor(cursorMirillaBlanco, new Vector2(cursorMirillaBlanco.width / 2, cursorMirillaBlanco.height / 2), CursorMode.ForceSoftware);

                break;
            case 3:
                barrera1.SetVisibleFalse();
                barrera2.SetVisibleFalse();
                barrera3.SetVisibleTrue();
                barrera4.SetVisibleTrue();
                barrera5.SetVisibleFalse();

                bossbar1.SetActive(false);
                bossbar2.SetActive(true);
                bossbar3.SetActive(false);
                iaBoss2.IntroMusic();
                Destroy(trigger1);
                Destroy(trigger3);
                gameObject.transform.position = new Vector3(76.05f, 5.91f, -160f);
                planeBody.transform.rotation = new Quaternion(0f, 0f, -450f / 360f, 1);
                Sala0.SetActive(false);
                Sala1.SetActive(false);
                Sala2.SetActive(false);
                Boss1.SetActive(false);

                Cursor.SetCursor(cursorMirillaNegro, new Vector2(cursorMirillaNegro.width / 2, cursorMirillaNegro.height / 2), CursorMode.ForceSoftware);
                break;
            case 4:
                barrera1.SetVisibleFalse();
                barrera2.SetVisibleFalse();
                barrera3.SetVisibleFalse();
                barrera4.SetVisibleFalse();
                barrera5.SetVisibleFalse();

                bossbar1.SetActive(false);
                bossbar2.SetActive(false);
                bossbar3.SetActive(false);

                Destroy(trigger1);
                Destroy(trigger3);
                gameObject.transform.position = new Vector3(88.5f, 16.8f, -160f);
                planeBody.transform.rotation = new Quaternion(0f, 0f, -150f / 360f, 1);
                Sala0.SetActive(false);
                Sala1.SetActive(false);
                Sala2.SetActive(false);
                Sala3.SetActive(false);
                Boss1.SetActive(false);
                Boss2.SetActive(false);

                Cursor.SetCursor(cursorMirillaNegro, new Vector2(cursorMirillaNegro.width / 2, cursorMirillaNegro.height / 2), CursorMode.ForceSoftware);
                break;
            case 5:
                barrera1.SetVisibleFalse();
                barrera2.SetVisibleFalse();
                barrera3.SetVisibleFalse();
                barrera4.SetVisibleFalse();
                barrera5.SetVisibleTrue();

                bossbar1.SetActive(false);
                bossbar2.SetActive(false);
                bossbar3.SetActive(true);
                iaBoss3.IntroMusic();
                Destroy(trigger1);
                Destroy(trigger3);
                Destroy(trigger5);
                gameObject.transform.position = new Vector3(89.9f, 82.9f, -160f);
                planeBody.transform.rotation = new Quaternion(0f, 0f, 0 / 360f, 1);
                Sala0.SetActive(false);
                Sala1.SetActive(false);
                Sala2.SetActive(false);
                Sala3.SetActive(false);
                Sala4.SetActive(false);
                Boss1.SetActive(false);
                Boss2.SetActive(false);

                Cursor.SetCursor(cursorMirillaNegro, new Vector2(cursorMirillaNegro.width / 2, cursorMirillaNegro.height / 2), CursorMode.ForceSoftware);
                break;
            default:
                break;
        }
    }
    public void DestroyTrigger()
    {
        Cursor.SetCursor(cursorMirillaNegro, new Vector2(cursorMirillaNegro.width / 2, cursorMirillaNegro.height / 2), CursorMode.ForceSoftware);
        Destroy(TriggerMirilla);
    }
    public void CheckCollision()
    {

        state = PlayerPrefs.GetInt("estado");
        switch (state)
        {
            case 0:
                barrera1.SetVisibleTrue();
                barrera2.SetVisibleTrue();
                bossbar1.SetActive(true);
                iaBoss1.IntroMusic();
                Destroy(trigger1);
                if (state+1 > PlayerPrefs.GetInt("guardado"))
                {
                    PlayerPrefs.SetInt("guardado", 1);
                }
                playerContr.RegenOn();
                
                AddState(1);
                
                
                break;
            case 1:
                break;
            case 2:
                barrera3.SetVisibleTrue();
                barrera4.SetVisibleTrue();
                bossbar2.SetActive(true);
                iaBoss2.IntroMusic();
                Destroy(trigger3);
                if (state + 1 > PlayerPrefs.GetInt("guardado"))
                {
                    PlayerPrefs.SetInt("guardado", 3);
                }
                playerContr.RegenOn();
                
                AddState(3);
                
                break;
            case 3:
                break;

            case 4:
                barrera5.SetVisibleTrue();
                bossbar3.SetActive(true);
                iaBoss3.IntroMusic();
                Destroy(trigger5);
                if (state + 1 > PlayerPrefs.GetInt("guardado"))
                {
                    PlayerPrefs.SetInt("guardado", 5);
                }
                playerContr.RegenOn();
                
                AddState(5);
                
                break;
            case 5:
                break;
            default:
                break;
        }
        
    }
    public void AddState(int estado)
    {
        PlayerPrefs.SetInt("estado", estado);
    }
    
}
