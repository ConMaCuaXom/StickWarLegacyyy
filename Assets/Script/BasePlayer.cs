using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePlayer : MonoBehaviour
{
    public float currentGold;
    public float totalGold = 300f;
    public float time = 0;
    public float timeToAddGold = 5f;
    public bool checkTime = true;
    public float goldAddAuto = 20;
    public float HP = 2000f;
    public float currentHP;
    public BuyUnit buyUnit;
    public WinOrLose wl;
    public Image healthBar;
    public Text DisplayCurrentHP;
    

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
        if (currentHP < 0)
            currentHP = 0;
        float ratio = currentHP / HP;
        healthBar.DOFillAmount(ratio, 0.25f);
        DisplayCurrentHP.text = currentHP.ToString();
        if (currentHP <= 0)
        {
            wl.LoseOrWin();
        }
    }
}
