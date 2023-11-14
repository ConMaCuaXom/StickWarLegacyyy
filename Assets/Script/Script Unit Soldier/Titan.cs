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
        attackRange = 5f;
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
            buyUnit.rally.titans.Remove(this);
            buyUnit.limitUnitCurrent -= 3;
        }
    }
}
