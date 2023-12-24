using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpearMan : BaseSoldier
{

    private void Awake()
    {
        agent = GetComponent<Agent>();                
        attackRange = 2.7f;
        dangerRange = 7f;
        damage = 20f;
        hp = 300f;
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
            if (buyUnit.rally.spears.Contains(this) == true)
            {
                buyUnit.rally.spears.Remove(this);
                buyUnit.limitUnitCurrent -= 3;
            }          
        }
        if (isDead && agent.isEnemy)
        {            
            if (testEnemy.rallyE.spearsE.Contains(this) == true)
            {
                testEnemy.rallyE.spearsE.Remove(this);
                testEnemy.limitUnitCurrent -= 3;
            }
                
        }
    }
}
