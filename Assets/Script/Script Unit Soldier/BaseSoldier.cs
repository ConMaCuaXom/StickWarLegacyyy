using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseSoldier : MonoBehaviour
{
    public Agent agent;
    public BaseSoldier targetE;
    public BaseSoldier targetP;
    public BuyUnit buyUnit => GameManager.Instance.buyUnit;
    public TestEnemy testEnemy => GameManager.Instance.testEnemy;
    public WinOrLose wol => GameManager.Instance.winOrLose;

    

    public float dangerRange;
    public float attackRange;
    public float damage;
    public float hp;
    public float currentHP;
    public float distanceE;
    public float distanceP;
    public float timeToDestroy = 5f;
    public float timeToPush;
    public float timeFighting;

    public bool isDead = false;
    public bool onAttack = false;
    public bool onDef = false;
    public bool attackOnBase = false;
    public bool pushBack = false;
    public bool hulolo = false;
    public bool attacking = false;
    public bool fighting = false;
    public virtual void TargetOnWho()
    {
        if (agent.isPlayer == true && GameManager.Instance.enemy != null)
        {
            List<BaseSoldier> listEnemy = GameManager.Instance.enemy.OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToList();
            HowToAttackE(listEnemy);
        }
        if (agent.isEnemy == true && GameManager.Instance.player != null)
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
                TargetIsNull();
            }
            if (Vector3.Distance(transform.position, list[0].transform.position) >= Vector3.Distance(transform.position, agent.baseEnemy.transform.position) + 2)
            {
                onAttack = false;
                AttackOnBaseEnemy();
            }
        }

        if (list.Count == 0)
        {
            TargetIsNull();
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
                TargetIsNull();
            }
            if (Vector3.Distance(transform.position, list[0].transform.position) >= Vector3.Distance(transform.position, agent.basePlayer.transform.position) + 2)
            {
                onAttack = false;
                AttackOnBaseEnemy();
            }
        }

        if (list.Count == 0)
        {
            TargetIsNull();
        }
    }

    public virtual void InDangerZone()
    {
        if (agent.isPlayer)
            GoToEnemy(targetE, distanceE);
        if (agent.isEnemy)
            GoToEnemy(targetP, distanceP);
    }
    public virtual void GoToEnemy(BaseSoldier target, float distanceToEnemy)
    {
        if (target.isDead == false)
        {            
            if (distanceToEnemy > attackRange && attacking == false)
            {                              
                agent.agent.isStopped = false;
                agent.SetDestination(target.transform.position);
                agent.animator.SetBool("Attack", false);
            }
            else
            {
                attacking = true;
                agent.RotationOnTarget(target.transform.position - transform.position);               
                agent.agent.isStopped = true;                             
                agent.animator.SetBool("Run", false);
                agent.animator.SetBool("Attack", true);
                RandomAttack();
                attackOnBase = false;
            }
        }
        else
            TargetIsDead();
    }

    public void AttackingOff()
    {
        attacking = false;
    }

    public void Fighting()
    {
        fighting = true;
    }

    public virtual void RandomAttack()
    {
        int rd = Random.Range(0, 2);
        if (rd == 0)
        {
            agent.animator.SetBool("Attack0", true);
            agent.animator.SetBool("Attack1", false);
        }
        if (rd == 1)
        {
            agent.animator.SetBool("Attack0", false);
            agent.animator.SetBool("Attack1", true);
        }
    }

    public virtual void RandomDeath()
    {
        int rd = Random.Range(0, 2);
        if (rd == 0)       
            agent.animator.SetTrigger("Death1");                    
        if (rd == 1)       
            agent.animator.SetTrigger("Death2");                   
    }   

    public void TargetIsDead()
    {        
        onAttack = false;
        agent.animator.SetBool("Attack", false);                     
    }

    public virtual void AttackOnTarget()
    {
        fighting = true;
        timeFighting = 0;
        if (agent.isPlayer && targetE != null)
            targetE.TakeDamage(damage);               
        if (agent.isEnemy && targetP != null)
            targetP.TakeDamage(damage);        
    }

    public virtual void TakeDamage(float dmg)
    {        
        currentHP -= dmg;   
        fighting = true;
        timeFighting = 0;
        if (currentHP <= 0 && isDead == false)
        {
            isDead = true;
            RandomDeath();
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

    public virtual void TargetIsNull()
    {             
        onAttack = false;
        agent.animator.SetBool("Attack", false);
        if (agent.isEnemy)
            targetP = null;
        if (agent.isPlayer)
            targetE = null;
        if (onAttack == false && attackOnBase == false)                               
            agent.agent.isStopped = false;                    
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
        if (agent != null && agent.isEnemy && targetP != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(targetP.transform.position, transform.position);           
        }
    } 

    public virtual void AttackOnBaseEnemy()
    {
        if (agent.isPlayer && onAttack == false)
        {                  
            if (((transform.position.x >= agent.baseEnemy.transform.position.x - 3) && (transform.position.x <= agent.baseEnemy.transform.position.x + 3)) &&
                    ((transform.position.z >= agent.baseEnemy.transform.position.z - 3) && (transform.position.z <= agent.baseEnemy.transform.position.z + 3)))           
            {               
                agent.agent.isStopped = true;
                attackOnBase = true;
                agent.animator.SetBool("Run", false);
                agent.animator.SetBool("AttackOnBase", true);
                agent.LookAtEnemyBase();
            } else
            {
                agent.AttackBase();
            }
        }
        if (agent.isEnemy && onAttack == false)
        {
            if (((transform.position.x >= agent.basePlayer.transform.position.x - 3) && (transform.position.x <= agent.basePlayer.transform.position.x + 3)) &&
                    ((transform.position.z >= agent.basePlayer.transform.position.z - 3) && (transform.position.z <= agent.basePlayer.transform.position.z + 3)))
            {                
                agent.agent.isStopped = true;
                attackOnBase = true;
                agent.animator.SetBool("Run", false);
                agent.animator.SetBool("AttackOnBase", true);
                agent.LookAtEnemyBase();
            }
            else
            {
                agent.AttackBase();
            }
        }
    }

    public virtual void DamageForBase()
    {
        if (agent.isPlayer)
            agent.baseEnemy.TakeDamage(damage);
        if (agent.isEnemy)
            agent.basePlayer.TakeDamage(damage);
    }

    public virtual void PushBack()
    {
        pushBack = true;       
        agent.agent.isStopped = true;
        this.DelayCall(2f, () =>
        {            
            pushBack = false;
            agent.agent.isStopped = false;
        });        
        if (currentHP > 0) 
            agent.animator.SetTrigger("PushBack");
        agent.animator.SetBool("Run", false);
        agent.animator.SetBool("AttackOnBase", false);
        agent.animator.SetBool("Attack", false);
    }

    public virtual void WiOrLo()
    {
        if (agent.isPlayer && wol.playerWin == true)
        {
            agent.animator.SetBool("Victory", true);
        }
        if (agent.isEnemy && wol.playerLose == true)
        {
            agent.animator.SetBool("Victory", true);
        }
    }

    public virtual void OnFighting()
    {
        if (fighting == true)
        {
            timeFighting += Time.deltaTime;
            if (timeFighting >= 5f)
                fighting = false;
        }
    }
}
