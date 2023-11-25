using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MagicMan : BaseSoldier
{
    public GameObject soldierOfMe;
    public Rally rally;
    
    public float timeForSpawn = 0;
    private void Awake()
    {
        agent = GetComponent<Agent>();       
        basePlayer = GameManager.Instance.basePlayer;
        baseEnemy = GameManager.Instance.baseEnemy;
        buyUnit = GameManager.Instance.buyUnit;
        rally = GameManager.Instance.rally;
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
        SpawnSoldier();
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        if (isDead && agent.isPlayer)
        {
            buyUnit.rally.magics.Remove(this);
            buyUnit.limitUnitCurrent -= 5;
        }
    }

    public void SpawnSoldier()
    {
        timeForSpawn += Time.deltaTime;
        if (timeForSpawn >= 3 && rally.tinys.Count < 10)
        {
            agent.animator.SetTrigger("SpawnTiny");           
            GameObject Soldier = Instantiate(soldierOfMe);
            Tiny tiny = Soldier.GetComponent<Tiny>();
            rally.tinys.Add(tiny);
            if (agent.isPlayer)
            {
                tiny.agent.isPlayer = true;
                GameManager.Instance.player.Add(tiny);
            }
                
            if (agent.isEnemy)
            {
                tiny.agent.isEnemy = true;
                GameManager.Instance.enemy.Add(tiny);
            }
                
            Soldier.transform.position = transform.position + Vector3.right * 5f;
            timeForSpawn = 0;
        }

    }
}
