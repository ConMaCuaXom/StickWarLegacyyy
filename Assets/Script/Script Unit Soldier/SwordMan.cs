using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwordMan : BaseSoldier
{
    private void Awake()
    {
        agent = GetComponent<Agent>();               
        attackRange = 2f;
        dangerRange = 10f;
        damage = 5f;
        hp = 2000f;
        currentHP = hp;
        isDead = false;
        onAttack = false;
        onDef = false;
    }

    private void Update()
    {
        
        if (isDead == true || onDef == true || pushBack == true)
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
