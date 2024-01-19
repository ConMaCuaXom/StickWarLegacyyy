using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Archer : BaseSoldier
{
    public Character character;   
    public GameObject Arrow;
    public TargetDynamicSound targetDynamicSound = null;
    private void Awake()
    {
        agent = GetComponent<Agent>();                       
        attackRange = character.attackRange;
        dangerRange = character.dangerRange;
        damage = character.attackDamage;
        hp = character.hp;
        timeToDestroy = character.timeToDestroy;
        currentHP = hp;
        targetDynamicSound = GetComponent<TargetDynamicSound>();
        targetDynamicSound.Initialized();
        isDead = false;
        onAttack = false;
        WhichBody(PlayerPrefs.GetString("ArcherBody"));
        WhichHead(PlayerPrefs.GetString("ArcherHead"));
        WhichWeapon(PlayerPrefs.GetString("ArcherWeapon"));
    }

    private void Update()
    {
        HPinCamera();
        if (onDef == true || isDead == true || pushBack == true)
            return;
        TargetOnWho();
        WiOrLo();
        

    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        if (isDead && agent.isPlayer)
        {
            if(buyUnit.rally.archers.Contains(this) == true)
            {
                buyUnit.rally.archers.Remove(this);
                buyUnit.limitUnitCurrent--;
            }           
        }
        if (isDead && agent.isEnemy)
        {
            if (testEnemy.rallyE.archersE.Contains(this) == true)
            {
                testEnemy.rallyE.archersE.Remove(this);
                testEnemy.limitUnitCurrent--;
            }
                           
        }

    }

    public void RespawnArrow()
    {
        GameObject arrow = Instantiate(Arrow);
        ArrowAndBolt aab = arrow.GetComponent<ArrowAndBolt>();
        aab.archer = this;
        if (agent.isPlayer)
        {
            aab.target = targetE;
            aab.isPlayer = true;
        }
            
        if (agent.isEnemy)
        {
            aab.target = targetP;
            aab.isPlayer = false;
        }          
        arrow.transform.position = transform.position;
        AttackSound();
    }

    public void RespawnArrowBase()
    {
        GameObject arrow = Instantiate(Arrow);
        ArrowAndBolt aab = arrow.GetComponent<ArrowAndBolt>();
        aab.archer = this;
        if (agent.isPlayer)
            aab.isPlayer = true;
        if (agent.isEnemy)
            aab.isPlayer = false;
        arrow.transform.position = transform.position;
        AttackSound();
    }

    public override void HowToAttackE(List<BaseSoldier> list)
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
            if (targetE != null && baseEnemy.currentHP > 0)
            {
                if (Vector3.Distance(transform.position, agent.baseEnemy.transform.position) <= Vector3.Distance(transform.position, targetE.transform.position))
                {
                    onAttack = false;
                    AttackOnBaseEnemy();
                }
            } else
            {
                if (Vector3.Distance(transform.position, agent.baseEnemy.transform.position) <= attackRange)
                    AttackOnBaseEnemy();
            }          
        }

        if (list.Count == 0)
        {
            TargetIsNull();
        }
    }

    public override void HowToAttackP(List<BaseSoldier> list)
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
            if (targetP != null)
            {
                if (Vector3.Distance(transform.position, agent.basePlayer.transform.position) <= Vector3.Distance(transform.position, targetP.transform.position))
                {
                    onAttack = false;
                    AttackOnBaseEnemy();
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, agent.basePlayer.transform.position) <= attackRange)
                    AttackOnBaseEnemy();
            }
        }
        if (list.Count == 0)
        {
            TargetIsNull();
        }
    }

    public override void AttackOnBaseEnemy()
    {
        if (baseEnemy.currentHP <= 0 || agent.agent.enabled == false)
            return;
        if (agent.isPlayer && onAttack == false)
        {
            if (Vector3.Distance(transform.position,agent.baseEnemy.transform.position) <= attackRange)
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
            if (Vector3.Distance(transform.position, agent.basePlayer.transform.position) <= attackRange)
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

    public override void TargetIsNull()
    {
        onAttack = false;
        if (agent.isPlayer && Vector3.Distance(transform.position, agent.baseEnemy.transform.position) >= attackRange && agent.agent.enabled == true)
            agent.agent.isStopped = false;
        if (agent.isEnemy && Vector3.Distance(transform.position, agent.basePlayer.transform.position) >= attackRange && agent.agent.enabled == true)
            agent.agent.isStopped = false;
        agent.animator.SetBool("Attack", false);
        if (agent.isEnemy)       
            targetP = null;       
        if (agent.isPlayer)
            targetE = null;
    }

    public override void AttackingOff()
    {
        attacking = false;
        //if (agent.agent.enabled == true && attackOnBase == false)
        //    agent.agent.isStopped = false;
        agent.agent.enabled = true;
    }
    public void AttackSound()
    {
        soundDynamic.PlayOneShot("Archer_shot");
    }
}
