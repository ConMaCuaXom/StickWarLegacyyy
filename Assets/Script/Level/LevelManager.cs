using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static int currentLv;

    public BuyUnit buyUnit => GameManager.Instance.buyUnit;
    public BaseEnemy baseEnemy => GameManager.Instance.baseEnemy;
    public BasePlayer basePlayer => GameManager.Instance.basePlayer;
    public TestEnemy testEnemy => GameManager.Instance.testEnemy;
    public RallyEnemy rallyEnemy => GameManager.Instance.rallyEnemy;
    public Rally rally => GameManager.Instance.rally;

    public TestMode testMode;
    public List<Boat> boats;
    public List<GameObject> tutorialLv1;

    public float pOP;
    public float pOE;
    public float time;
    public float timeCheck;
    public float timeMiner;
    public float timeArcher;

    public bool checkHP;
    public bool goldFirst;
    public bool soldierPlayerFirst;
    public bool soldierEnemyFirst;
    public List<bool> tuto;
    

    private void Awake()
    {
        
        time = 0;
        timeCheck = 0;
        timeMiner = 0;
        timeArcher = 0;
        checkHP = false;
        goldFirst = true;
        soldierPlayerFirst = true;
        soldierEnemyFirst = true;
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
            basePlayer.currentGold = 150;
            goldFirst = false;
        }               
        
        if (rally.swords.Count >= 2 && rallyEnemy.archersE.Count < 1 && timeArcher > 10)
        {
            testEnemy.BuyArcher();
            timeArcher = 0;
        }
        if (rallyEnemy.minersE.Count < 2 && timeMiner > 30)
        {
            testEnemy.BuyMiner();
            timeMiner = 0;
        }
        rallyEnemy.OnFight();
        if (pOP > pOE && rallyEnemy.OnFight() && baseEnemy.currentHP > 300)
        {
            rallyEnemy.DefenseE();
        }
        
        if (baseEnemy.currentHP < 650 && checkHP == false)
        {
            testEnemy.BuyArcher();
            testEnemy.BuyMiner();
            checkHP = true;
        }
       
    }
    public void Level2()
    {
        
        buyUnit.buySpearMan.gameObject.SetActive(false);
        buyUnit.buyTitanMan.gameObject.SetActive(false);
        buyUnit.buyMagicMan.gameObject.SetActive(false);
    }
    public void Level3()
    {
        
        buyUnit.buyTitanMan.gameObject.SetActive(false);
        buyUnit.buyMagicMan.gameObject.SetActive(false);
    }
    public void Level4()
    {
        
        buyUnit.buyMagicMan.gameObject.SetActive(false);
    }
    public void Level5()
    {
        
    }
    public void Level6()
    {
        
    }
    public void Level7()
    {
        
    }
    public void Level8()
    {
        
    }
    public void Level9()
    {
        
    }
    public void Level10()
    {
       
    }

    public void PowerOfMilitary()
    {
        pOP = rally.archers.Count * 75 + rally.swords.Count * 100 + rally.spears.Count * 300 + rally.magics.Count * 400 + rally.titans.Count * 400;
        pOE = rallyEnemy.archersE.Count * 75 + rallyEnemy.swordsE.Count * 100 + rallyEnemy.spearsE.Count * 300 + rallyEnemy.magicsE.Count * 400 + rallyEnemy.titansE.Count * 400;
    }
}
