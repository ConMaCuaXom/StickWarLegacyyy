using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static int currentLv;

    public List<LevelConfig> levelConfig = null;
    public BuyUnit buyUnit => GameManager.Instance.buyUnit;
    public BaseEnemy baseEnemy => GameManager.Instance.baseEnemy;
    public BasePlayer basePlayer => GameManager.Instance.basePlayer;
    public TestEnemy testEnemy => GameManager.Instance.testEnemy;
    public RallyEnemy rallyEnemy => GameManager.Instance.rallyEnemy;
    public Rally rally => GameManager.Instance.rally;
    public WinOrLose winOrLose => GameManager.Instance.winOrLose;

    public TestMode testMode;
    public List<Boat> boats;
    public List<GameObject> tutorialLv1;

    public float pOP;
    public float pOE;
    public float time;
    public float timeCheck;
    public float timeMiner;
    public float timeArcher;
    public float timeSwordMan;
    public float timeSpearMan;
    public float timeTitanMan;
    public float timeMagicMan;

    public bool checkHP;
    public bool goldFirst;
    public bool checkFinalBoss;
    public List<bool> tuto;
    

    private void Awake()
    {
        
        time = 0;
        timeCheck = 0;
        timeMiner = 0;
        timeArcher = 0;
        timeSwordMan = 0;
        timeSpearMan = 0;
        timeTitanMan = 0;
        timeMagicMan = 0;
        checkHP = false;
        goldFirst = true; 
        checkFinalBoss = false;
        for (int i = 0; i < 10; i++)
        {
            bool ttrl = true;
            tuto.Add(ttrl);
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        timeMiner += Time.deltaTime;
        timeArcher += Time.deltaTime;
        timeSwordMan += Time.deltaTime;
        timeSpearMan += Time.deltaTime;
        timeTitanMan += Time.deltaTime;
        timeMagicMan += Time.deltaTime;
        WhichLv();
        PowerOfMilitary();
    }

    public void WhichLv()
    {
        switch (LevelManager.currentLv)
        {
            case 1:
                Level1(); break;
            case 2:
                Level2(); break;
            case 3:
                Level3(); break;
            case 4:
                Level4(); break;
            case 5:
                Level5(); break;
            case 6:
                Level6(); break;
            case 7:
                Level7(); break;
            case 8:
                Level8(); break;
            case 9:
                Level9(); break;
            case 10:
                Level10(); break;
        }
    }
    public void Level1()
    {
        if (time > 2f && tuto[0])
            tutorialLv1[0].SetActive(true);
        if (rally.miners.Count > 0 && tuto[1])
        {           
            tutorialLv1[0].SetActive(false);
            tutorialLv1[1].SetActive(true);
            if (tuto[0])
            {
                basePlayer.currentGold += 250;
                testEnemy.BuyMiner();
                tuto[0] = false;
            }          
        }
        if (rally.swords.Count == 2)
        {
            if (rally.swords[1].onRally && tuto[2])
            {
                tuto[1] = false;
                tutorialLv1[1].SetActive(false);
                tutorialLv1[2].SetActive(true);
            }
        }
       
        if (rally.attackForward.isOn && tuto[3])
        {
            tuto[2] = false;
            tutorialLv1[2].SetActive(false);
            tutorialLv1[3].SetActive(true);
            timeCheck += Time.deltaTime;
            if (timeCheck > 2f && timeCheck < 4f)
            {               
                tutorialLv1[3].SetActive(false);
                tutorialLv1[4].SetActive(true);
            }
            if (timeCheck >= 4f && timeCheck < 7f)
            {
                tutorialLv1[4].SetActive(false);
                tutorialLv1[5].SetActive(true);
            }
            if (timeCheck > 7f)
            {
                tutorialLv1[5].SetActive(false);
                tutorialLv1[3].SetActive(false);
                tuto[3] = false;
            }              
        } 

        if (testMode.cheating == false)
        {
            buyUnit.buyArcher.gameObject.SetActive(false);
            buyUnit.buySpearMan.gameObject.SetActive(false);
            buyUnit.buyTitanMan.gameObject.SetActive(false);
            buyUnit.buyMagicMan.gameObject.SetActive(false);
        }
        foreach (Boat boatHide in boats)
        {
            boatHide.agent.isStopped = true;
        }
        if (goldFirst)
        {
            basePlayer.currentGold = levelConfig[currentLv - 1].firstGoldForPlayer;
            goldFirst = false;
        }

        BuyArcherE();
        BuyMinerE();
        
        if (pOP > pOE && baseEnemy.currentHP > levelConfig[currentLv - 1].minCurrentHpForDef && rally.PlayerIsNear() == true)
        {          
            rallyEnemy.DefenseE();
        }
        
        if (baseEnemy.currentHP < levelConfig[currentLv - 1].currentHpForAddUnit && checkHP == false)
        {
            testEnemy.BuyArcher();
            testEnemy.BuyMiner();
            checkHP = true;
        }
       
    }
    public void Level2()
    {
        if (testMode.cheating == false)
        {           
            buyUnit.buySpearMan.gameObject.SetActive(false);
            buyUnit.buyTitanMan.gameObject.SetActive(false);
            buyUnit.buyMagicMan.gameObject.SetActive(false);
        }
        if (goldFirst)
        {
            basePlayer.currentGold = levelConfig[currentLv - 1].firstGoldForPlayer;
            buyUnit.AddMiner(2);
            buyUnit.limitUnitCurrent += 2;
            testEnemy.BuyMiner();
            testEnemy.BuyMiner();
            this.DelayCall(2f, () =>
            {
                testEnemy.BuySwordMan();                
            });
            goldFirst = false;
        }
        if (tuto[4])
        foreach (Miner mn in rally.miners)
        {
            if (mn.currentHP < mn.hp)
            {
                tutorialLv1[6].SetActive(true);
                tuto[4] = false; 
                break;
            }
        }
        if (rally.defense.isOn == true)
            tutorialLv1[6].SetActive(false);
        if (baseEnemy.currentHP < levelConfig[currentLv - 1].currentHpForAddUnit && checkHP == false)
        {
            baseEnemy.currentGold += levelConfig[currentLv - 1].addGoldForEnemy;
            testEnemy.BuySwordMan();
            testEnemy.BuySwordMan();
            testEnemy.BuySwordMan();
            testEnemy.BuyMiner();
            testEnemy.BuyMiner();
            checkHP = true;
        }
        BuyMinerE();
        BuySwordManE();
        EnemyDefense();
        EnemyRally();
        EnemyAttack();       
    }
    public void Level3()
    {
        if (testMode.cheating == false)
        {
            buyUnit.buySpearMan.gameObject.SetActive(false);
            buyUnit.buyTitanMan.gameObject.SetActive(false);
            buyUnit.buyMagicMan.gameObject.SetActive(false);
        }
        if (goldFirst)
        {
            basePlayer.currentGold = levelConfig[currentLv - 1].firstGoldForPlayer;
            buyUnit.AddMiner(2);
            buyUnit.limitUnitCurrent += 2;
            testEnemy.BuyMiner();
            testEnemy.BuyMiner();
            this.DelayCall(2f, () =>
            {
                testEnemy.BuySpearMan();
            });
            goldFirst = false;
        }
        if (baseEnemy.currentHP < levelConfig[currentLv - 1].currentHpForAddUnit && checkHP == false)
        {
            baseEnemy.currentGold += levelConfig[currentLv - 1].addGoldForEnemy;
            testEnemy.BuySpearMan();
            testEnemy.BuySpearMan();
            testEnemy.BuySpearMan();
            testEnemy.BuyMiner();
            testEnemy.BuyMiner();
            checkHP = true;
        }
        BuyMinerE();
        BuySpearManE();
        EnemyDefense();
        EnemyRally();
        EnemyAttack();
        
    }
    public void Level4()
    {
        if (testMode.cheating == false)
        {
            buyUnit.buyTitanMan.gameObject.SetActive(false);
            buyUnit.buyMagicMan.gameObject.SetActive(false);
        }
        if (goldFirst)
        {
            basePlayer.currentGold = levelConfig[currentLv - 1].firstGoldForPlayer;
            baseEnemy.currentGold = levelConfig[currentLv - 1].firstGoldForEnemy;
            buyUnit.AddMiner(2);
            buyUnit.limitUnitCurrent += 2;
            testEnemy.BuyMiner();
            testEnemy.BuyMiner();
            testEnemy.BuyMiner();
            goldFirst = false;
        }
        if (baseEnemy.currentHP < levelConfig[currentLv - 1].currentHpForAddUnit && checkHP == false)
        {
            baseEnemy.currentGold += levelConfig[currentLv - 1].addGoldForEnemy;
            testEnemy.BuyMagicMan();
            testEnemy.BuyMagicMan();
            testEnemy.BuyMiner();
            testEnemy.BuyMiner();
            checkHP = true;
        }
        EnemyDefense();
        EnemyRally();
        EnemyAttack();        
        BuyMinerE();
        BuyMagicManE();       
    }
    public void Level5()
    {
        if (testMode.cheating == false)
        {
            buyUnit.buyTitanMan.gameObject.SetActive(false);           
        }
        if (goldFirst)
        {
            basePlayer.currentGold = levelConfig[currentLv - 1].firstGoldForPlayer;
            baseEnemy.currentGold = levelConfig[currentLv - 1].firstGoldForEnemy;
            buyUnit.AddMiner(2);
            buyUnit.limitUnitCurrent += 2;
            testEnemy.BuyMiner();
            testEnemy.BuyMiner();
            testEnemy.BuyMiner();
            goldFirst = false;
        }
        if (baseEnemy.currentHP < levelConfig[currentLv - 1].currentHpForAddUnit && checkHP == false)
        {
            baseEnemy.currentGold += levelConfig[currentLv - 1].addGoldForEnemy;
            testEnemy.BuySwordMan();
            testEnemy.BuySwordMan();
            testEnemy.BuySwordMan();
            testEnemy.BuyArcher();
            testEnemy.BuyArcher();
            testEnemy.BuyArcher();
            testEnemy.BuyMiner();
            checkHP = true;
        }
        EnemyDefense();
        EnemyRally();
        EnemyAttack();      
        BuyMinerE();
        BuySwordManE();
        BuyArcherE();        
    }
    public void Level6()
    {
        if (testMode.cheating == false)
        {
            buyUnit.buyTitanMan.gameObject.SetActive(false);
        }
        if (goldFirst)
        {
            basePlayer.currentGold = levelConfig[currentLv - 1].firstGoldForPlayer;
            baseEnemy.currentGold = levelConfig[currentLv - 1].firstGoldForEnemy;
            buyUnit.AddMiner(2);
            buyUnit.limitUnitCurrent += 2;
            testEnemy.BuyMiner();
            testEnemy.BuyMiner();
            testEnemy.BuyMiner();
            goldFirst = false;
        }
        if (baseEnemy.currentHP < levelConfig[currentLv - 1].currentHpForAddUnit && checkHP == false)
        {
            baseEnemy.currentGold += levelConfig[currentLv - 1].addGoldForEnemy;
            testEnemy.BuySpearMan();
            testEnemy.BuySpearMan();
            testEnemy.BuySpearMan();
            testEnemy.BuySpearMan();
            testEnemy.BuyArcher();
            testEnemy.BuyArcher();
            testEnemy.BuyArcher();
            testEnemy.BuyArcher();
            testEnemy.BuyMiner();
            checkHP = true;
        }
        EnemyDefense();
        EnemyRally();
        EnemyAttack();
        BuyMinerE();
        BuyArcherE();
        BuySpearManE();
    }
    public void Level7()
    {
        if (testMode.cheating == false)
        {
            buyUnit.buyTitanMan.gameObject.SetActive(false);
        }
        if (goldFirst)
        {
            basePlayer.currentGold = levelConfig[currentLv - 1].firstGoldForPlayer;
            baseEnemy.currentGold = levelConfig[currentLv - 1].firstGoldForEnemy;
            buyUnit.AddMiner(2);
            buyUnit.limitUnitCurrent += 2;
            testEnemy.BuyMiner();
            testEnemy.BuyMiner();
            testEnemy.BuyMiner();
            goldFirst = false;
        }
        if (baseEnemy.currentHP < levelConfig[currentLv - 1].currentHpForAddUnit && checkHP == false)
        {
            baseEnemy.currentGold += levelConfig[currentLv - 1].addGoldForEnemy;
            testEnemy.BuySpearMan();
            testEnemy.BuySpearMan();
            testEnemy.BuySwordMan();
            testEnemy.BuySwordMan();
            testEnemy.BuyArcher();
            testEnemy.BuyArcher();
            testEnemy.BuyMagicMan();
            testEnemy.BuyMiner();
            checkHP = true;
        }
        EnemyDefense();
        EnemyRally();
        EnemyAttack();
        BuyMinerE();
        BuySwordManE();
        BuySpearManE();
        BuyMagicManE();
    }
    public void Level8()
    {
        if (testMode.cheating == false)
        {
            buyUnit.buyTitanMan.gameObject.SetActive(false);
        }
        if (goldFirst)
        {
            basePlayer.currentGold = levelConfig[currentLv - 1].firstGoldForPlayer;
            baseEnemy.currentGold = levelConfig[currentLv - 1].firstGoldForEnemy;
            buyUnit.AddMiner(2);
            buyUnit.limitUnitCurrent += 2;
            testEnemy.BuyMiner();
            testEnemy.BuyMiner();
            testEnemy.BuyMiner();
            goldFirst = false;
        }
        if (baseEnemy.currentHP < levelConfig[currentLv - 1].currentHpForAddUnit && checkHP == false)
        {
            baseEnemy.currentGold += levelConfig[currentLv - 1].addGoldForEnemy;
            testEnemy.BuyTitan();           
            testEnemy.BuyMiner();
            checkHP = true;
        }
        EnemyDefense();
        EnemyRally();
        EnemyAttack();
        BuyMinerE();
        BuyTitanManE();
    }
    public void Level9()
    {
        if (goldFirst)
        {
            basePlayer.currentGold = levelConfig[currentLv - 1].firstGoldForPlayer;
            baseEnemy.currentGold = levelConfig[currentLv - 1].firstGoldForEnemy;
            buyUnit.AddMiner(2);
            buyUnit.limitUnitCurrent += 2;
            testEnemy.BuyMiner();
            testEnemy.BuyMiner();
            testEnemy.BuyMiner();
            goldFirst = false;
        }
        if (baseEnemy.currentHP < levelConfig[currentLv - 1].currentHpForAddUnit && checkHP == false)
        {
            baseEnemy.currentGold += levelConfig[currentLv - 1].addGoldForEnemy;
            testEnemy.BuyTitan();
            testEnemy.BuyMagicMan();
            testEnemy.BuyArcher();
            testEnemy.BuyArcher();
            testEnemy.BuyArcher();
            testEnemy.BuyArcher();            
            testEnemy.BuySwordMan();
            testEnemy.BuySwordMan();
            checkHP = true;
        }
        EnemyDefense();
        EnemyRally();
        EnemyAttack();
        BuyMinerE();
        BuySpearManE();
        BuyArcherE();
        BuySwordManE();
        BuyTitanManE();
        BuyMagicManE();        
    }
    public void Level10()
    {
        if (goldFirst)
        {
            basePlayer.currentGold = levelConfig[currentLv - 1].firstGoldForPlayer;
            baseEnemy.currentGold = levelConfig[currentLv - 1].firstGoldForEnemy;
            buyUnit.AddMiner(2);
            buyUnit.limitUnitCurrent += 2;
            testEnemy.BuyMiner();
            testEnemy.BuyMiner();
            testEnemy.BuyMiner();
            goldFirst = false;
        }
        if (baseEnemy.currentHP < levelConfig[currentLv - 1].currentHpForAddUnit && checkHP == false)
        {
            baseEnemy.currentGold += levelConfig[currentLv - 1].addGoldForEnemy;
            testEnemy.BuyTitan();
            testEnemy.BuyMagicMan();
            testEnemy.BuyArcher();
            testEnemy.BuyArcher();
            testEnemy.BuyArcher();
            testEnemy.BuyArcher();
            testEnemy.BuySwordMan();
            testEnemy.BuySwordMan();
            checkHP = true;
        }
        if (checkFinalBoss == false)
        {
            EnemyDefense();
            EnemyRally();
            EnemyAttack();
            BuyMinerE();
            BuySpearManE();
            BuyArcherE();
            BuySwordManE();
            BuyTitanManE();
            BuyMagicManE();
        }
        
        if (winOrLose.finalBoss == true && checkFinalBoss == false)
        {
            rallyEnemy.DeleteAll();
            testEnemy.BuySuperTitan();           
            checkFinalBoss = true;
        }
    }

    public void PowerOfMilitary()
    {
        pOP = rally.archers.Count * 75 + rally.swords.Count * 100 + rally.spears.Count * 275 + rally.magics.Count * 400 + rally.titans.Count * 800;
        pOE = rallyEnemy.archersE.Count * 75 + rallyEnemy.swordsE.Count * 100 + rallyEnemy.spearsE.Count * 275 + rallyEnemy.magicsE.Count * 400 + rallyEnemy.titansE.Count * 800;
    }

    public void BuyMinerE()
    {
        if (rallyEnemy.minersE.Count < levelConfig[currentLv - 1].maxMiner && timeMiner > levelConfig[currentLv - 1].timeSpawnMiner && baseEnemy.currentGold >= testEnemy.minerPrice)
        {
            testEnemy.BuyMiner();
            timeMiner = 0;
        }
    }

    public void BuyArcherE()
    {
        if (rallyEnemy.archersE.Count < levelConfig[currentLv - 1].maxArcher && timeArcher > levelConfig[currentLv - 1].timeSpawnArcher)
        {
            testEnemy.BuyArcher();
            timeArcher = 0;
        }
    }

    public void BuySwordManE()
    {
        if (rallyEnemy.swordsE.Count < levelConfig[currentLv - 1].maxSwordMan && timeSwordMan > levelConfig[currentLv - 1].timeSpawnSwordMan)
        {
            testEnemy.BuySwordMan();
            timeSwordMan = 0;
        }
    }

    public void BuySpearManE()
    {
        if (rallyEnemy.spearsE.Count < levelConfig[currentLv - 1].maxSpearMan && timeSwordMan > levelConfig[currentLv - 1].timeSpawnSwordMan && baseEnemy.currentGold >= testEnemy.spearManPrice)
        {
            testEnemy.BuySpearMan();
            timeSwordMan = 0;
        }
    }

    public void BuyMagicManE()
    {
        if (rallyEnemy.magicsE.Count < levelConfig[currentLv - 1].maxMagicMan && timeMagicMan > levelConfig[currentLv - 1].timeSpawnMagicMan && baseEnemy.currentGold >= testEnemy.magicManPrice)
        {
            testEnemy.BuyMagicMan();
            timeMagicMan = 0;
        }
    }

    public void BuyTitanManE()
    {
        if (rallyEnemy.titansE.Count < levelConfig[currentLv - 1].maxTitanMan && timeTitanMan > levelConfig[currentLv - 1].timeSpawnTitanMan && baseEnemy.currentGold >= testEnemy.titanManPrice)
        {
            testEnemy.BuyTitan();
            timeTitanMan = 0;
        }
    }

    public void EnemyAttack()
    {
        if (pOP <= pOE)
        {
            rallyEnemy.rallyingE.isOn = false;
            rallyEnemy.defenseE.isOn = false;
            rallyEnemy.AttackForwardE();
        }
    }

    public void EnemyRally()
    {
        if (pOP > pOE && (rally.PlayerIsNear() == false || baseEnemy.currentHP <= levelConfig[currentLv - 1].minCurrentHpForDef))
        {
            rallyEnemy.Rallyt1E();
        }
    }

    public void EnemyDefense()
    {
        if (pOP > pOE && baseEnemy.currentHP > levelConfig[currentLv - 1].minCurrentHpForDef && rally.PlayerIsNear() == true)
        {
            rallyEnemy.rallyingE.isOn = false;
            rallyEnemy.DefenseE();
        }
    }
}
