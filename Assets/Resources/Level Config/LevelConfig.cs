using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/CreateLevelConfig", fileName = "LevelConfig")]
public class LevelConfig : ScriptableObject
{    
    public float timeSpawnMiner;
    public float timeSpawnArcher;
    public float timeSpawnSwordMan;
    public float timeSpawnSpearMan;
    public float timeSpawnTitanMan;
    public float timeSpawnMagicMan;

    public float maxMiner;
    public float maxArcher;
    public float maxSwordMan;
    public float maxSpearMan;
    public float maxTitanMan;
    public float maxMagicMan;

    public float minCurrentHpForDef;

    public float firstGoldForPlayer;
    public float firstGoldForEnemy;

    public float currentHpForAddUnit;

    public float addGoldForEnemy;

    public LevelConfig()
    {
        
    }
}
