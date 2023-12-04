using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyUnit : MonoBehaviour
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
    public Rally rally = null;   
        
    public int limitUnit = 50;
    public int limitUnitCurrent = 2;
       
    public bool checkMiner = false;
    public bool checkLimit = false;



    private void Awake()
    {
        rally = GetComponent<Rally>();
        buyMiner.onClick.AddListener(BuyMiner); 
        buySwordMan.onClick.AddListener(BuySwordMan);
        buyArcher.onClick.AddListener(BuyArcher);
        buySpearMan.onClick.AddListener(BuySpearMan);
        buyMagicMan.onClick.AddListener(BuyMagicMan);
        buyTitanMan.onClick.AddListener(BuyTitan);
        basePlayer = GameManager.Instance.basePlayer;        
        baseEnemy = GameManager.Instance.baseEnemy;       
    }

    private void Update()
    {
        
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
        GameObject miner = Instantiate(Miner, GameManager.Instance.defensePointP.position, GameManager.Instance.defensePointP.rotation);
        Miner mn = miner.GetComponent<Miner>();       
        GameManager.Instance.player.Add(mn);
        rally.miners.Add(mn);
        if (rally.miners.Count >= GameManager.Instance.goldInGoldMine.Count * 2 - 2)
            checkMiner = true;
        //miner.transform.position = GameManager.Instance.defensePointP.position;
        limitUnitCurrent++;
    }

    public void BuySwordMan()
    {
        if (limitUnitCurrent >= limitUnit)
            return;
        GameObject swordMan = Instantiate(Swordman, GameManager.Instance.defensePointP.position, GameManager.Instance.defensePointP.rotation);
        SwordMan sw = swordMan.GetComponent<SwordMan>();       
        //swordMan.transform.position = GameManager.Instance.defensePointP.position;
        limitUnitCurrent++;                      
        rally.swords.Add(sw);
        GameManager.Instance.player.Add(sw);
    }

    public void BuyArcher()
    {
        if (limitUnitCurrent >= limitUnit)
            return;
        GameObject archer = Instantiate(Archer, GameManager.Instance.defensePointP.position, GameManager.Instance.defensePointP.rotation);        
        Archer ar = archer.GetComponent<Archer>();
        GameManager.Instance.player.Add(ar);       
        //archer.transform.position = GameManager.Instance.defensePointP.position;
        limitUnitCurrent++;        
        rally.archers.Add(ar);                
    }

    public void BuySpearMan()
    {
        if (limitUnitCurrent >= limitUnit)
            return;
        GameObject spearMan = Instantiate(Spearman, GameManager.Instance.defensePointP.position, GameManager.Instance.defensePointP.rotation);       
        SpearMan sp = spearMan.GetComponent<SpearMan>();
        GameManager.Instance.player.Add(sp);       
        //spearMan.transform.position = GameManager.Instance.defensePointP.position;
        limitUnitCurrent += 3;                   
        rally.spears.Add(sp);
    }

    public void BuyMagicMan()
    {
        if (limitUnitCurrent >= limitUnit)
            return;
        GameObject magicman = Instantiate(Magicman, GameManager.Instance.defensePointP.position, GameManager.Instance.defensePointP.rotation);       
        MagicMan mg = magicman.GetComponent<MagicMan>();
        GameManager.Instance.player.Add(mg);        
        //magicman.transform.position = GameManager.Instance.defensePointP.position;
        limitUnitCurrent += 5;       
        rally.magics.Add(mg);        
    }

    public void BuyTitan()
    {
        if (limitUnitCurrent >= limitUnit)
            return;
        GameObject titan = Instantiate(TitanMan, GameManager.Instance.defensePointP.position, GameManager.Instance.defensePointP.rotation);       
        Titan tt = titan.GetComponent<Titan>();
        GameManager.Instance.player.Add(tt);       
        //titan.transform.position = GameManager.Instance.defensePointP.position;
        limitUnitCurrent += 3;        
        rally.titans.Add(tt);
    }
}
