using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Image image;
    public GameObject barraVida;
    public float altura;
    public bool posicionBarraVida = true;
    //VARIABLES COMUNES
    public float speed;
    public float speedFollowing;
    public float rotateSpeed;
    private float fixedSpeed;
    public float vision;
    public float weaponRange;
    public float distanceToTarget;
    public float maxLife;
    public float life;
    public bool bossFinal = false;
    public bool hitPlayer = false;
    private bool startimer = false;
    private float timetoend = 0f;
    public GameObject fadeout;
    private Quaternion rotation;

    public GameObject pasuyopciones;
    public Transform nextPoint;
    private Transform oldPoint;
    public Transform target;
    public GameObject explosionPrefab;
    public GameObject explosionPrefab2;

    private GameObject player;
    public bool IsBoss;
    public bool isPlane;

    public bool patrolling;
    public bool following;
    public bool inCombat;
    public bool alreadyDead;
    private bool jiji =  true;
    private Rigidbody2D _rb;
    private PowerUps powers;
    private DissolveOnFire _dissolve;

    //SFX
    public AudioSource explosionSFX;

    // Use this for initialization
    void Start()
    {
        _rb = this.gameObject.GetComponent<Rigidbody2D>();
        _dissolve = this.gameObject.GetComponent<DissolveOnFire>();
        player = GameObject.Find("Player Variant");
        powers = player.GetComponent<PowerUps>();
        fixedSpeed = speed * Time.deltaTime;
        patrolling = true;
        following = false;
        inCombat = false;
        alreadyDead = false;
        life = maxLife;
    }

    // Update is called once per frame 
    void Update()
    {
        checkDeath(bossFinal);
        checkStatus();
        FinalScreen();
        if (!IsBoss)
        {
            LifeBar();
        }
        
    }
    public void LifeBar()
    {
        //rotacion
        barraVida.transform.rotation = Quaternion.Euler(0.0f, 0.0f, gameObject.transform.rotation.z * -1.0f);
        //posicion
        if (posicionBarraVida)
        {
            barraVida.transform.position = gameObject.transform.position + new Vector3(0, altura, 0);
        }
        
        
        //valor vida
        image.fillAmount = life / maxLife;

        //visible/invisible
        if ((life == maxLife) || (life<=0))
        {
            barraVida.SetActive(false);

        }
        else
        {
            barraVida.SetActive(true);
        }



    }
    public Transform GetTarget()
    {
        return target;
    }
    public Transform GetNextPoint()
    {
        return nextPoint;
    }
    public void SetTarget(Transform targ)
    {
        target = targ;
    }
    public void SetNextPoint(Transform point)
    {
        nextPoint = point;
    }
    //RANGO DE VISION EN ESCENA
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, vision);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, weaponRange);
    }

    public void reduceLife (float n)
    {
        life = life - n;
        hitPlayer = true;
    }
    public void rellenarLife()
    {
        life = maxLife;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Point" && patrolling)
        {
            fixedSpeed = -fixedSpeed;
            oldPoint = nextPoint;
            nextPoint = collision.gameObject.GetComponent<PatrolPointer>().nextPoint;
        }
    }
    public void StartTimerTrue()
    {
        startimer = true;
    }
    public void FinalScreen()
    {
        if (startimer)
        {
            timetoend += Time.deltaTime;
        }
        if (timetoend >= 1f)
        {
            pasuyopciones.GetComponent<PauseMenu>().LoadFinalScreen();
        }
        
        
    }
    public void Dead()
    {
        if (bossFinal)
        {

        }
        else
        {
            AudioSource audioSFX = Instantiate(explosionSFX);
            AudioManager.PlaySFX(audioSFX);
        }
        
        _dissolve.dieTest = true;
        this.patrolling = false;
        this.inCombat = false;
        this.weaponRange = 0f;
        this.vision = 0f;
        transform.gameObject.tag = "Nothing";

        if (isPlane)
        {
            GameObject explosion2 = Instantiate(explosionPrefab, transform.position, Quaternion.identity, this.transform);
            this.rotateSpeed = 10f;
            StartCoroutine(KillPlane());


        }

        else if (bossFinal)
        {
            
            GameObject explosion2 = Instantiate(explosionPrefab2, transform.position, Quaternion.identity, this.transform);
            this.rotateSpeed = 0;
        }

        else
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            this.rotateSpeed = 0;
            this.speed = 0;
            this.speedFollowing = 0;
        }

        Transform minimapIcon = this.transform.Find("MinimapIcon");
        Destroy(minimapIcon.gameObject);

        if (hitPlayer)
        {
            powers.AllyCounter();
        }

        alreadyDead = true;
    }

    IEnumerator KillPlane()
    {
        yield return new WaitForSeconds(5);
        GameObject explosion3 = Instantiate(explosionPrefab2, transform.position, Quaternion.identity);
        Destroy(explosion3, 1f);
        Destroy(gameObject);

            }

    //CHECKERS
    public void checkDeath(bool bossFinal)
    {
        if (life <= 0 && !bossFinal)
        {
            if (!alreadyDead) {
                Dead();
            }
        }
    }

    public void checkStatus()
    {
        distanceToTarget = checkDistance();

        if (distanceToTarget < vision)
        {
            following = true;
            patrolling = false;
        }

        else
        { 
            following = false;
            patrolling = true;
        }

        if (distanceToTarget < weaponRange)
        {
            inCombat = true;
        }

        else
        {
            inCombat = false;
        }
    }
    public float checkDistance()
    {
        if (!(PlayerController.instance.GetPlayerDead()))
        {
            return Vector2.Distance(transform.position, target.transform.position);
        }
        else
        {
            return Vector2.Distance(transform.position, transform.position);
        }
        
    }

    //GETTERS
    public bool getFollowing()
    {
        return following;
    }

    public bool getPatrolling()
    {
        return patrolling;
    }

    public bool getCombat()
    {
        return inCombat;
    }

    public float getDistance()
    {
        return checkDistance();
    }

    public float getSpeed()
    {
        return speed;
    }

    public float getRotateSpeed()
    {
        return rotateSpeed;
    }

    public float getSpeedFollowing()
    {
        return speedFollowing;
    }

    public Transform getNextPoint()
    {
        return nextPoint;
    }

    public Transform getTargetPlayer()
    {
        return target;
    }

    public float getLife()
    {
        return life;
    }

}
