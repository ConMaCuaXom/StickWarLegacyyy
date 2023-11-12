using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BaseSoldier : MonoBehaviour
{
    public Agent agent;
    public Animator animator;
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

    public virtual void InDangerZone()
    {
        NoTargetAlive();
        if (agent.isPlayer && targetE.isDead == false)
        {
            agent.RotationOnTarget(targetE.transform.position - transform.position);
            if (distanceE > attackRange)
            {
                agent.agent.isStopped = true;
                agent.MoveForward();                
                animator.SetBool("Run", true);
                animator.SetBool("Attack", false);
            }                       
            else
            {               
                agent.agent.isStopped = true;
                animator.SetBool("Run", false);
                animator.SetBool("Attack", true);
            }                     
        }
        if (agent.isEnemy && targetP.isDead == false)
        {
            agent.RotationOnTarget(targetP.transform.position - transform.position);
            if (distanceP > attackRange)
            {
                agent.MoveForward();
                animator.SetBool("Run", true);
                animator.SetBool("Attack", false);
            }
            else
            {
                onAttack = true;
                animator.SetBool("Run", false);
                animator.SetBool("Attack", true);
            }
        }


    }

    public void NoTargetAlive()
    {
        if ((agent.isPlayer && targetE.isDead == true) || (agent.isEnemy && targetP.isDead == true))
        {
            onAttack = false;
            animator.SetBool("Attack", false);
        }
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
        onAttack = true;       
    }

    public void GoToRallyPoint()
    {        
        agent.LookAtEnemyBase();
        animator.SetBool("Run", true);
    }
    public void StopRallyPoint()
    {
        agent.LookAtEnemyBase();
        animator.SetBool("Run", false);                
    }


    private void OnDrawGizmos()
    {
        if (agent != null && agent.isPlayer && targetE != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(targetE.transform.position, transform.position);
        }
    }
}
