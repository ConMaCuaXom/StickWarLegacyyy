using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public class BaseSoldier : MonoBehaviour
{
    public Agent agent;    
    public BaseEnemy baseEnemy;
    public BasePlayer basePlayer;
    public BaseSoldier targetE;
    public BaseSoldier targetP;
    public BuyUnit buyUnit;

    public float dangerRange;
    public float attackRange;
    public float damage;
    public float hp;
    public float currentHP;
    public float distanceE;
    public float distanceP;
    public float timeToDestroy = 3f;

    public bool isDead = false;
    public bool onAttack = false;
    public bool onDef = false;
    public bool attackOnBase = false;

    public void GoToEnemy(BaseSoldier target,float distanceToEnemy)
    {
        if (target.isDead == false)
        {
            agent.RotationOnTarget(target.transform.position - transform.position);
            if (distanceToEnemy > attackRange)
            {
                agent.obstacle.enabled = false;
                agent.agent.enabled = true;
                agent.agent.isStopped = false;
                agent.SetDestination(target.transform.position);
                agent.animator.SetBool("Attack", false);
            }
            else
            {
                if (agent.agent.enabled == true)
                    agent.agent.isStopped = true;
                agent.agent.enabled = false;
                agent.obstacle.enabled = true;
                agent.animator.SetBool("Run", false);
                agent.animator.SetBool("Attack", true);
                attackOnBase = false;
            }
        } 
        else
            TargetIsDead();

    }

    public virtual void InDangerZone()
    {       
        if (agent.isPlayer)                   
            GoToEnemy(targetE, distanceE);                                  
        if (agent.isEnemy)       
            GoToEnemy(targetP, distanceP);                  
    }

    public void TargetIsDead()
    {        
        onAttack = false;
        agent.animator.SetBool("Attack", false);
        agent.obstacle.enabled = false;
        agent.agent.enabled = true;
    }

    public virtual void AttackOnTarget()
    {
        if (agent.isPlayer && targetE != null)                           
            targetE.TakeDamage(damage);               
        if (agent.isEnemy && targetP != null)                  
            targetP.TakeDamage(damage);        
    }

    public virtual void TakeDamage(float dmg)
    {        
        currentHP -= dmg;       
        if (currentHP <= 0 && isDead == false)
        {
            isDead = true;
            agent.animator.SetTrigger("Death");
            agent.agent.enabled = false;
            if (agent.isEnemy)           
                GameManager.Instance.enemy.Remove(this);            
            if (agent.isPlayer)           
                GameManager.Instance.player.Remove(this);                           
            this.DelayCall(timeToDestroy, () =>
            {
                Destroy(gameObject);
            });
        }
    }

    public virtual void TargetIsNull(BaseSoldier target)
    {             
        onAttack = false;
        agent.animator.SetBool("Attack", false);
        target = null;
        if (onAttack == false && attackOnBase == false)
        {
            agent.obstacle.enabled = false;
            agent.agent.enabled = true;
            agent.agent.isStopped = false;
        }
              
    }
   
    
    public void StopRallyPoint()
    {
        agent.LookAtEnemyBase();
        agent.animator.SetBool("Run", false);                
    }


    private void OnDrawGizmos()
    {
        if (agent != null && agent.isPlayer && targetE != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(targetE.transform.position, transform.position);
        }
    }

    public virtual void TargetOnWho()
    {        
        if (agent.isPlayer == true)
        {
            List<BaseSoldier> listEnemy = GameManager.Instance.enemy.OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToList();
            HowToAttackE(listEnemy);
                       
        }  
        if (agent.isEnemy == true)
        {
            List<BaseSoldier> listPlayer = GameManager.Instance.player.OrderBy(p => Vector3.Distance(transform.position, p.transform.position)).ToList();
            HowToAttackP(listPlayer);
        }
    }

    public virtual void HowToAttackE(List<BaseSoldier> list)
    {
        if (list.Count > 0)
        {
            if (Vector3.Distance(transform.position, list[0].transform.position) <= dangerRange)
            {
                onAttack = true;
                targetE = list[0];
                distanceE = Vector3.Distance(transform.position, targetE.transform.position);
                InDangerZone();
            }
            else
            {
                TargetIsNull(targetE);
            }
        }
        if (list.Count == 0)
        {
            TargetIsNull(targetE);
        }
    }
    public virtual void HowToAttackP(List<BaseSoldier> list)
    {
        if (list.Count > 0)
        {
            if (Vector3.Distance(transform.position, list[0].transform.position) <= dangerRange)
            {
                onAttack = true;
                targetP = list[0];
                distanceP = Vector3.Distance(transform.position, targetP.transform.position);
                InDangerZone();
            }
            else
            {
                TargetIsNull(targetP);
            }
        }
        if (list.Count == 0)
        {
            TargetIsNull(targetP);
        }
    }

    public virtual void AttackOnBaseEnemy()
    {
        if (agent.isPlayer && onAttack == false)
        {
            float distanceToBaseEnemy = Vector3.Distance(transform.position, baseEnemy.transform.position);
            if (distanceToBaseEnemy <= attackRange + 2)
            {
                if (agent.agent.enabled == true)
                    agent.agent.isStopped = true;
                attackOnBase = true;
                agent.agent.enabled = false;
                agent.obstacle.enabled = true;
                agent.animator.SetBool("Run", false);
                agent.animator.SetBool("AttackOnBase", true);                
            } else
            {
                agent.AttackBase();
            }
        }
        if (agent.isEnemy && onAttack == false)
        {
            float distanceToBaseEnemy = Vector3.Distance(transform.position, basePlayer.transform.position);
            if (distanceToBaseEnemy <= attackRange + 2)
            {
                if (agent.agent.enabled == true)
                    agent.agent.isStopped = true;
                agent.agent.enabled = false;
                agent.obstacle.enabled = true;
                agent.animator.SetBool("Run", false);
                agent.animator.SetBool("AttackOnBase", true);
            }
        }
    }

    public virtual void DamageForBase()
    {
        if (agent.isPlayer)
            baseEnemy.TakeDamage(damage);
        if (agent.isEnemy)
            basePlayer.TakeDamage(damage);
    }
}
