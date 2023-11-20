using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOrLose : MonoBehaviour
{
    public BaseEnemy enemy;
    public BasePlayer player;
    public GameObject win;
    public GameObject lose;
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
    }

    public void LoseOrWin()
    {
        if (enemy.currentHP <= 0 && playerLose == false)
        {
            win.SetActive(true);
            playerWin = true;
        }

        if (player.currentHP <= 0 && playerWin == false)
        {
            lose.SetActive(true);
            playerLose = true;
        }
    }
}
