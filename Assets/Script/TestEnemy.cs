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
    public BaseEnemy baseEnemy = null;
    public BasePlayer basePlayer = null;
    public RallyEnemy rallyE = null;

    public int limitUnit = 50;
    public int limitUnitCurrent = 2;

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
        basePlayer = GameManager.Instance.basePlayer;
        baseEnemy = GameManager.Instance.baseEnemy;
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
        if (checkMiner || (limitUnitCurrent >= limitUnit))
            return;
        GameObject miner = Instantiate(Miner);
        Miner mn = miner.GetComponent<Miner>();      
        GameManager.Instance.enemy.Add(mn);
        rallyE.minersE.Add(mn);
        if (rallyE.minersE.Count >= GameManager.Instance.goldInGoldMine.Count * 2 - 2)
            checkMiner = true;
        miner.transform.position = GameManager.Instance.defensePointE.position;
        limitUnitCurrent++;
    }

    public void BuySwordMan()
    {
        if (limitUnitCurrent >= limitUnit)
            return;
        GameObject swordMan = Instantiate(Swordman);
        SwordMan sw = swordMan.GetComponent<SwordMan>();       
        swordMan.transform.position = GameManager.Instance.defensePointE.position;        
        rallyE.swordsE.Add(sw);
        GameManager.Instance.enemy.Add(sw);
        limitUnitCurrent++;
    }

    public void BuyArcher()
    {
        if (limitUnitCurrent >= limitUnit)
            return;
        GameObject archer = Instantiate(Archer);
        Archer ar = archer.GetComponent<Archer>();
        GameManager.Instance.enemy.Add(ar);       
        archer.transform.position = GameManager.Instance.defensePointE.position;
        rallyE.archersE.Add(ar);
        limitUnitCurrent++;
    }

    public void BuySpearMan()
    {
        if (limitUnitCurrent >= limitUnit)
            return;
        GameObject spearMan = Instantiate(Spearman);
        SpearMan sp = spearMan.GetComponent<SpearMan>();
        GameManager.Instance.enemy.Add(sp);       
        spearMan.transform.position = GameManager.Instance.defensePointE.position;
        rallyE.spearsE.Add(sp);
        limitUnitCurrent += 3;
    }

    public void BuyMagicMan()
    {
        if (limitUnitCurrent >= limitUnit)
            return;
        GameObject magicman = Instantiate(Magicman);
        MagicMan mg = magicman.GetComponent<MagicMan>();
        GameManager.Instance.enemy.Add(mg);       
        magicman.transform.position = GameManager.Instance.defensePointE.position;
        rallyE.magicsE.Add(mg);
        limitUnitCurrent += 5;
    }

    public void BuyTitan()
    {
        if (limitUnitCurrent >= limitUnit)
            return;
        GameObject titan = Instantiate(TitanMan);
        Titan tt = titan.GetComponent<Titan>();
        GameManager.Instance.enemy.Add(tt);       
        titan.transform.position = GameManager.Instance.defensePointE.position;
        rallyE.titansE.Add(tt);
        limitUnitCurrent += 3;
    }
}
