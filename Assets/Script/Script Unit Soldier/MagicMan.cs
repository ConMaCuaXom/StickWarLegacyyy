using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MagicMan : BaseSoldier
{
    public GameObject soldierOfMe;
    public GameObject explosion;
    public ParticleSystem destroyedMid;
    public Rally rally;
    public RallyEnemy rallyEnemy;
    public Transform spawnPoint;
    
    public float timeForSpawn = 0;
    public int numberOfSoldier;

    
    private void Awake()
    {
        agent = GetComponent<Agent>();              
        rally = GameManager.Instance.rally;
        rallyEnemy = GameManager.Instance.rallyEnemy;        
        attackRange = 10f;
        dangerRange = 10f;
        damage = 25f;
        hp = 70f;
        currentHP = hp;
        isDead = false;
        onAttack = false;
        numberOfSoldier = 0;
    }

    private void Update()
    {
        if (isDead == true || onDef == true || hulolo == true || pushBack == true)
            return;
        TargetOnWho();
        SpawnSoldier();
        WiOrLo();
        OnFighting();
       
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        if (isDead && agent.isPlayer)
        {
            if(rally.magics.Contains(this) == true)
            {
                rally.magics.Remove(this);
                buyUnit.limitUnitCurrent -= 5;
            }         
        }
        if (isDead && agent.isEnemy)
        {           
            if (testEnemy.rallyE.magicsE.Contains(this) == true)
            {
                testEnemy.rallyE.magicsE.Remove(this);
                testEnemy.limitUnitCurrent -= 5;
            }
                
        }
    }

    public void Explosion()
    {
        if (GameManager.Instance.allMagicAndEffect.Contains(destroyedMid) == false)
        {
            GameObject explosionn = Instantiate(explosion);
            destroyedMid = explosionn.GetComponent<ParticleSystem>();
            GameManager.Instance.allMagicAndEffect.Add(destroyedMid);
        }
        else
        {
            destroyedMid.gameObject.SetActive(true);
        }
        if (agent.isPlayer && targetE!=null)
        {
            targetE.TakeDamage(damage);
            targetE.PushBack();           
            destroyedMid.gameObject.transform.position = targetE.transform.position;
            targetE.TakeDamage(damage);
            targetE.PushBack();
            for (int i = GameManager.Instance.enemy.Count - 1; i >= 0; i--)
            {
                BaseSoldier soldier = GameManager.Instance.enemy[i];
                if (Vector3.Distance(soldier.transform.position, targetE.transform.position) <= 2f)
                {
                    soldier.TakeDamage(damage);
                    soldier.PushBack();
                }
            }
        }
        if (agent.isEnemy && targetP != null)
        {
            targetP.TakeDamage(damage);
            targetP.PushBack();            
            destroyedMid.gameObject.transform.position = targetP.transform.position;
            for (int i = GameManager.Instance.player.Count - 1; i >= 0; i--)
            {
                BaseSoldier soldier = GameManager.Instance.player[i];
                if (Vector3.Distance(soldier.transform.position, targetP.transform.position) <= 2f)
                {
                    soldier.TakeDamage(damage);
                    soldier.PushBack();
                }
            }
        }       
    }

    

    public void SpawnSoldier()
    {
        timeForSpawn += Time.deltaTime;
        if (timeForSpawn >= 4 && numberOfSoldier < 2)
        {
            hulolo = true;
            if (agent.agent.enabled == true)
                agent.agent.isStopped = true;
            agent.animator.SetTrigger("SpawnTiny");                       
            timeForSpawn = 0;
        }
    }

    public void HololoBegin()
    {       
        GameObject Soldier = Instantiate(soldierOfMe, spawnPoint.position, transform.rotation);
        Tiny tiny = Soldier.GetComponent<Tiny>();
        tiny.daddy = this;       
        numberOfSoldier++;       
        if (agent.isPlayer)
        {
            rally.tinys.Add(tiny);            
            GameManager.Instance.player.Add(tiny);            
        }
        if (agent.isEnemy)
        {
            rallyEnemy.tinysE.Add(tiny);            
            GameManager.Instance.enemy.Add(tiny);            
        }        
    }

    public void HololoFinish()
    {
        hulolo = false;
        if (agent.agent.enabled == true)
            agent.agent.isStopped = false;
    }
}
