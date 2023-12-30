using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Titan : BaseSoldier
{
    public Character character;
    public float aoePushBack => character.aoePushBack;
    private void Awake()
    {
        agent = GetComponent<Agent>();              
        attackRange = character.attackRange;
        dangerRange = character.dangerRange;
        damage = character.attackDamage;
        hp = character.hp;
        timeToDestroy = character.timeToDestroy;
        currentHP = hp;
        isDead = false;
        onAttack = false;
    }

    private void Update()
    {
        if (onDef == true)
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
            for (int i = GameManager.Instance.enemy.Count - 1; i >= 0; i--)
            {
                BaseSoldier soldier = GameManager.Instance.enemy[i];
                if (Vector3.Distance(soldier.transform.position, targetE.transform.position) <= aoePushBack && soldier != null)
                {
                    soldier.TakeDamage(damage);
                    soldier.PushBack();
                }
            }           
        }           
        if (agent.isEnemy && targetP != null)
        {                      
            for(int i = GameManager.Instance.player.Count - 1; i >= 0; i--)
            {
                BaseSoldier soldier = GameManager.Instance.player[i];
                if (Vector3.Distance(soldier.transform.position, targetP.transform.position) <= aoePushBack && soldier != null)
                {
                    soldier.TakeDamage(damage);
                    soldier.PushBack();
                }
            }            
        }            
    }
}
