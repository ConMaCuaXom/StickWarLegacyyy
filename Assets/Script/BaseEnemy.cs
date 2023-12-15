using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{      
    public float currentGold;
    public float totalGold = 300f;
    public float time = 0;
    public float timeToAddGold = 5f;
    public bool checkTime = true;
    public float goldAddAuto = 20;
    public float HP = 2000f;
    public float currentHP;   
    public WinOrLose wl;        

    private void Awake()
    {
        currentGold = totalGold;
        currentHP = HP;
    }

    private void Update()
    {
        if (checkTime == false)
            return;
        AutoAddGold(goldAddAuto);

    }

    private void AutoAddGold(float gold)
    {
        checkTime = false;
        this.DelayCall(timeToAddGold, () =>
        {
            currentGold += gold;
            checkTime = true;
        });
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
