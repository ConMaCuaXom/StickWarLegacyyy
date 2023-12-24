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

    [SerializeField] private Text textIndexSw;
    [SerializeField] private Text textIndexMn;
    [SerializeField] private Text textIndexAr;
    [SerializeField] private Text textIndexSp;
    [SerializeField] private Text textIndexMg;
    [SerializeField] private Text textIndexTt;



    public BaseEnemy baseEnemy => GameManager.Instance.baseEnemy;
    public BasePlayer basePlayer => GameManager.Instance.basePlayer;
    public Rally rally = null;

    
    public int limitUnit = 50;
    public int limitUnitCurrent = 0;

    public int indexSwordMan;
    public int indexMiner;
    public int indexArcher;
    public int indexSpearMan;
    public int indexMagicMan;
    public int indexTitanMan;

    public bool checkMiner = false;
    public bool checkLimit = false;

    public List<int> stt;

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
        indexSwordMan = 0;
        indexArcher = 0;
        indexSpearMan = 0;
        indexMagicMan = 0;
        indexTitanMan = 0;
        indexMiner = 0;
    }

    private void Update()
    {
        HowToBuy();
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

    public void AddMiner(int i)
    {
        for (int j = 0; j < i; j++)
        {
            GameObject miner = Instantiate(Miner, GameManager.Instance.defensePointP.position, GameManager.Instance.defensePointP.rotation);
            Miner mn = miner.GetComponent<Miner>();
            GameManager.Instance.player.Add(mn);
            rally.miners.Add(mn);            
        }
        
    }

    public void BuyMiner()
    {
        if (limitUnitCurrent < limitUnit && basePlayer.currentGold >= minerPrice && checkMiner == false)
        {
            basePlayer.currentGold -= minerPrice;
            limitUnitCurrent++;
            stt.Add(0);
            indexMiner++;
            if (indexMiner < 1)
                textIndexMn.text = "";
            else
                textIndexMn.text = (indexMiner).ToString();
        }       
    }
    public void BuyingMiner()
    {
        delayAll = true;
        loadingMiner.DOFillAmount(0, 0f);
        loadingMinerRed.DOFillAmount(1, 0f);
        loadingMinerRed.DOFillAmount(0, delayTimeMiner);
        loadingMiner.DOFillAmount(1, delayTimeMiner);
        this.DelayCall(delayTimeMiner, () =>
        {
            AddMiner(1);
            if (rally.miners.Count >= GameManager.Instance.goldInGoldMine.Count * 2)
                checkMiner = true;
            indexMiner--;
            if (indexMiner < 1)
                textIndexMn.text = "";
            else
                textIndexMn.text = (indexMiner).ToString();
            stt.RemoveAt(0);
            delayAll = false;
        });
    }


    public void HowToBuy()
    {
        if (stt.Count == 0 || delayAll)
            return;
        switch (stt[0])
        {
            case 0:
                BuyingMiner();
                break;
            case 1:
                BuyingSwordMan();
                break;
            case 2:
                BuyingArcher();
                break;
            case 3:
                BuyingSpearMan();
                break;
            case 4:
                BuyingMagicMan();
                break;
            case 5:
                BuyingTitanMan();
                break;
        }
    }

    public void BuySwordMan()
    {
        if (limitUnitCurrent < limitUnit && basePlayer.currentGold >= swordManPrice)
        {
            basePlayer.currentGold -= swordManPrice;
            limitUnitCurrent++;
            stt.Add(1);
            indexSwordMan++;                       
            if (indexSwordMan < 1)
                textIndexSw.text = "";
            else
                textIndexSw.text = (indexSwordMan).ToString();
        }                                 
    }

    public void BuyingSwordMan()
    {
        delayAll = true;
        loadingSwordMan.DOFillAmount(0, 0f);
        loadingSwordManRed.DOFillAmount(1, 0f);
        loadingSwordMan.DOFillAmount(1, delayTimeSwordMan);
        loadingSwordManRed.DOFillAmount(0, delayTimeSwordMan);
        this.DelayCall(delayTimeSwordMan, () =>
        {
            GameObject swordMan = Instantiate(Swordman, GameManager.Instance.defensePointP.position, GameManager.Instance.defensePointP.rotation);
            SwordMan sw = swordMan.GetComponent<SwordMan>();
            rally.swords.Add(sw);
            GameManager.Instance.player.Add(sw);
            indexSwordMan--;
            if (indexSwordMan < 1)
                textIndexSw.text = "";
            else
                textIndexSw.text = (indexSwordMan).ToString();
            stt.RemoveAt(0);
            delayAll = false;
        });
    }

    public void BuyArcher()
    {
        if (limitUnitCurrent < limitUnit && basePlayer.currentGold >= archerPrice)
        {
            basePlayer.currentGold -= archerPrice;
            limitUnitCurrent++;
            stt.Add(2);
            indexArcher++;
            if (indexArcher < 1)
                textIndexAr.text = "";
            else
                textIndexAr.text = (indexArcher).ToString();
        }       
    }

    public void BuyingArcher()
    {
        delayAll = true;
        loadingArcher.DOFillAmount(0, 0f);
        loadingArcherRed.DOFillAmount(1, 0f);
        loadingArcher.DOFillAmount(1, delayTimeArcher);
        loadingArcherRed.DOFillAmount(0, delayTimeArcher);
        this.DelayCall(delayTimeArcher, () =>
        {
            GameObject archer = Instantiate(Archer, GameManager.Instance.defensePointP.position, GameManager.Instance.defensePointP.rotation);
            Archer ar = archer.GetComponent<Archer>();
            GameManager.Instance.player.Add(ar);
            rally.archers.Add(ar);
            indexArcher--;
            if (indexArcher < 1)
                textIndexAr.text = "";
            else
                textIndexAr.text = (indexArcher).ToString();
            stt.RemoveAt(0);
            delayAll = false;
        });
    }

    public void BuySpearMan()
    {
        if (limitUnitCurrent < limitUnit - 2 && basePlayer.currentGold >= spearManPrice)
        {
            basePlayer.currentGold -= spearManPrice;
            limitUnitCurrent += 3;
            stt.Add(3);
            indexSpearMan++;
            if (indexSpearMan < 1)
                textIndexSp.text = "";
            else
                textIndexSp.text = (indexSpearMan).ToString();
        }       
    }

    public void BuyingSpearMan()
    {
        delayAll = true;
        loadingSpearMan.DOFillAmount(0, 0f);
        loadingSpearManRed.DOFillAmount(1, 0f);
        loadingSpearMan.DOFillAmount(1, delayTimeSpearMan);
        loadingSpearManRed.DOFillAmount(0, delayTimeSpearMan);
        this.DelayCall(delayTimeSpearMan, () =>
        {
            GameObject spearMan = Instantiate(Spearman, GameManager.Instance.defensePointP.position, GameManager.Instance.defensePointP.rotation);
            SpearMan sp = spearMan.GetComponent<SpearMan>();
            GameManager.Instance.player.Add(sp);
            rally.spears.Add(sp);
            indexSpearMan--;
            if (indexSpearMan < 1)
                textIndexSp.text = "";
            else
                textIndexSp.text = (indexSpearMan).ToString();
            stt.RemoveAt(0);
            delayAll = false;
        });
    }

    public void BuyMagicMan()
    {
        if (limitUnitCurrent < limitUnit - 4 && basePlayer.currentGold >= magicManPrice)
        {
            basePlayer.currentGold -= magicManPrice;
            limitUnitCurrent += 5;
            stt.Add(4);
            indexMagicMan++;
            if (indexMagicMan < 1)
                textIndexMg.text = "";
            else
                textIndexMg.text = (indexMagicMan).ToString();
        }       
    }

    public void BuyingMagicMan()
    {
        delayAll = true;
        loadingMagicMan.DOFillAmount(0, 0f);
        loadingMagicManRed.DOFillAmount(1, 0f);
        loadingMagicMan.DOFillAmount(1, delayTimeMagicMan);
        loadingMagicManRed.DOFillAmount(0, delayTimeMagicMan);
        this.DelayCall(delayTimeMagicMan, () =>
        {
            GameObject magicman = Instantiate(Magicman, GameManager.Instance.defensePointP.position, GameManager.Instance.defensePointP.rotation);
            MagicMan mg = magicman.GetComponent<MagicMan>();
            GameManager.Instance.player.Add(mg);
            rally.magics.Add(mg);
            indexMagicMan--;
            if (indexMagicMan < 1)
                textIndexMg.text = "";
            else
                textIndexMg.text = (indexMagicMan).ToString();
            stt.RemoveAt(0);
            delayAll = false;
        });
    }
    public void BuyTitan()
    {
        if (limitUnitCurrent < limitUnit - 2 && basePlayer.currentGold >= titanManPrice)
        {
            basePlayer.currentGold -= titanManPrice;
            limitUnitCurrent += 3;
            stt.Add(5);
            indexTitanMan++;
            if (indexTitanMan < 1)
                textIndexTt.text = "";
            else
                textIndexTt.text = (indexTitanMan).ToString();
        }
        if (limitUnitCurrent >= limitUnit - 2 || basePlayer.currentGold < titanManPrice || delayAll)
            return;
        delayAll = true;
        loadingTitanMan.DOFillAmount(0, 0f);
        loadingTitanManRed.DOFillAmount(1, 0f);                      
        loadingTitanMan.DOFillAmount(1, delayTimeTitanMan);
        loadingTitanManRed.DOFillAmount(0, delayTimeTitanMan);
        this.DelayCall(delayTimeTitanMan, () =>
        {
            GameObject titan = Instantiate(TitanMan, GameManager.Instance.defensePointP.position, GameManager.Instance.defensePointP.rotation);
            Titan tt = titan.GetComponent<Titan>();
            GameManager.Instance.player.Add(tt);
            rally.titans.Add(tt);
            delayAll = false;
        });
    }

    public void BuyingTitanMan()
    {
        delayAll = true;
        loadingTitanMan.DOFillAmount(0, 0f);
        loadingTitanManRed.DOFillAmount(1, 0f);
        loadingTitanMan.DOFillAmount(1, delayTimeTitanMan);
        loadingTitanManRed.DOFillAmount(0, delayTimeTitanMan);
        this.DelayCall(delayTimeTitanMan, () =>
        {
            GameObject titan = Instantiate(TitanMan, GameManager.Instance.defensePointP.position, GameManager.Instance.defensePointP.rotation);
            Titan tt = titan.GetComponent<Titan>();
            GameManager.Instance.player.Add(tt);
            rally.titans.Add(tt);
            indexTitanMan--;
            if (indexTitanMan < 1)
                textIndexTt.text = "";
            else
                textIndexTt.text = (indexTitanMan).ToString();
            stt.RemoveAt(0);
            delayAll = false;
        });
    }
}
