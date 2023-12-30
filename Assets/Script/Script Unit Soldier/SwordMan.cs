using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwordMan : BaseSoldier
{
    public Character character;
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
        onDef = false;
        nearBase = false;
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
            if (buyUnit.rally.swords.Contains(this) == true)
            {
                buyUnit.rally.swords.Remove(this);
                buyUnit.limitUnitCurrent--;
            }          
        }
        if (isDead && agent.isEnemy)
        {
            if (testEnemy.rallyE.swordsE.Contains(this) == true)
            {
                testEnemy.rallyE.swordsE.Remove(this);
                testEnemy.limitUnitCurrent--;
            }
                
        }
    }
}
