using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwordMan : BaseSoldier
{
    private void Awake()
    {
        agent = GetComponent<Agent>();        
        basePlayer = GameManager.Instance.basePlayer;
        baseEnemy = GameManager.Instance.baseEnemy;
        buyUnit = GameManager.Instance.buyUnit;
        attackRange = 1.5f;
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
        if (pushBack == true)
        {
            ActionInPush();
        }
        if (isDead == true || onDef == true || pushBack == true)
            return;
        TargetOnWho();       
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        if (isDead && agent.isPlayer)
        {
            buyUnit.rally.swords.Remove(this);
            buyUnit.limitUnitCurrent--;
        }
    }
}
