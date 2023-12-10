using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinOrLose : MonoBehaviour
{
    public BaseEnemy enemy;
    public BasePlayer player;
    public GameObject win;
    public GameObject lose;

    public Button nextLv;
    public bool playerWin;
    public bool playerLose;

    private void Awake()
    {
        enemy = GameManager.Instance.baseEnemy;
        player = GameManager.Instance.basePlayer;
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
        if (enemy.currentHP <= 0 && playerLose == false)
        {
            win.SetActive(true);
            nextLv.gameObject.SetActive(true);
            playerWin = true;
            PlayerPrefs.SetString("FirstTime", "No");
            
            if (LevelManager.currentLv == PlayerPrefs.GetInt("LvUnlock"))
            {
                
                PlayerPrefs.SetInt("LvUnlock", LevelManager.currentLv + 1);
                
            }
        }

        if (player.currentHP <= 0 && playerWin == false)
        {
            lose.SetActive(true);
            playerLose = true;
        }
    }

    public void BackToLevelScene()
    {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }
}
