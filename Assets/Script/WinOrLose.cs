using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinOrLose : MonoBehaviour
{
    public BaseEnemy baseEnemy => GameManager.Instance.baseEnemy;
    public BasePlayer basePlayer => GameManager.Instance.basePlayer;
    public GameObject win;
    public GameObject lose;
    public bool finalBoss = false;

    public Button nextLv;
    public bool playerWin;
    public bool playerLose;   

    private void Awake()
    {       
        win.SetActive(false);
        lose.SetActive(false);
        playerLose = false;
        playerWin = false;
        nextLv.onClick.AddListener(BackToLevelScene);      
    }

    private void OnDisable()
    {
        nextLv.onClick.RemoveAllListeners();
    }

    public void LoseOrWin()
    {
        if (baseEnemy.currentHP <= 0 && playerLose == false && LevelManager.currentLv < 10)
        {
            baseEnemy.currentHP = 0;
            win.SetActive(true);
            nextLv.gameObject.SetActive(true);
            playerWin = true;
            PlayerPrefs.SetString("FirstTime", "No");          
            if (LevelManager.currentLv == PlayerPrefs.GetInt("LvUnlock"))
            {                
                PlayerPrefs.SetInt("LvUnlock", LevelManager.currentLv + 1);               
            }
            PlayerPrefs.SetInt("Gem", PlayerPrefs.GetInt("Gem") + 10);
        }
        if (baseEnemy.currentHP <= 0 && playerLose == false && LevelManager.currentLv == 10)
        {
            baseEnemy.currentHP = 0;
            finalBoss = true;                                               
        }
            

        if (basePlayer.currentHP <= 0 && playerWin == false)
        {
            nextLv.gameObject.SetActive(true);
            basePlayer.currentHP = 0;
            lose.SetActive(true);
            playerLose = true;
        }
    }
    public void CampaignComplete()
    {
        win.SetActive(true);
        nextLv.gameObject.SetActive(true);
        playerWin = true;
    }

    public void BackToLevelScene()
    {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }
}
