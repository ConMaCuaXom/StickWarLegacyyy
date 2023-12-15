using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowAndBolt : MonoBehaviour
{   
    public BaseSoldier target;
    //public CrossbowmanDefender crossbowmanDefender;
    public Boat boat;
    public Archer archer;
    public BaseEnemy baseEnemy;
    public BasePlayer basePlayer;

    public GameObject Bombom;
    public ParticleSystem bomPS;

    public float damageAr => archer.damage;
    public float damageBo => boat.damage;
    public float timeToDestroy = 10f;
    public float timeDur;
    public float timeTotal = 10f;

    

    //private bool haveMove = true;
    public bool isPlayer;


    private void Awake()
    {
        baseEnemy = GameManager.Instance.baseEnemy;
        basePlayer = GameManager.Instance.basePlayer;
        this.DelayCall(timeToDestroy, () =>
        {
            Destroy(gameObject);
        });
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //if (!haveMove)
        //    return;

        if (target != null)
            MoveToTarget(target.transform);
        if (archer!= null && archer.attackOnBase == true)
            MoveToBase();
    }

    private void MoveToTarget(Transform target)
    {
        if (boat != null)
        {
            timeDur += Time.deltaTime;
            float delta = timeDur / timeTotal;
            float teta = (timeDur - 0.1f) / timeTotal;
            Vector3 p1 = (boat.transform.position + target.transform.position) / 2 + Vector3.up * 4;
            transform.position = (1 - delta) * (1 - delta) * (boat.transform.position + Vector3.up * 1 + Vector3.right *0.5f) + 2 * (1 - delta) * delta * p1 + delta * delta * (target.transform.position + Vector3.up * 1f);
            transform.up = ((1 - teta) * (1 - teta) * (boat.transform.position + Vector3.up * 1 + Vector3.right * 0.5f) + 2 * (1 - teta) * teta * p1 + teta * teta * (target.transform.position + Vector3.up * 1f)) - transform.position;
        }
        if (archer != null)
        {
            timeDur += Time.deltaTime;
            float delta = timeDur / timeTotal;
            float teta = (timeDur - 0.1f) / timeTotal;
            Vector3 p1 = (archer.transform.position + target.transform.position) / 2 + Vector3.up * 2;
            transform.position = (1 - delta) * (1 - delta) * (archer.transform.position + Vector3.up * 1) + 2 * (1 - delta) * delta * p1 + delta * delta * (target.transform.position + Vector3.up * 1f);
            transform.up = ((1 - teta) * (1 - teta) * (archer.transform.position + Vector3.up * 1) + 2 * (1 - teta) * teta * p1 + teta * teta * (target.transform.position + Vector3.up * 1f)) - transform.position;
        }
        
    }

    public void MoveToBase()
    {
        if (archer.agent.isPlayer)
        {
            timeDur += Time.deltaTime;
            float delta = timeDur / timeTotal;
            float teta = (timeDur - 0.1f) / timeTotal;
            Vector3 p1 = (archer.transform.position + baseEnemy.transform.position) / 2 + Vector3.up * 2;
            transform.position = (1 - delta) * (1 - delta) * (archer.transform.position + Vector3.up * 1) + 2 * (1 - delta) * delta * p1 + delta * delta * (baseEnemy.transform.position + Vector3.up * 0.5f);
            transform.up = ((1 - teta) * (1 - teta) * (archer.transform.position + Vector3.up * 1) + 2 * (1 - teta) * teta * p1 + teta * teta * (baseEnemy.transform.position + Vector3.up * 0.5f)) - transform.position;
        }
        if (archer.agent.isEnemy)
        {
            timeDur += Time.deltaTime;
            float delta = timeDur / timeTotal;
            float teta = (timeDur - 0.1f) / timeTotal;
            Vector3 p1 = (archer.transform.position + basePlayer.transform.position) / 2 + Vector3.up * 2;
            transform.position = (1 - delta) * (1 - delta) * (archer.transform.position + Vector3.up * 1) + 2 * (1 - delta) * delta * p1 + delta * delta * (basePlayer.transform.position + Vector3.up * 0.5f);
            transform.up = ((1 - teta) * (1 - teta) * (archer.transform.position + Vector3.up * 1) + 2 * (1 - teta) * teta * p1 + teta * teta * (basePlayer.transform.position + Vector3.up * 0.5f)) - transform.position;
        }

    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (target!=null && other.gameObject.transform.position == target.transform.position)
        {
            //    haveMove = false;
            //    transform.SetParent(other.transform); // con set bo'
            
            if (boat != null)
            {
                target.TakeDamage(damageBo);              
                GameObject boom = Instantiate(Bombom,other.transform.position,transform.rotation);               
            }
            Destroy(gameObject);
            if (archer != null)
                target.TakeDamage(damageAr);
        }
        if (archer != null && archer.agent.isPlayer && other.CompareTag("BaseEnemy"))
        {
            
            archer.agent.baseEnemy.TakeDamage(damageAr);
            Destroy(gameObject);
        }
        if (archer != null && archer.agent.isEnemy && other.CompareTag("BasePlayer"))
        {
            
            archer.agent.basePlayer.TakeDamage(damageAr);
            Destroy(gameObject);
        }
    }

    

}
