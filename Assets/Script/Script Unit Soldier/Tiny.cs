using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiny : BaseSoldier
{
    public MagicMan daddy;
    private void Awake()
    {
        agent = GetComponent<Agent>();
        basePlayer = GameManager.Instance.basePlayer;
        baseEnemy = GameManager.Instance.baseEnemy;
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
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        if (isDead && agent.isPlayer)
        {
            buyUnit.rally.tinys.Remove(this);
            daddy.numberOfSoldier--;
        }
    }
}
