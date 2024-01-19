using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class BaseSoldier : MonoBehaviour
{
    public Agent agent;
    public BaseSoldier targetE;
    public BaseSoldier targetP;

    public BaseEnemy baseEnemy => GameManager.Instance.baseEnemy;
    public BuyUnit buyUnit => GameManager.Instance.buyUnit;
    public TestEnemy testEnemy => GameManager.Instance.testEnemy;
    public WinOrLose wol => GameManager.Instance.winOrLose;
    public SoundDynamic soundDynamic => AudioManager.Instance.dynamicSoundActive[transform];

    public Image hpImage;
    public Canvas canvas;

    public enum Death
    {
        Death1,
        Death2
    }

    public enum SoundDeath
    {
        soldier_die_01,
        soldier_die_02,
        soldier_die_03,
        soldier_die_04,
        soldier_die_05,
        soldier_die_06,
        soldier_die_07,
        soldier_die_08,
        soldier_die_09,
        soldier_die_10
    }

    public float dangerRange;
    public float attackRange;
    public float damage;
    public float hp;
    public float currentHP;
    public float distanceE;
    public float distanceP;
    public float timeToDestroy;
    public float timeToPush;
    

    public bool isDead = false;
    public bool onAttack = false;
    public bool onDef = false;
    public bool attackOnBase = false;
    public bool pushBack = false;
    public bool hulolo = false;
    public bool attacking = false;
    
    public bool onRally = false;
    public bool nearBase = false;

    public List<GameObject> head = null;
    public List<GameObject> body = null;
    public List<GameObject> weapon = null;
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
            if (nearBase && baseEnemy.currentHP > 0)
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
            if (nearBase)
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
        if (pushBack || wol.playerLose || wol.playerWin)
            return;
            if (distanceToEnemy > attackRange && attacking == false && isDead == false)
            {         
                if (agent.agent.enabled == true)
                    agent.agent.isStopped = false;
                agent.SetDestination(target.transform.position);
                agent.animator.SetBool("Attack", false);
            }
            if (distanceToEnemy <= attackRange)
            {
                if (agent.agent.enabled == true)
                    agent.agent.isStopped = true;
                agent.RotationOnTarget(target.transform.position - transform.position);                                                           
                agent.animator.SetBool("Run", false);
                agent.animator.SetBool("Attack", true);
                RandomAttack();
                attackOnBase = false;
            }             
    }

    public virtual void AttackingOff()
    {
        attacking = false;
        //if (agent.agent.enabled == true)
        //    agent.agent.isStopped = false;   
        agent.agent.enabled = true;
    }

    public void AttackingOn()
    {
        attacking = true;
        //if (agent.agent.enabled == true)
        //    agent.agent.isStopped = true;
        if (agent.agent.enabled == true)
            agent.agent.enabled = false;
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
        Death rdDeath = (Death)Random.Range(0, 2);                                          
        agent.animator.SetTrigger(rdDeath.ToString());
        SoundDeath rdSoundDeath = (SoundDeath)Random.Range(0, 10);
        soundDynamic.PlayOneShot(rdSoundDeath.ToString());
    }   

    //public void TargetIsDead()
    //{        
    //    onAttack = false;
    //    agent.animator.SetBool("Attack", false);       
    //}

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
        hpImage.gameObject.SetActive(true);
        float ratio = currentHP / hp;
        hpImage.DOFillAmount(ratio,0.2f);
        if (currentHP <= 0 && isDead == false)
        {
            isDead = true;
            RandomDeath();
            //agent.agent.isStopped = true;
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

    public virtual void DeleteThis()
    {
        currentHP = 0;
        isDead = true;
        RandomDeath();
        if (agent.agent.enabled == true)
            agent.agent.isStopped = true;
        GameManager.Instance.enemy.Remove(this);
        this.DelayCall(timeToDestroy, () =>
        {
            Destroy(gameObject);
        });
    }

    public virtual void TargetIsNull()
    {             
        onAttack = false;
        if (nearBase == false && agent.agent.enabled == true)
            agent.agent.isStopped = false;
        agent.animator.SetBool("Attack", false);
        if (agent.isEnemy)
        {
            targetP = null;           
        }          
        if (agent.isPlayer)
            targetE = null;                          
    }     
    public void StopRallyPoint()
    {
        agent.LookAtEnemyBase();
        agent.animator.SetBool("Run", false);
        onRally = true;
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
        if (agent.isPlayer && other.CompareTag("BaseEnemy") || agent.isEnemy && other.CompareTag("BasePlayer"))
            nearBase = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (agent.isPlayer && other.CompareTag("BaseEnemy") || agent.isEnemy && other.CompareTag("BasePlayer"))
            nearBase = false;
    }


    public virtual void AttackOnBaseEnemy()
    {
        if (pushBack || baseEnemy.currentHP <= 0 || agent.agent.enabled == false)
            return;
        if (agent.isPlayer && onAttack == false)
        {                  
            if (nearBase == true)
            {
                if (agent.agent.enabled == true)
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
        if (agent.isEnemy && onAttack == false)
        {
            if (nearBase)
            {            
                if (agent.agent.enabled == true)
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
        if (agent.isPlayer && agent.baseEnemy.currentHP > 0)
            agent.baseEnemy.TakeDamage(damage - 10f);
        if (agent.isEnemy && agent.basePlayer.currentHP > 0)
            agent.basePlayer.TakeDamage(damage - 10f);
    }

    public virtual void PushBack()
    {
        pushBack = true;
        agent.agent.enabled = false;
            //agent.agent.isStopped = true;
        //this.DelayCall(2f, () =>
        //{            
        //    pushBack = false;
        //    agent.agent.isStopped = false;
        //});        
        if (currentHP > 0) 
            agent.animator.SetTrigger("PushBack");
        agent.animator.SetBool("Run", false);
        agent.animator.SetBool("AttackOnBase", false);
        agent.animator.SetBool("Attack", false);
    }

    public virtual void PushBackEnd()
    {
        agent.agent.enabled = true;
            //agent.agent.isStopped = false;
        pushBack = false;
        attacking = false;
    }

    public virtual void WiOrLo()
    {
        
        if (wol.playerWin == true)
        {
            if (agent.isPlayer)
            {
                agent.agent.enabled = false;
                agent.animator.SetBool("Victory", true);
            }
            if (agent.isEnemy)
            {
                agent.agent.enabled = false;
                agent.animator.SetBool("Run", false);
                agent.animator.SetBool("Attack", false);
            }
            
        }
        if (wol.playerLose == true)
        {
            if (agent.isEnemy)
            {
                agent.agent.enabled = false;
                agent.animator.SetBool("Victory", true);
            }
            if (agent.isPlayer)
            {
                agent.agent.enabled = false;
                agent.animator.SetBool("Run", false);
                agent.animator.SetBool("Attack", false);
            }
            
        }
    }
    public void WhichBody(string bodyy)
    {
        if (bodyy == "Body0")
        {
            body[0].SetActive(true);
            body[1].SetActive(false);
            body[2].SetActive(false);
            body[3].SetActive(false);
        }
        if (bodyy == "Body1")
        {
            body[0].SetActive(false);
            body[1].SetActive(true);
            body[2].SetActive(false);
            body[3].SetActive(false);
        }
        if (bodyy == "Body2")
        {
            body[0].SetActive(false);
            body[1].SetActive(false);
            body[2].SetActive(true);
            body[3].SetActive(false);
        }
        if (bodyy == "Body3")
        {
            body[0].SetActive(false);
            body[1].SetActive(false);
            body[2].SetActive(false);
            body[3].SetActive(true);
        }
    }

    public void WhichHead(string headd)
    {
        if (headd == "Head0")
        {
            head[0].SetActive(true);
            head[1].SetActive(false);
            head[2].SetActive(false);
            head[3].SetActive(false);
        }
        if (headd == "Head1")
        {
            head[0].SetActive(false);
            head[1].SetActive(true);
            head[2].SetActive(false);
            head[3].SetActive(false);
        }
        if (headd == "Head2")
        {
            head[0].SetActive(false);
            head[1].SetActive(false);
            head[2].SetActive(true);
            head[3].SetActive(false);
        }
        if (headd == "Head3")
        {
            head[0].SetActive(false);
            head[1].SetActive(false);
            head[2].SetActive(false);
            head[3].SetActive(true);
        }
    }
    public void WhichWeapon(string weaponn)
    {
        if (weaponn == "Weapon0")
        {
            weapon[0].SetActive(true);
            weapon[1].SetActive(false);
            weapon[2].SetActive(false);
            weapon[3].SetActive(false);
        }
        if (weaponn == "Weapon1")
        {
            weapon[0].SetActive(false);
            weapon[1].SetActive(true);
            weapon[2].SetActive(false);
            weapon[3].SetActive(false);
        }
        if (weaponn == "Weapon2")
        {
            weapon[0].SetActive(false);
            weapon[1].SetActive(false);
            weapon[2].SetActive(true);
            weapon[3].SetActive(false);
        }
        if (weaponn == "Weapon3")
        {
            weapon[0].SetActive(false);
            weapon[1].SetActive(false);
            weapon[2].SetActive(false);
            weapon[3].SetActive(true);
        }
    }

    //public virtual void OnFighting()
    //{
    //    if (fighting == true)
    //    {
    //        timeFighting += Time.deltaTime;
    //        if (timeFighting >= 5f)
    //            fighting = false;
    //    }
    //}

    public void HPinCamera()
    {
        hpImage.transform.forward = GameManager.Instance.mainCamera.transform.forward;
    }

    public void WhichColor()
    {
        if (agent.isEnemy)
            hpImage.color = new Color(255f, 0f, 0f, 255f);
        if (agent.isPlayer)
            hpImage.color = new Color(0f, 255f, 176f, 255f);
    }

    
}
