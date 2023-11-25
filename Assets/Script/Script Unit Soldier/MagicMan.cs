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
    public int numberOfSoldier;

    
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
        numberOfSoldier = 0;
    }

    private void Update()
    {
        if (isDead == true || onDef == true || hulolo == true)
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
        if (timeForSpawn >= 3 && numberOfSoldier < 8)
        {
            hulolo = true;
            agent.agent.isStopped = true;
            agent.animator.SetTrigger("SpawnTiny");                       
            timeForSpawn = 0;
        }
    }

    public void HololoBegin()
    {       
        GameObject Soldier = Instantiate(soldierOfMe);
        Tiny tiny = Soldier.GetComponent<Tiny>();
        tiny.daddy = this;
        rally.tinys.Add(tiny);
        numberOfSoldier++;
        if (agent.isPlayer)
        {
            tiny.agent.isPlayer = true;
            tiny.agent.transform.forward = Vector3.right;
            GameManager.Instance.player.Add(tiny);
        }
        if (agent.isEnemy)
        {
            tiny.agent.isEnemy = true;
            tiny.agent.transform.forward = Vector3.left;            
            GameManager.Instance.enemy.Add(tiny);
        }
        Soldier.transform.position = transform.position + Vector3.right * 1.5f;
    }

    public void HololoFinish()
    {
        hulolo = false;
    }
}
