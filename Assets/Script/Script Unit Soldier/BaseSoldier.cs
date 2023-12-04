using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseSoldier : MonoBehaviour
{
    public Agent agent;    
    public BaseEnemy baseEnemy;
    public BasePlayer basePlayer;
    public BaseSoldier targetE;
    public List<BaseSoldier> targetEE;
    public BaseSoldier targetP;
    public List<BaseSoldier> targetPP;
    public BuyUnit buyUnit;
    public TestEnemy testEnemy;
    public WinOrLose wol;

    public float dangerRange;
    public float attackRange;
    public float damage;
    public float hp;
    public float currentHP;
    public float distanceE;
    public float distanceP;
    public float timeToDestroy = 3f;
    public float timeToPush;

    public bool isDead = false;
    public bool onAttack = false;
    public bool onDef = false;
    public bool attackOnBase = false;
    public bool pushBack = false;
    public bool hulolo = false;
    


    //public virtual void GoToEnemy(BaseSoldier target,float distanceToEnemy)
    //{
    //    if (target.isDead == false)
    //    {
    //        agent.RotationOnTarget(target.transform.position - transform.position);
    //        if (distanceToEnemy > attackRange)
    //        {
    //            agent.obstacle.enabled = false;
    //            agent.agent.enabled = true;
    //            agent.agent.isStopped = false;
    //            agent.SetDestination(target.transform.position);
    //            agent.animator.SetBool("Attack", false);
    //        }
    //        else
    //        {
    //            if (agent.agent.enabled == true)
    //                agent.agent.isStopped = true;
    //            agent.agent.enabled = false;
    //            agent.obstacle.enabled = true;
    //            agent.animator.SetBool("Run", false);
    //            agent.animator.SetBool("Attack", true);
    //            RandomAttack();               
    //            attackOnBase = false;
    //        }
    //    } 
    //    else
    //        TargetIsDead();

    //}

    
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



    //public virtual void InDangerZone()
    //{       
    //    if (agent.isPlayer)                   
    //        GoToEnemy(targetE, distanceE);                                  
    //    if (agent.isEnemy)       
    //        GoToEnemy(targetP, distanceP);                  
    //}

    public void TargetIsDead(List<BaseSoldier> listTarget)
    {        
        onAttack = false;
        agent.animator.SetBool("Attack", false);
        agent.obstacle.enabled = false;
        agent.agent.enabled = true;
        listTarget.RemoveAt(0);
    }

    public virtual void AttackOnTarget()
    {
        if (agent.isPlayer && targetEE.Count > 0)
            targetEE[0].TakeDamage(damage);               
        if (agent.isEnemy && targetPP != null)
            targetPP[0].TakeDamage(damage);        
    }

    public virtual void TakeDamage(float dmg)
    {        
        currentHP -= dmg;       
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
        if (agent != null && agent.isEnemy && targetP != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(targetP.transform.position, transform.position);           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (agent.isPlayer && other.CompareTag("Enemy"))
        {
            BaseSoldier targetE0 = other.gameObject.GetComponent<BaseSoldier>();
            targetEE.Add(targetE0);
        }
        if (agent.isEnemy && other.CompareTag("Player"))
        {
            BaseSoldier targetP0 = other.gameObject.GetComponent<BaseSoldier>();
            targetPP.Add(targetP0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (targetEE.Count >0 && agent.isPlayer && other.CompareTag("Enemy"))
        {
            targetEE.OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToList();
            onAttack = true;
            if (targetEE[0].isDead == false)
            {
                Debug.Log(Vector3.Distance(transform.position, targetEE[0].transform.position));
                Debug.Log("transform.position: " + transform.position);
                Debug.Log("targetEE[0].transform.position" + targetEE[0].transform.position);
                agent.RotationOnTarget(targetEE[0].transform.position - transform.position);
                if (Vector3.Distance(transform.position, targetEE[0].transform.position) > attackRange)
                {
                    agent.obstacle.enabled = false;
                    agent.agent.enabled = true;
                    agent.agent.isStopped = false;
                    agent.SetDestination(targetEE[0].transform.position);
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
                    RandomAttack();
                    attackOnBase = false;
                }
            }
            else
            {
                TargetIsDead(targetEE);               
            }
                
        }
        if (targetPP != null && agent.isEnemy && other.CompareTag("Player"))
        {
            targetPP.OrderBy(p => Vector3.Distance(transform.position, p.transform.position)).ToList();
            onAttack = true;
            if (targetPP[0].isDead == false)
            {
                agent.RotationOnTarget(targetPP[0].transform.position - transform.position);
                if (Vector3.Distance(transform.position, targetPP[0].transform.position) > attackRange)
                {
                    agent.obstacle.enabled = false;
                    agent.agent.enabled = true;
                    agent.agent.isStopped = false;
                    agent.SetDestination(targetPP[0].transform.position);
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
                    RandomAttack();
                    attackOnBase = false;
                }
            }
            else
            {
                TargetIsDead(targetPP);
            }

        }
    }
    //public virtual void TargetOnWho()
    //{        
    //    if (agent.isPlayer == true)
    //    {
    //        List<BaseSoldier> listEnemy = GameManager.Instance.enemy.OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToList();
    //        HowToAttackE(listEnemy);
                       
    //    }  
    //    if (agent.isEnemy == true && GameManager.Instance.player != null)
    //    {
    //        List<BaseSoldier> listPlayer = GameManager.Instance.player.OrderBy(p => Vector3.Distance(transform.position, p.transform.position)).ToList();
    //        HowToAttackP(listPlayer);
    //    }
    //}

    //public virtual void HowToAttackE(List<BaseSoldier> list)
    //{
    //    if (list.Count > 0)
    //    {
    //        if (Vector3.Distance(transform.position, list[0].transform.position) <= dangerRange)
    //        {
    //            onAttack = true;
    //            targetE = list[0];
    //            distanceE = Vector3.Distance(transform.position, targetE.transform.position);
    //            InDangerZone();
    //        }
    //        else
    //        {
    //            TargetIsNull();
    //        }
    //        if (Vector3.Distance(transform.position, list[0].transform.position) >= Vector3.Distance(transform.position, baseEnemy.transform.position + Vector3.right*(-2)))
    //        {
    //            onAttack = false;
    //            AttackOnBaseEnemy();
    //        }
    //    }
        
    //    if (list.Count == 0)
    //    {
    //        TargetIsNull();
    //    }
    //}
    //public virtual void HowToAttackP(List<BaseSoldier> list)
    //{
    //    if (list.Count > 0)
    //    {
    //        if (Vector3.Distance(transform.position, list[0].transform.position) <= dangerRange)
    //        {
    //            onAttack = true;
    //            targetP = list[0];
    //            distanceP = Vector3.Distance(transform.position, targetP.transform.position);
    //            InDangerZone();
    //        }
    //        else
    //        {
    //            TargetIsNull();
    //        }
    //        if (Vector3.Distance(transform.position, list[0].transform.position) >= Vector3.Distance(transform.position, basePlayer.transform.position + Vector3.right * 2))
    //        {
    //            onAttack = false;
    //            AttackOnBaseEnemy();
    //        }
    //    }
        
    //    if (list.Count == 0)
    //    {
    //        TargetIsNull();
    //    }
    //}

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
                attackOnBase = true;
                agent.agent.enabled = false;
                agent.obstacle.enabled = true;
                agent.animator.SetBool("Run", false);
                agent.animator.SetBool("AttackOnBase", true);
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
            baseEnemy.TakeDamage(damage);
        if (agent.isEnemy)
            basePlayer.TakeDamage(damage);
    }

    public virtual void PushBack()
    {
        pushBack = true;
        this.DelayCall(2f, () =>
        {            
            pushBack = false;
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

    
}
