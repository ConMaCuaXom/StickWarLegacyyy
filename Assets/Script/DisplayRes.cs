using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayRes : MonoBehaviour
{
    public Text text;
    public Text limit;





    private void Update()
    {
        text.text = "Gold: " + GameManager.Instance.basePlayer.currentGold.ToString();
        limit.text = "Limit: " + GameManager.Instance.buyUnit.limitUnitCurrent.ToString() + "/" + GameManager.Instance.buyUnit.limitUnit;
    }
}
