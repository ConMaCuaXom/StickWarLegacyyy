using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Titan : BaseSoldier
{

    private void Awake()
    {
        agent = GetComponent<Agent>();              
        attackRange = 2.7f;
        dangerRange = 7f;
        damage = 30f;
        hp = 1000f;
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
        OnFighting();
        
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
            for (int i = GameManager.Instance.enemy.Count - 1; i >= 0; i--)
            {
                BaseSoldier soldier = GameManager.Instance.enemy[i];
                if (Vector3.Distance(soldier.transform.position, targetE.transform.position) <= 2f && soldier != null)
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
            for(int i = GameManager.Instance.player.Count - 1; i >= 0; i--)
            {
                BaseSoldier soldier = GameManager.Instance.player[i];
                if (Vector3.Distance(soldier.transform.position, targetP.transform.position) <= 2f && soldier != null)
                {
                    soldier.TakeDamage(damage);
                    soldier.PushBack();
                }
            }            
        }            
    }
}
