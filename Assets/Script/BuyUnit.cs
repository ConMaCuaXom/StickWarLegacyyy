using DG.Tweening;
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

    [SerializeField] private int minerPrice;
    [SerializeField] private int swordManPrice;
    [SerializeField] private int archerPrice;
    [SerializeField] private int spearManPrice;
    [SerializeField] private int titanManPrice;
    [SerializeField] private int magicManPrice;

    [SerializeField] private float delayTimeMiner;
    [SerializeField] private float delayTimeSwordMan;
    [SerializeField] private float delayTimeArcher;
    [SerializeField] private float delayTimeSpearMan;
    [SerializeField] private float delayTimeTitanMan;
    [SerializeField] private float delayTimeMagicMan;

    [SerializeField] private Image loadingMinerRed;
    [SerializeField] private Image loadingMiner;
    [SerializeField] private Image loadingSwordManRed;
    [SerializeField] private Image loadingSwordMan;
    [SerializeField] private Image loadingArcherRed;
    [SerializeField] private Image loadingArcher;
    [SerializeField] private Image loadingSpearManRed;
    [SerializeField] private Image loadingSpearMan;
    [SerializeField] private Image loadingTitanManRed;
    [SerializeField] private Image loadingTitanMan;
    [SerializeField] private Image loadingMagicManRed;
    [SerializeField] private Image loadingMagicMan;



    public BaseEnemy baseEnemy => GameManager.Instance.baseEnemy;
    public BasePlayer basePlayer => GameManager.Instance.basePlayer;
    public Rally rally = null;

    
    public int limitUnit = 50;
    public int limitUnitCurrent = 2;
       
    public bool checkMiner = false;
    public bool checkLimit = false;

    [SerializeField] private bool delayAll;   

    private void Awake()
    {
        rally = GetComponent<Rally>();
        buyMiner.onClick.AddListener(BuyMiner); 
        buySwordMan.onClick.AddListener(BuySwordMan);
        buyArcher.onClick.AddListener(BuyArcher);
        buySpearMan.onClick.AddListener(BuySpearMan);
        buyMagicMan.onClick.AddListener(BuyMagicMan);
        buyTitanMan.onClick.AddListener(BuyTitan);

        delayAll = false;       
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
        if (checkMiner || limitUnitCurrent >= limitUnit || basePlayer.currentGold < minerPrice || delayAll )
            return;
        delayAll = true;
        loadingMiner.DOFillAmount(0, 0f);
        loadingMinerRed.DOFillAmount(1, 0f);
        GameObject miner = Instantiate(Miner, GameManager.Instance.defensePointP.position, GameManager.Instance.defensePointP.rotation);
        Miner mn = miner.GetComponent<Miner>();       
        GameManager.Instance.player.Add(mn);
        basePlayer.currentGold -= minerPrice;
        rally.miners.Add(mn);
        if (rally.miners.Count >= GameManager.Instance.goldInGoldMine.Count * 2 - 2)
            checkMiner = true;        
        limitUnitCurrent++;
        loadingMinerRed.DOFillAmount(0, delayTimeMiner);
        loadingMiner.DOFillAmount(1, delayTimeMiner);
        this.DelayCall(delayTimeMiner, () =>
        {
            delayAll = false;
        });
    }

    public void BuySwordMan()
    {
        if (limitUnitCurrent >= limitUnit || basePlayer.currentGold < swordManPrice || delayAll)
            return;
        delayAll = true;
        loadingSwordMan.DOFillAmount(0, 0f);
        loadingSwordManRed.DOFillAmount(1, 0f);
        GameObject swordMan = Instantiate(Swordman, GameManager.Instance.defensePointP.position, GameManager.Instance.defensePointP.rotation);
        SwordMan sw = swordMan.GetComponent<SwordMan>();       
        basePlayer.currentGold -= swordManPrice;
        limitUnitCurrent++;                      
        rally.swords.Add(sw);
        GameManager.Instance.player.Add(sw);
        loadingSwordMan.DOFillAmount(1, delayTimeSwordMan);
        loadingSwordManRed.DOFillAmount(0, delayTimeSwordMan);
        this.DelayCall(delayTimeSwordMan, () =>
        {
            delayAll = false;
        });
    }

    public void BuyArcher()
    {
        if (limitUnitCurrent >= limitUnit || basePlayer.currentGold < archerPrice || delayAll)
            return;
        delayAll = true;
        loadingArcher.DOFillAmount(0, 0f);
        loadingArcherRed.DOFillAmount(1, 0f);
        GameObject archer = Instantiate(Archer, GameManager.Instance.defensePointP.position, GameManager.Instance.defensePointP.rotation);        
        Archer ar = archer.GetComponent<Archer>();
        GameManager.Instance.player.Add(ar);       
        basePlayer.currentGold -= archerPrice;
        limitUnitCurrent++;        
        rally.archers.Add(ar);
        loadingArcher.DOFillAmount(1, delayTimeArcher);
        loadingArcherRed.DOFillAmount(0, delayTimeArcher);
        this.DelayCall(delayTimeArcher, () =>
        {
            delayAll = false;
        });
    }

    public void BuySpearMan()
    {
        if (limitUnitCurrent >= limitUnit - 2 || basePlayer.currentGold < spearManPrice || delayAll)
            return;
        delayAll = true;
        loadingSpearMan.DOFillAmount(0, 0f);
        loadingSpearManRed.DOFillAmount(1, 0f);
        GameObject spearMan = Instantiate(Spearman, GameManager.Instance.defensePointP.position, GameManager.Instance.defensePointP.rotation);       
        SpearMan sp = spearMan.GetComponent<SpearMan>();
        GameManager.Instance.player.Add(sp);       
        basePlayer.currentGold -= spearManPrice;
        limitUnitCurrent += 3;                   
        rally.spears.Add(sp);
        loadingSpearMan.DOFillAmount(1, delayTimeSpearMan);
        loadingSpearManRed.DOFillAmount(0, delayTimeSpearMan);
        this.DelayCall(delayTimeSpearMan, () =>
        {
            delayAll = false;
        });
    }

    public void BuyMagicMan()
    {
        if (limitUnitCurrent >= limitUnit - 4 || basePlayer.currentGold < magicManPrice || delayAll)
            return;
        delayAll = true;
        loadingMagicMan.DOFillAmount(0, 0f);
        loadingMagicManRed.DOFillAmount(1, 0f);
        GameObject magicman = Instantiate(Magicman, GameManager.Instance.defensePointP.position, GameManager.Instance.defensePointP.rotation);       
        MagicMan mg = magicman.GetComponent<MagicMan>();
        GameManager.Instance.player.Add(mg);        
        basePlayer.currentGold -= magicManPrice;
        limitUnitCurrent += 5;       
        rally.magics.Add(mg);
        loadingMagicMan.DOFillAmount(1, delayTimeMagicMan);
        loadingMagicManRed.DOFillAmount(0, delayTimeMagicMan);
        this.DelayCall(delayTimeMagicMan, () =>
        {
            delayAll = false;
        });
    }

    public void BuyTitan()
    {
        if (limitUnitCurrent >= limitUnit - 2 || basePlayer.currentGold < titanManPrice || delayAll)
            return;
        delayAll = true;
        loadingTitanMan.DOFillAmount(0, 0f);
        loadingTitanManRed.DOFillAmount(1, 0f);
        GameObject titan = Instantiate(TitanMan, GameManager.Instance.defensePointP.position, GameManager.Instance.defensePointP.rotation);       
        Titan tt = titan.GetComponent<Titan>();
        GameManager.Instance.player.Add(tt);
        basePlayer.currentGold -= titanManPrice;
        limitUnitCurrent += 3;        
        rally.titans.Add(tt);
        loadingTitanMan.DOFillAmount(1, delayTimeTitanMan);
        loadingTitanManRed.DOFillAmount(0, delayTimeTitanMan);
        this.DelayCall(delayTimeTitanMan, () =>
        {
            delayAll = false;
        });
    }
}
