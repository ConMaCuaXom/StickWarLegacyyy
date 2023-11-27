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
    
    public float timeForSpawn = 0;
    public int numberOfSoldier;

    
    private void Awake()
    {
        agent = GetComponent<Agent>();       
        basePlayer = GameManager.Instance.basePlayer;
        baseEnemy = GameManager.Instance.baseEnemy;
        buyUnit = GameManager.Instance.buyUnit;
        rally = GameManager.Instance.rally;
        rallyEnemy = GameManager.Instance.rallyEnemy;
        testEnemy = GameManager.Instance.testEnemy;
        wol = GameManager.Instance.winOrLose;
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
        if (isDead == true || onDef == true || hulolo == true || pushBack == true)
            return;
        TargetOnWho();
        SpawnSoldier();
        WiOrLo();
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
            List<BaseSoldier> listEnemy = GameManager.Instance.enemy;
            foreach (BaseSoldier soldier in listEnemy)
            {
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
            List<BaseSoldier> listPlayer = GameManager.Instance.player;
            foreach (BaseSoldier soldier in listPlayer)
            {
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
        if (timeForSpawn >= 3 && numberOfSoldier < 8)
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
        GameObject Soldier = Instantiate(soldierOfMe);
        Tiny tiny = Soldier.GetComponent<Tiny>();
        tiny.daddy = this;       
        numberOfSoldier++;
        Soldier.SetActive(false);
        if (agent.isPlayer)
        {
            rally.tinys.Add(tiny);
            tiny.transform.LookAt(baseEnemy.transform);
            GameManager.Instance.player.Add(tiny);
            Soldier.transform.position = this.transform.position + Vector3.right * 1.5f;
            //Debug.Log(tiny.gameObject.transform.position);
            Soldier.SetActive(true);
        }
        if (agent.isEnemy)
        {
            rallyEnemy.tinysE.Add(tiny);
            tiny.transform.LookAt(basePlayer.transform);
            GameManager.Instance.enemy.Add(tiny);
            tiny.transform.position = this.transform.position + Vector3.right * (-1.5f);
            //Debug.Log(tiny.gameObject.transform.position);
            Soldier.SetActive(true);
        }
        this.DelayCall(1f, () =>
        {
            hulolo = false;
            if (agent.agent.enabled == true)
                agent.agent.isStopped = false;
            //Debug.Log(tiny.gameObject.transform.position);
        });
    }

    public void HololoFinish()
    {
        //hulolo = false;
        //if (agent.agent.enabled == true)
        //    agent.agent.isStopped = false;
    }
}
