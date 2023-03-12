using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public GameObject explosionPrefab;
    public bool playerDead = false;
    public GameObject powerupintro;

    public float regenValue;
    public bool regen = false;
    public bool first = true;
    public float maxHealth = 100;
    public float currentHealth;
    public GameObject vidaPlayer;
    HealthBar healthBar;
    public bool pause = false;
    private float _horizontalInput;
    private Vector3 _mousePosition;
    public GameObject pausemenus;
    PauseMenu pauseMenu;
    private bool boost;

    public GameObject minimap;
    Camera minimapCamera;

    MapSetter mapSetter;
    PowerUps powerUps;
    WeaponsController weaponsScript;
    PlayerMovement movementScript;
    AnimationsController playerAnimationScript;    
    Transform planeBody;
    Transform planeTurret;
    Transform firePoint;

    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        healthBar = vidaPlayer.GetComponent<HealthBar>();
        minimapCamera = minimap.GetComponent<Camera>();
        mapSetter = GetComponent<MapSetter>();
        powerUps = GetComponent<PowerUps>();
        weaponsScript = GetComponent<WeaponsController>();
        movementScript = GetComponent<PlayerMovement>();
        playerAnimationScript = GetComponent<AnimationsController>();
        planeBody = transform.Find("Plane");
        planeTurret = planeBody.Find("Turret");
        firePoint = planeTurret.Find("FirePoint");
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        pause = false;
        pauseMenu = pausemenus.GetComponent<PauseMenu>();

        
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnter();
        if (first)
        {
            mapSetter.MapState();
            first = false;
        }
        if (!pause)
        {
            GetPlayerInput();
            healthHackInfinite();
            healthHack100();
            Minimap();
            HealthRegen();

            TurretFire();
            FireMissile();
            movementScript.RotatePlayer(_horizontalInput);
            movementScript.RotatePlaneTurret(_mousePosition);

            playerAnimationScript.PlayerMovementSprites();

            boost = movementScript.GetBoost();

            playerAnimationScript.PlayerBoostSprite(boost);

            SetPlayerDead();
            PlayerDeath();
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            
        }
        pause = PauseMenu.GetIsPaused();
    }


    private void FixedUpdate()
    {
        movementScript.MovePlayer();
        
    }
    private void CheckEnter()
    {
        if (Input.GetKeyDown("return") && PlayerPrefs.GetInt("primeraVez") ==0)
        {
            PlayerPrefs.SetInt("primeraVez", 1);
            powerupintro.SetActive(false);
            Time.timeScale = 1f;
            pauseMenu.SetControlMenu(false);

        }
    }
    //HACKS
    private void healthHackInfinite()
    {
        if (Input.GetKeyDown("9")){
            currentHealth = 9999999;
            healthBar.SetHealth(currentHealth);
        }
    }

    private void healthHack100()
    {
        if (Input.GetKeyDown("0"))
        {
            currentHealth = 250;
            healthBar.SetHealth(currentHealth);
        }
    }
    public void RegenOn()
    {
        regen = true;
    }
    public void HealthRegen()
    {
        if (regen)

        {

            if (currentHealth < maxHealth)
            {
                currentHealth += regenValue;
                healthBar.SetHealth(currentHealth);
            }
            else
            {
                regen = false;
            }
        }

    }
    private void Minimap()
    {
        if (Input.GetKeyDown("left shift"))
        {
            if (minimapCamera.orthographicSize < 7)
            {
                minimapCamera.orthographicSize = 12;
            }

            else minimapCamera.orthographicSize = 6;

        }
    }

    private void GetPlayerInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        _mousePosition = new Vector3(mousePosition.x, mousePosition.y);

    }

    private void TurretFire()
    {
        if(Input.GetMouseButton(0))
        {
            weaponsScript.TurretFire();
        }
    }

    private void FireMissile()
    {
        if (Input.GetMouseButton(1))
        {
            weaponsScript.MissileFire();
        }
    }

    private void SetPlayerDead()
    {
        if (currentHealth <= 0)
        {
            playerDead = true;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!(powerUps.GetShield()))
        {
            powerUps.GotHit();
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "EnemyMissile")
        {
            TakeDamage(40);
        }
        if (collision.transform.tag == "EnemyBullet")
        {
            TakeDamage(10);
        }
        if (collision.transform.tag == "TriggersBarreras")
        {
            mapSetter.CheckCollision();
        }
        if (collision.transform.tag == "BarrerasElectricas")
        {
           playerDead = true;
        }
        if (collision.transform.tag == "TriggerMirilla")
        {
            mapSetter.DestroyTrigger();

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Map")
        {
            playerDead = true;
        }        
    }

    public bool GetPlayerDead()
    {
        return playerDead;
    }

    private void PlayerDeath()
    {
        if (playerDead)
        {
            //playerAnimationScript.PlayerDead(true);
            //movementScript.SetSpeed(0);
            //movementScript.SetRotaionSpeed(0);

            FindObjectOfType<GameManager>().EndGame();
            Instantiate(explosionPrefab, new Vector3(planeBody.position.x, planeBody.position.y, planeBody.position.z), Quaternion.identity);
            Destroy(gameObject);
        }

    }


}
