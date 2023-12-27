using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestEnemy : MonoBehaviour
{
    public Button buyMiner;
    public Button buySwordMan;
    public Button buyArcher;
    public Button buySpearMan;
    public Button buyMagicMan;
    public Button buyTitanMan;
    public GameObject Miner;
    public GameObject Swordman;
    public GameObject Archer;
    public GameObject Spearman;
    public GameObject TitanMan;
    public GameObject Magicman;

    public GameObject SuperTitan;

    public int minerPrice;
    public int swordManPrice;
    public int archerPrice;
    public int spearManPrice;
    public int titanManPrice;
    public int magicManPrice;

    public BaseEnemy baseEnemy => GameManager.Instance.baseEnemy;
    public BasePlayer basePlayer => GameManager.Instance.basePlayer;
    public RallyEnemy rallyE = null;

    public int limitUnit = 50;
    public int limitUnitCurrent = 0;

    public bool checkMiner = false;
    public bool checkLimit = false;

    private void Awake()
    {
        rallyE = GetComponent<RallyEnemy>();
        buyMiner.onClick.AddListener(BuyMiner);
        buySwordMan.onClick.AddListener(BuySwordMan);
        buyArcher.onClick.AddListener(BuyArcher);
        buySpearMan.onClick.AddListener(BuySpearMan);
        buyMagicMan.onClick.AddListener(BuyMagicMan);
        buyTitanMan.onClick.AddListener(BuyTitan);        
    }
    private void OnDisable()
    {
        buyArcher.onClick.RemoveAllListeners();
        buyMiner.onClick.RemoveAllListeners();
        buySwordMan.onClick.RemoveAllListeners();
        buySpearMan.onClick.RemoveAllListeners();
        buyMagicMan.onClick.RemoveAllListeners();
        buyTitanMan.onClick.RemoveAllListeners();
    }
    public void BuyMiner()
    {
        if (checkMiner || (limitUnitCurrent >= limitUnit) || baseEnemy.currentGold < minerPrice)
            return;
        baseEnemy.currentGold -= minerPrice;
        GameObject miner = Instantiate(Miner, GameManager.Instance.defensePointE.position, GameManager.Instance.defensePointE.rotation);
        Miner mn = miner.GetComponent<Miner>();      
        GameManager.Instance.enemy.Add(mn);
        rallyE.minersE.Add(mn);
        if (rallyE.minersE.Count >= GameManager.Instance.goldInGoldMine.Count * 2)
            checkMiner = true;       
        limitUnitCurrent++;
    }

    public void BuySwordMan()
    {
        if (limitUnitCurrent >= limitUnit || baseEnemy.currentGold < swordManPrice)
            return;
        baseEnemy.currentGold -= swordManPrice;
        GameObject swordMan = Instantiate(Swordman, GameManager.Instance.defensePointE.position, GameManager.Instance.defensePointE.rotation);
        SwordMan sw = swordMan.GetComponent<SwordMan>();                      
        rallyE.swordsE.Add(sw);
        GameManager.Instance.enemy.Add(sw);
        limitUnitCurrent++;
    }

    public void BuyArcher()
    {
        if (limitUnitCurrent >= limitUnit || baseEnemy.currentGold < archerPrice)
            return;
        baseEnemy.currentGold -= archerPrice;
        GameObject archer = Instantiate(Archer, GameManager.Instance.defensePointE.position, GameManager.Instance.defensePointE.rotation);
        Archer ar = archer.GetComponent<Archer>();
        GameManager.Instance.enemy.Add(ar);               
        rallyE.archersE.Add(ar);
        limitUnitCurrent++;
    }

    public void BuySpearMan()
    {
        if (limitUnitCurrent >= limitUnit || baseEnemy.currentGold < spearManPrice)
            return;
        baseEnemy.currentGold -= spearManPrice;
        GameObject spearMan = Instantiate(Spearman, GameManager.Instance.defensePointE.position, GameManager.Instance.defensePointE.rotation);
        SpearMan sp = spearMan.GetComponent<SpearMan>();
        GameManager.Instance.enemy.Add(sp);             
        rallyE.spearsE.Add(sp);
        limitUnitCurrent += 3;
    }

    public void BuyMagicMan()
    {
        if (limitUnitCurrent >= limitUnit || baseEnemy.currentGold < magicManPrice)
            return;
        baseEnemy.currentGold -= magicManPrice;
        GameObject magicman = Instantiate(Magicman, GameManager.Instance.defensePointE.position, GameManager.Instance.defensePointE.rotation);
        MagicMan mg = magicman.GetComponent<MagicMan>();
        GameManager.Instance.enemy.Add(mg);              
        rallyE.magicsE.Add(mg);
        limitUnitCurrent += 5;
    }

    public void BuyTitan()
    {
        if (limitUnitCurrent >= limitUnit || baseEnemy.currentGold < titanManPrice)
            return;
        baseEnemy.currentGold -= titanManPrice;
        GameObject titan = Instantiate(TitanMan, GameManager.Instance.defensePointE.position, GameManager.Instance.defensePointE.rotation);
        Titan tt = titan.GetComponent<Titan>();
        GameManager.Instance.enemy.Add(tt);               
        rallyE.titansE.Add(tt);
        limitUnitCurrent += 3;
    }

    public void BuySuperTitan()
    {
        GameObject Stitan = Instantiate(SuperTitan, GameManager.Instance.baseEnemy.transform.position, GameManager.Instance.baseEnemy.transform.rotation);
        SuperTitan stt = Stitan.GetComponent<SuperTitan>();
        GameManager.Instance.enemy.Add(stt);
    }
}
