using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CrossbowmanDefender : MonoBehaviour
{
    public Agent agent;
    public BaseSoldier targetE;
    public BaseSoldier targetP;
    public Transform attackPoint;
    public Transform defensePoint;   
    public GameObject Bolt;

    public float attackRange = 15f;
    public float damage = 10f;

    private void Awake()
    {
        agent = GetComponent<Agent>();
    }

    public void AttackDef()
    {
        if (agent.isPlayer == true)
        {
            List<BaseSoldier> listEnemy = GameManager.Instance.enemy.OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToList();

            if (listEnemy.Count > 0)
            {
                if (Vector3.Distance(transform.position, listEnemy[0].transform.position) <= attackRange)
                {                   
                    targetE = listEnemy[0];
                    agent.RotationOnTarget(targetE.transform.position - transform.position);
                    agent.animator.SetBool("Attack", true);
                }
                else
                {
                    NoTarget(targetE);
                }
            }
            if (listEnemy.Count == 0)
            {
                NoTarget(targetE);
            }
        }
        if (agent.isEnemy == true)
        {
            List<BaseSoldier> listPlayer = GameManager.Instance.player.OrderBy(p => Vector3.Distance(transform.position, p.transform.position)).ToList();

            if (listPlayer.Count > 0)
            {
                if (Vector3.Distance(transform.position, listPlayer[0].transform.position) <= attackRange)
                {
                    targetP = listPlayer[0];
                    agent.RotationOnTarget(targetP.transform.position - transform.position);
                    agent.animator.SetBool("Attack", true);
                }
                else
                {
                    NoTarget(targetP);
                }
            }
            if (listPlayer.Count == 0)
            {
                NoTarget(targetP);
            }
        }
    }

    public void NoTarget(BaseSoldier target)
    {
        target = null;
        agent.animator.SetBool("Attack", false);
    }
    public void AttackOnTarget()
    {
        if (agent.isPlayer && targetE != null)
            targetE.TakeDamage(damage);
        if (agent.isEnemy && targetP != null)
            targetP.TakeDamage(damage);
    }

    public void RespawnBolt()
    {
        GameObject bolt = Instantiate(Bolt);
        ArrowAndBolt aab = bolt.GetComponent<ArrowAndBolt>();
        aab.crossbowmanDefender = this;      
        if (agent.isPlayer) 
            aab.target = targetE;
        if (agent.isEnemy)
            aab.target = targetP;
        bolt.transform.position = transform.position;       
    }

    public void GoAttackPoint()
    {
        float distance = Vector3.Distance(transform.position, attackPoint.position);
        if (distance > 0.2f)
            agent.SetDestination(attackPoint.position);
        else
            agent.animator.SetBool("Run", false);
    }

    public void GoDefensePoint()
    {
        float distance = Vector3.Distance(transform.position, defensePoint.position);
        if (distance > 0.2f)
            agent.SetDestination(defensePoint.position);
        else
        {
            agent.animator.SetBool("Run", false);
            agent.RotationOnTarget(attackPoint.position - transform.position);
        }
                  
        agent.animator.SetBool("Attack", false);
    }
}
