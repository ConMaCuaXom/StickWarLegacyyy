using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Miner : BaseSoldier
{    
    public GoldInGoldMine goldMineGO;   
    public int indexGoldMine = 0;
    public int indexGoldEnemy = 7;
    public int goldTake = 120;
    public float distanceToGoldMine;
    public float distanceToBase;
    public float rangeToBase = 5;
    public float rangeToCook = 5;    
    public float timeForCook = 4.5f;
    public float time;
    public float goldInMiner = 0;
   
    public bool canGoToBase = false;
    public bool canGoToGoldMine = true;
    public bool checkAddPerson;

    private void Awake()
    {
        canGoToBase = false;
        canGoToGoldMine = true;
        checkAddPerson = true;
        agent = GetComponent<Agent>();       
        if (agent.isPlayer)
            goldMineGO = GameManager.Instance.goldInGoldMine[indexGoldMine];
        if (agent.isEnemy)
            goldMineGO = GameManager.Instance.goldInGoldMine[indexGoldEnemy];                
        attackRange = 10f;
        damage = 5f;
        hp = 100f;   
        currentHP = hp;
    }


    private void Update()
    {
        if (pushBack ||  onDef)
            return;
        GoToGoldMine();
        GoToBase();
        WiOrLo();
    }

    public void GoToGoldMine()
    {
        if (checkAddPerson || goldMineGO == null)
        {
            AddIndexGoldMine();
            AddPerson();
        }                                    
        if (canGoToBase)
            return;      
        
        agent.agent.isStopped = false;              
        agent.SetDestination(goldMineGO.transform.position);                 
        distanceToGoldMine = Vector3.Distance(transform.position, goldMineGO.transform.position);
        if (distanceToGoldMine <= rangeToCook)
        {           
            agent.agent.isStopped = true;             
            agent.animator.SetBool("CookCook", true);
            agent.RotationOnTarget(goldMineGO.transform.position - transform.position);
            if (goldInMiner >= 120)
            {
                canGoToBase = true;
                canGoToGoldMine = false;
                agent.agent.isStopped = false;
            }                                                       
        } else
            agent.animator.SetBool("CookCook", false);


    }

    public void GoToBase()
    {
        if (canGoToGoldMine == true)
            return;
        agent.LookAtYourBase();
        agent.animator.SetBool("CookCook", false);
        agent.GoToYourBase();
        if (agent.isPlayer == true)
            distanceToBase = Vector3.Distance(transform.position, agent.basePlayer.transform.position);
        if (agent.isEnemy == true)
            distanceToBase = Vector3.Distance(transform.position, agent.baseEnemy.transform.position);
        if( distanceToBase <= rangeToBase )
        {
            canGoToGoldMine = true;
            canGoToBase = false;           
            
            if (agent.isPlayer)
                agent.basePlayer.currentGold += goldInMiner;
            if (agent.isEnemy)
                agent.baseEnemy.currentGold += goldInMiner;
            goldInMiner = 0;
        }
    }

    public void CookCook()
    {
        goldMineGO.TakeGold(goldTake);
        goldInMiner += goldTake;
    }

    public void AddPerson()
    {
        goldMineGO.person++;
        checkAddPerson = false;
    }


    public void AddIndexGoldMine()
    {
        while (goldMineGO.person >= 2 || goldMineGO == null)
        {
            if (agent.isPlayer)
            {
                indexGoldMine++;
                goldMineGO = GameManager.Instance.goldInGoldMine[indexGoldMine];
            }
            if (agent.isEnemy)
            {
                indexGoldEnemy--;
                goldMineGO = GameManager.Instance.goldInGoldMine[indexGoldEnemy];
            }
            
        }       
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        if (isDead && agent.isPlayer)
        {
            goldMineGO.person--;
            if (buyUnit.rally.miners.Contains(this) == true)
            {
                buyUnit.rally.miners.Remove(this);
                buyUnit.limitUnitCurrent--;
            }            
        }
        if (isDead && agent.isEnemy)
        {
            goldMineGO.person--;
            if (testEnemy.rallyE.minersE.Contains(this) == true)
            {
                testEnemy.rallyE.minersE.Remove(this);
                testEnemy.limitUnitCurrent--;
            }
        }
                
    }
}
