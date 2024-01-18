using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Character/CreateCharacterProp", fileName = "CharacterProp")]
public class Character : ScriptableObject
{
    public float hp;
    public float armor;
    public float dangerRange;
    public float attackRange;
    public float attackDamage;
    public float attackSpeed;
    public float timeToDestroy;  
    public float timeForHololo;
    public float goldInMinerMax;
    public float aoePushBack;
    public float moveSpeed;

    public int goldTake1time;
    public int maxTiny;

    public Character()
    {

    }
}
