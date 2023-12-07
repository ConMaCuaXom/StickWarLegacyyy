using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewConfig", menuName = "LevelConfig/NewConfig")]
public class ConfigLvObject : ScriptableObject
{
    [System.Serializable]
    public class Config
    {
        public int level;
        public SoldierType[] type;
    }

    public Config[] levels = null;
}

public enum SoldierType
{
    Miner,
    Archer,

}
