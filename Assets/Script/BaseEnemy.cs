using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public GameObject Miner;
    public GameObject Swordman;
    public GameObject Archer;
    public GameObject Spearman;
    public GameObject TitanMan;
    public GameObject Magicman;
    public WinOrLose wl;


    public float HP = 2000f;
    public float currentHP;

    private void Awake()
    {
        currentHP = HP;
    }
    
    public void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            wl.LoseOrWin();
        }
    }
}
