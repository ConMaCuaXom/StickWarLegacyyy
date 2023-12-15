using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMode : MonoBehaviour
{
    public Toggle testMode;
    public GameObject UIEnemy;
    public BuyUnit buyUnit;
    public bool cheating;
    
    private void Start()
    {
        testMode.onValueChanged.AddListener(TestModeOn);
    }   
    
    public void TestModeOn(bool value)
    {
        UIEnemy.SetActive(testMode.isOn);
        if (testMode.isOn)
        {
            cheating = true;
            buyUnit.buyArcher.gameObject.SetActive(true);
            buyUnit.buySpearMan.gameObject.SetActive(true);
            buyUnit.buyTitanMan.gameObject.SetActive(true);
            buyUnit.buyMagicMan.gameObject.SetActive(true);
        }
        else
        {
            cheating = false;
        }
    }
}
