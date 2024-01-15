using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Miner : BaseSoldier
{    
    public Character character;
    public GoldInGoldMine goldMineGO;
    public TargetDynamicSound targetDynamicSound = null;
    //public SoundDynamic soundDynamic = null;
    public int indexGoldMine = 0;
    public int indexGoldEnemy = 7;

    public enum SoundCook
    {
        Miner_Cook,
        Miner_Cook2
    }
    //public SoundCook soundCook;

    public int goldTake1time => character.goldTake1time;
    public float distanceToGoldMine;
    public float distanceToBase;
    public float rangeToBase = 5;
    public float rangeToCook = 5;       
    public float time;
    public float goldInMinerCurrent;
    public float goldInMinermax => character.goldInMinerMax;
   
    public bool canGoToBase = false;
    public bool canGoToGoldMine = true;
    public bool checkAddPerson;   

    private void Awake()
    {
        canGoToBase = false;
        canGoToGoldMine = true;
        checkAddPerson = true;
        agent = GetComponent<Agent>();  
        targetDynamicSound = GetComponent<TargetDynamicSound>();
        targetDynamicSound.Initialized();
        //soundDynamic = AudioManager.Instance.dynamicSoundActive[transform];
        if (agent.isPlayer)
            goldMineGO = GameManager.Instance.goldInGoldMine[indexGoldMine];
        if (agent.isEnemy)
            goldMineGO = GameManager.Instance.goldInGoldMine[indexGoldEnemy];                       
        hp = character.hp;
        timeToDestroy = character.timeToDestroy;
        currentHP = hp;
        goldInMinerCurrent = 0;              
        WhichBody(PlayerPrefs.GetString("MinerBody"));
        WhichHead(PlayerPrefs.GetString("MinerHead"));
        WhichWeapon(PlayerPrefs.GetString("MinerWeapon"));              
    }


    private void Update()
    {
        if (pushBack ||  onDef || isDead == true)
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
            if (goldInMinerCurrent >= goldInMinermax)
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
                agent.basePlayer.currentGold += goldInMinerCurrent;
            if (agent.isEnemy)
                agent.baseEnemy.currentGold += goldInMinerCurrent;
            goldInMinerCurrent = 0;
        }
    }

    public void CookCook()
    {
        goldMineGO.TakeGold(goldTake1time);
        goldInMinerCurrent += goldTake1time;
        //soundDynamic.PlayOneShot("Miner_Cook");
    }

    public void SoundCookCook()
    {
        SoundCook rd = (SoundCook)Random.Range(0,2);
        soundDynamic.PlayOneShot(rd.ToString());
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
    public override void WiOrLo()
    {

        if (wol.playerWin == true)
        {
            if (agent.isPlayer)
            {
                agent.agent.isStopped = true;
                agent.animator.SetBool("Victory", true);
            }
            if (agent.isEnemy)
            {
                agent.agent.isStopped = true;
                agent.animator.SetBool("Run", false);
                agent.animator.SetBool("CookCook", false);
            }

        }
        if (wol.playerLose == true)
        {
            if (agent.isEnemy)
            {
                agent.agent.isStopped = true;
                agent.animator.SetBool("Victory", true);
            }
            if (agent.isPlayer)
            {
                agent.agent.isStopped = true;
                agent.animator.SetBool("Run", false);
                agent.animator.SetBool("CookCook", false);
            }

        }
    }
}
