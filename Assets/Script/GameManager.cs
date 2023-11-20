using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-1000)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;   
    public List<GoldInGoldMine> goldInGoldMine = null;
    public List<Transform> rallyPoint = null;
    public List<Transform> rallyPointE = null;
    public List<BaseSoldier> player = null;
    public List<BaseSoldier> enemy = null;
    public List<CrossbowmanDefender> crossbowmanDefendersP = null;
    public BasePlayer basePlayer;
    public BaseEnemy baseEnemy;
    public Rally rally;
    public BuyUnit buyUnit;

    public Transform defensePointP = null;
    public Transform defensePointE = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Instance = null;
    }


}
