using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Archer : BaseSoldier
{
    public GameObject Arrow;
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
            buyUnit.rally.archers.Remove(this);
            buyUnit.limitUnitCurrent--;
        }
                                        
    }

    public void RespawnArrow()
    {
        GameObject arrow = Instantiate(Arrow);
        ArrowAndBolt aab = arrow.GetComponent<ArrowAndBolt>();
        aab.archer = this;
        if (agent.isPlayer)
            aab.target = targetE;
        if (agent.isEnemy)
            aab.target = targetP;
        arrow.transform.position = transform.position;       
    }

    public void RespawnArrowBase()
    {
        GameObject arrow = Instantiate(Arrow);
        ArrowAndBolt aab = arrow.GetComponent<ArrowAndBolt>();
        aab.archer = this;
        if (agent.isPlayer)
            aab.target = targetE;
        if (agent.isEnemy)
            aab.target = targetP;
        arrow.transform.position = transform.position;
    }
}
