using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Boat : MonoBehaviour
{
    public NavMeshAgent agent;
    public BaseSoldier targetE;
    public BaseSoldier targetP;
    public Transform attackPoint;
    public Transform defensePoint;
    public GameObject Bolt;
    public float time;
    public float attackSpeed;
    public bool onAttack;
    public bool isPlayer;
    public bool isEnemy;

    public float attackRange = 15f;
    public float damage = 10f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        onAttack = false;
    }

    private void Update()
    {
        Fire();
        //transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void AttackDef()
    {
        if (isPlayer == true)
        {
            List<BaseSoldier> listEnemy = GameManager.Instance.enemy.OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToList();
            if (listEnemy.Count > 0)
            {
                if (Vector3.Distance(transform.position, listEnemy[0].transform.position) <= attackRange)
                {
                    targetE = listEnemy[0];
                    onAttack = true;
                }                                                                                   
                else
                {
                    targetE = null;
                    onAttack = false;
                }                                            
            }
            if (listEnemy.Count == 0)
            {
                targetE = null;
                onAttack = false;
            }                                 
        }
        if (isEnemy == true)
        {
            List<BaseSoldier> listPlayer = GameManager.Instance.player.OrderBy(p => Vector3.Distance(transform.position, p.transform.position)).ToList();
            if (listPlayer.Count > 0)
            {
                if (Vector3.Distance(transform.position, listPlayer[0].transform.position) <= attackRange)
                {
                    targetP = listPlayer[0];
                    onAttack = true;
                }                                                             
                else
                {
                    targetP = null;
                    onAttack = false;
                }                                         
            }
            if (listPlayer.Count == 0)
            {
                targetP = null;
                onAttack = false;
            }                                   
        }       
    }

    public void Fire()
    {
        if (onAttack)
        {
            time += Time.deltaTime;
            if (time >= attackSpeed)
            {
                RespawnBolt();
                time = 0;
            }
        } else
            time = 0;
    }

    //public void AttackOnTarget()
    //{
    //    if (agent.isPlayer && targetE != null)
    //        targetE.TakeDamage(damage);
    //    if (agent.isEnemy && targetP != null)
    //        targetP.TakeDamage(damage);
    //}

    public void RespawnBolt()
    {
        GameObject bolt = Instantiate(Bolt);
        ArrowAndBolt aab = bolt.GetComponent<ArrowAndBolt>();
        aab.boat = this;
        if (isPlayer)
            aab.target = targetE;
        if (isEnemy)
            aab.target = targetP;
        bolt.transform.position = transform.position;
    }

    public void GoAttackPoint()
    {        
            agent.SetDestination(attackPoint.position);        
    }

    public void GoDefensePoint()
    {        
            agent.SetDestination(defensePoint.position);                              
    }
}
