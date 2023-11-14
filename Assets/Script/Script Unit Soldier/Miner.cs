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
        checkAddPerson = true;
        agent = GetComponent<Agent>();
        
        if (agent.isPlayer)
            goldMineGO = GameManager.Instance.goldInGoldMine[indexGoldMine];
        if (agent.isEnemy)
            goldMineGO = GameManager.Instance.goldInGoldMine[indexGoldEnemy];
        basePlayer = GameManager.Instance.basePlayer;
        baseEnemy = GameManager.Instance.baseEnemy;
        attackRange = 10f;
        damage = 5f;
        hp = 200f;   
        currentHP = hp;
    }


    private void Update()
    {       
        GoToGoldMine();
        GoToBase();       
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
        agent.RotationOnTarget(goldMineGO.transform.position - transform.position);
        agent.SetDestination(goldMineGO.transform.position);
        
        distanceToGoldMine = Vector3.Distance(transform.position, goldMineGO.transform.position);
        if (distanceToGoldMine <= rangeToCook)
        {           
            agent.agent.isStopped = true;             
            agent.animator.SetBool("CookCook", true);           
            time += Time.deltaTime;           
            if (time >= timeForCook)
            {
                canGoToBase = true;
                canGoToGoldMine = false;
                time = 0;
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
        agent.MoveForward();
        if (agent.isPlayer == true)
            distanceToBase = Vector3.Distance(transform.position,basePlayer.transform.position);
        if (agent.isEnemy == true)
            distanceToBase = Vector3.Distance(transform.position, baseEnemy.transform.position);
        if( distanceToBase <= rangeToBase )
        {
            canGoToGoldMine = true;
            canGoToBase = false;
            basePlayer.currentGold += goldInMiner;
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
            buyUnit.rally.miners.Remove(this);
            buyUnit.limitUnitCurrent--;
        }
    }
}
