using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllyController : MonoBehaviour
{
    public Image image;
    public GameObject barraVida;
    public float altura;
    //VARIABLES COMUNES
    public float speed;
    public float speedFollowing;
    public float rotateSpeed;
    private float fixedSpeed;
    public float weaponRange;
    public float distanceToTarget;
    public float maxLife;
    public float life;
    private float _dist;
    private float _minDist;

    private Transform closestTarget;
    [SerializeField]
    private Transform target;

    public GameObject[] targets;
    public string targetTag;
    public GameObject explosionPrefab;
    public GameObject explosionPrefab2;
    private GameObject player;

    public bool following;
    public bool inCombat;
    private bool alreadyDead = false;
    private Rigidbody2D _rb;

    //SFX
    public AudioSource explosionSFX;

    public static AllyController instance;
    // Use this for initialization
    void Start()
    {
        _rb = this.gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player Variant");
        fixedSpeed = speed * Time.deltaTime;
        following = true;
        inCombat = false;
        alreadyDead = false;
        life = maxLife;
        _dist = 0;
        _minDist = weaponRange;
    }

    // Update is called once per frame 
    void Update()
    {
        if (!alreadyDead)
        {
            checkStatus();
        }
        
        LifeBar();
    }
    public void LifeBar()
    {
        //rotacion
        barraVida.transform.rotation = Quaternion.Euler(0.0f, 0.0f, gameObject.transform.rotation.z * -1.0f);
        //posicion
        
            barraVida.transform.position = gameObject.transform.position + new Vector3(0, altura, 0);
        


        //valor vida
        image.fillAmount = life / maxLife;

        //visible/invisible
        if ((life == maxLife) || (life <= 0))
        {
            barraVida.SetActive(false);

        }
        else
        {
            barraVida.SetActive(true);
        }



    }
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    //RANGO DE VISION EN ESCENA
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, weaponRange);
    }

    public void reduceLife(float n)
    {
        life = life - n;
    }
    public void rellenarLife()
    {
        life = maxLife;
    }
    public void Dead()
    {
        inCombat = false;
        weaponRange = 0;
        this.rotateSpeed = 0f;

        AudioSource audioSFX = Instantiate(explosionSFX);
        AudioManager.PlaySFX(audioSFX);

        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity, this.transform);

        Transform minimapIcon = this.transform.Find("PlayerIcon");

        Destroy(minimapIcon.gameObject);
        StartCoroutine(KillPlane());
        alreadyDead = true;

        

    }
    IEnumerator KillPlane()
    {
        yield return new WaitForSeconds(5);
        GameObject explosion3 = Instantiate(explosionPrefab2, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }
    //CHECKERS
    public void checkStatus()
    {
        target = FindClosestEnemy().transform;
       
       
            distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
            if (distanceToTarget < weaponRange)
            {
                inCombat = true;
            }

            else
            {
                inCombat = true;
            }

            if (life <= 0) Dead();
        
    
        
    }
        
    

    //GETTERS
    


    public bool getCombat()
    {
        return inCombat;
    }

    public float getDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position);
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

    public Transform getTargetPlayer()
    {
        return target;
    }

    public Transform getPlayer()
    {
        return player.transform;
    }

    public float getLife()
    {
        return life;
    }
}
