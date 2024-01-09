using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Skins : MonoBehaviour
{   
    public Button backToLevel;  
    public GameObject miner;
    public GameObject archer;
    public GameObject allUnit;  

    private void Awake()
    {     
        backToLevel.onClick.AddListener(BackToLevelScene);
    }
    private void OnDisable()
    {       
        backToLevel.onClick.RemoveAllListeners();
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
}
