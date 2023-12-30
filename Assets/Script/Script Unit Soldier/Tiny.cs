using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiny : BaseSoldier
{
    public Character character;
    public MagicMan daddy;
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
    }

    private void Update()
    {
        if (onDef == true || pushBack == true)
            return;
        TargetOnWho();
        WiOrLo();       
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        if (isDead && agent.isPlayer)
        {
            if (daddy.rally.tinys.Contains(this) == true)
            {
                daddy.rally.tinys.Remove(this);
                daddy.numberOfSoldier--;
            }           
        }
        if (isDead && agent.isEnemy)
        {
            if (testEnemy.rallyE.tinysE.Contains(this) == true)
            {
                testEnemy.rallyE.tinysE.Remove(this);
                daddy.numberOfSoldier--;
            }
                
            
        }
    }
}
