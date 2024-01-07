using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Skins : MonoBehaviour
{
    //public Toggle minerB1;
    //public Toggle minerB2;
    //public Toggle minerB3;

    public List<Toggle> minerBody;
    public List<Toggle> minerHead;
    public List<Toggle> minerWeapon;
    public Button backToLevel;
    public Button minerShop;
    public Button archerShop;
    public Button next;
    public Button back;
    public GameObject miner;
    public GameObject archer;
    public GameObject allUnit;
    public GameObject buyArmor;
    public GameObject buyHair;
    public GameObject buyTools;

    private void Awake()
    {     
        backToLevel.onClick.AddListener(BackToLevelScene);
    }
    private void OnDisable()
    {       
        backToLevel.onClick.RemoveAllListeners();
    }
    public void MinerB1()
    {
        if (minerBody[0].isOn)
            PlayerPrefs.SetString("MinerBody", "Body1");
    }
    public void MinerB2()
    {
        if (minerBody[1].isOn)
            PlayerPrefs.SetString("MinerBody", "Body2");
    }
    public void MinerB3()
    {
        if (minerBody[2].isOn)
            PlayerPrefs.SetString("MinerBody", "Body3");
    }
    public void MinerH1()
    {
        if (minerHead[0].isOn)
            PlayerPrefs.SetString("MinerHead", "Head1");
    }
    public void MinerH2()
    {
        if (minerHead[1].isOn)
            PlayerPrefs.SetString("MinerHead", "Head2");
    }
    public void MinerH3()
    {
        if (minerHead[2].isOn)
            PlayerPrefs.SetString("MinerHead", "Head3");
    }
    public void MinerW1()
    {
        if (minerWeapon[0].isOn)
            PlayerPrefs.SetString("MinerWeapon", "Weapon1");
    }
    public void MinerW2()
    {
        if (minerWeapon[1].isOn)
            PlayerPrefs.SetString("MinerWeapon", "Weapon2");
    }
    public void MinerW3()
    {
        if (minerWeapon[2].isOn)
            PlayerPrefs.SetString("MinerWeapon", "Weapon3");
    }

    public void BackToLevelScene()
    {
        if (miner.activeInHierarchy == false && archer.activeInHierarchy == false)
            SceneManager.LoadScene("Level", LoadSceneMode.Single);
        else
            BackToSelectAll();
    }
    public void SelectMiner()
    {
        allUnit.SetActive(false);
        miner.SetActive(true);
        archer.SetActive(false);
    }
    public void SelectArcher()
    {
        allUnit.SetActive(false);
        miner.SetActive(false);
        archer.SetActive(true);
    }

    public void BackToSelectAll()
    {
        allUnit.SetActive(true);
        miner.SetActive(false);
        archer.SetActive(false);
    }

    public void NextPage()
    {
        if (buyHair.activeInHierarchy)
        {
            buyArmor.SetActive(false);
            buyHair.SetActive(false);
            buyTools.SetActive(true);
        }
        if (buyArmor.activeInHierarchy)
        {
            buyArmor.SetActive(false);
            buyHair.SetActive(true);
            buyTools.SetActive(false);           
        }          
    }

    public void PreviousPage()
    {
        if (buyHair.activeInHierarchy)
        {
            buyArmor.SetActive(true);
            buyHair.SetActive(false);
            buyTools.SetActive(false);
        }
        if (buyTools.activeInHierarchy)
        {
            buyArmor.SetActive(false);
            buyHair.SetActive(true);
            buyTools.SetActive(false);
        }
    }
}
