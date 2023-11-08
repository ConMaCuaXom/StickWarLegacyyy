using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    public float currentGold;
    public float totalGold = 300f;
    public float time = 0;
    public float timeToAddGold = 5f;
    public bool checkTime = true;
    public float goldAddAuto = 20;

    private void Awake()
    {
        currentGold = totalGold;
        
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
}
