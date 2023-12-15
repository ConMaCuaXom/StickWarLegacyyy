using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayRes : MonoBehaviour
{
    public Text text;
    public Text limit;
    public bool player;




    private void Update()
    {
        if (player)
        {
            text.text = "Gold: " + GameManager.Instance.basePlayer.currentGold.ToString();
            limit.text = "Limit: " + GameManager.Instance.buyUnit.limitUnitCurrent.ToString() + "/" + GameManager.Instance.buyUnit.limitUnit;
        }
        else
        {
            text.text = "Gold: " + GameManager.Instance.baseEnemy.currentGold.ToString();
            limit.text = "Limit: " + GameManager.Instance.testEnemy.limitUnitCurrent.ToString() + "/" + GameManager.Instance.buyUnit.limitUnit;
        }
        
    }
}
