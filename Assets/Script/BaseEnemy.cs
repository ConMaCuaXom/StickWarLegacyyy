using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

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
    public Image healthBar;
    public Text DisplayCurrentHP;
    public BoxCollider boxCollider;

    private void Awake()
    {
        currentGold = totalGold;
        currentHP = HP;
        boxCollider = GetComponent<BoxCollider>();
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
            boxCollider.enabled = false;
        }
    }
    private void OnDrawGizmos()
    {        
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 25f);      
    }
}
