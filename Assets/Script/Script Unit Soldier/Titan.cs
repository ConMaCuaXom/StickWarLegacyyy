using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Titan : BaseSoldier
{

    private void Awake()
    {
        agent = GetComponent<Agent>();        
        basePlayer = GameManager.Instance.basePlayer;
        baseEnemy = GameManager.Instance.baseEnemy;
        buyUnit = GameManager.Instance.buyUnit;
        testEnemy = GameManager.Instance.testEnemy;
        wol = GameManager.Instance.winOrLose;
        attackRange = 2.7f;
        dangerRange = 10f;
        damage = 5f;
        hp = 200f;
        currentHP = hp;
        isDead = false;
        onAttack = false;
    }

    private void Update()
    {
        if (isDead == true || onDef == true)
            return;
        TargetOnWho();
        WiOrLo();
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        if (isDead && agent.isPlayer)
        {
            if (buyUnit.rally.titans.Contains(this) == true)
            {
                buyUnit.rally.titans.Remove(this);
                buyUnit.limitUnitCurrent -= 3;
            }          
        }
        if (isDead && agent.isEnemy)
        {
            if (testEnemy.rallyE.titansE.Contains(this) == true)
            {
                testEnemy.rallyE.titansE.Remove(this);
                testEnemy.limitUnitCurrent -= 3;
            }
                
        }
    }

    public override void AttackOnTarget()
    {
        if (agent.isPlayer && targetE != null)
        {
            targetE.TakeDamage(damage);
            targetE.PushBack();
            foreach (BaseSoldier soldier in GameManager.Instance.enemy)
            {
                if (Vector3.Distance(soldier.transform.position, targetE.transform.position) <= 2f)
                {
                    soldier.TakeDamage(damage);
                    soldier.PushBack();
                }
            }
        }
            
        if (agent.isEnemy && targetP != null)
        {
            targetP.TakeDamage(damage);
            targetP.PushBack();
            foreach (BaseSoldier soldier in GameManager.Instance.player)
            {
                if (Vector3.Distance(soldier.transform.position, targetP.transform.position) <= 2f)
                {
                    soldier.TakeDamage(damage);
                    soldier.PushBack();
                }
            }
        }
            
    }
}
