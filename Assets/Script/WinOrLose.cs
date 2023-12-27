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
        if (baseEnemy.currentHP <= 0 && playerLose == false)
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
        }

        if (basePlayer.currentHP <= 0 && playerWin == false)
        {
            nextLv.gameObject.SetActive(true);
            basePlayer.currentHP = 0;
            lose.SetActive(true);
            playerLose = true;
        }
    }

    public void BackToLevelScene()
    {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }
}
