using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldInGoldMine : MonoBehaviour
{
    public int totalGold = 10000;
    public int currentGold = 0;
    public int person = 0;

    private void Awake()
    {
        currentGold = totalGold;
    }

    private void Update()
    {
        ZeroGold();
    }

    public void TakeGold(int gold)
    {
        currentGold = currentGold - gold;
    }

    public void ZeroGold()
    {
        if (currentGold <= 0)
        {
            Destroy(gameObject);
        }
    }
}
