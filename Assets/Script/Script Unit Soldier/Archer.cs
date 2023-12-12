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
        WiOrLo();
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        if (isDead && agent.isPlayer)
        {
            if(buyUnit.rally.archers.Contains(this) == true)
            {
                buyUnit.rally.archers.Remove(this);
                buyUnit.limitUnitCurrent--;
            }           
        }
        if (isDead && agent.isEnemy)
        {
            if (testEnemy.rallyE.archersE.Contains(this) == true)
            {
                testEnemy.rallyE.archersE.Remove(this);
                testEnemy.limitUnitCurrent--;
            }
                           
        }

    }

    public void RespawnArrow()
    {
        GameObject arrow = Instantiate(Arrow);
        ArrowAndBolt aab = arrow.GetComponent<ArrowAndBolt>();
        aab.archer = this;
        if (agent.isPlayer)
        {
            aab.target = targetE;
            aab.isPlayer = true;
        }
            
        if (agent.isEnemy)
        {
            aab.target = targetP;
            aab.isPlayer = false;
        }
            
        arrow.transform.position = transform.position;       
    }

    public void RespawnArrowBase()
    {
        GameObject arrow = Instantiate(Arrow);
        ArrowAndBolt aab = arrow.GetComponent<ArrowAndBolt>();
        aab.archer = this;
        if (agent.isPlayer)
            aab.isPlayer = true;
        if (agent.isEnemy)
            aab.isPlayer = false;
        arrow.transform.position = transform.position;
    }
}
