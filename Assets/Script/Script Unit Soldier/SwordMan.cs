using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwordMan : BaseSoldier
{
    private void Awake()
    {
        agent = GetComponent<Agent>();               
        attackRange = 2.5f;
        dangerRange = 7f;
        damage = 15f;
        hp = 100f;
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
