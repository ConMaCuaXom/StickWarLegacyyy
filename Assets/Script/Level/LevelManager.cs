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
    public TestEnemy testEnemy => GameManager.Instance.testEnemy;
    public RallyEnemy rallyEnemy => GameManager.Instance.rallyEnemy;
    public Rally rally => GameManager.Instance.rally;

    public TestMode testMode;
    public List<Boat> boats;

    public float pOP;
    public float pOE;
    public float time;
    public float timeMiner;
    public float timeArcher;

    public bool checkHP;


    private void Awake()
    {
        time = 0;
        timeMiner = 0;
        timeArcher = 0;
        checkHP = false;
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
        if (rally.swords.Count >= 2 && rallyEnemy.archersE.Count < 1 && timeArcher > 10)
        {
            testEnemy.BuyArcher();
            timeArcher = 0;
        }
        if (rallyEnemy.minersE.Count < 2 && timeMiner > 40)
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
