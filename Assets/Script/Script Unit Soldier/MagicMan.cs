using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class MagicMan : BaseSoldier
{

    private void Awake()
    {
        agent = GetComponent<Agent>();       
        basePlayer = GameManager.Instance.basePlayer;
        baseEnemy = GameManager.Instance.baseEnemy;
        buyUnit = GameManager.Instance.buyUnit;
        attackRange = 10f;
        dangerRange = 15f;
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
            buyUnit.rally.magics.Remove(this);
            buyUnit.limitUnitCurrent -= 3;
        }
    }
}
