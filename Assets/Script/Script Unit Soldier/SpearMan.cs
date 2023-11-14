using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpearMan : BaseSoldier
{

    private void Awake()
    {
        agent = GetComponent<Agent>();        
        basePlayer = GameManager.Instance.basePlayer;
        baseEnemy = GameManager.Instance.baseEnemy;
        buyUnit = GameManager.Instance.buyUnit;
        attackRange = 3f;
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
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        if (isDead && agent.isPlayer)
        {
            buyUnit.rally.spears.Remove(this);
            buyUnit.limitUnitCurrent -= 3;
        }
    }
}
