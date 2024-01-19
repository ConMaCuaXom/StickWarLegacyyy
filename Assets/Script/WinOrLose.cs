using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public AudioSource audioSource;
    public Text currentGem;
    public Text currentSkillPoint;
    public List<GameObject> hide;

    private void Awake()
    {       
        win.SetActive(false);
        lose.SetActive(false);
        playerLose = false;
        playerWin = false;
        nextLv.onClick.AddListener(BackToLevelScene);      
        audioSource = GetComponent<AudioSource>();
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
                PlayerPrefs.SetInt("SkillPoint", PlayerPrefs.GetInt("SkillPoint") + 1);
            }
            currentGem.text = PlayerPrefs.GetInt("Gem").ToString();
            int gemNow = PlayerPrefs.GetInt("Gem");
            PlayerPrefs.SetInt("Gem", PlayerPrefs.GetInt("Gem") + 10);
            int gemNow2 = PlayerPrefs.GetInt("Gem");
            DOVirtual.Int(gemNow, gemNow2, 1f, (x) =>
            {
                currentGem.text = x.ToString();
            });                                   
            currentSkillPoint.text = PlayerPrefs.GetInt("SkillPoint").ToString();

            //AudioManager.Instance.PlayOneShot("Base_Destroy", PlayerPrefs.GetInt("SoundVolumn"));
            AudioManager.Instance.audioSource.Stop();       
            foreach (GameObject gameObject in hide)
            {
                gameObject.SetActive(false);
            }
        }
        if (baseEnemy.currentHP <= 0 && playerLose == false && LevelManager.currentLv == 10)
        {
            baseEnemy.currentHP = 0;
            finalBoss = true;
            //AudioManager.Instance.PlayOneShot("Base_Destroy", PlayerPrefs.GetInt("SoundVolumn"));
        }
            

        if (basePlayer.currentHP <= 0 && playerWin == false)
        {
            nextLv.gameObject.SetActive(true);
            basePlayer.currentHP = 0;
            lose.SetActive(true);
            playerLose = true;
            
            //AudioManager.Instance.PlayOneShot("Base_Destroy", PlayerPrefs.GetInt("SoundVolumn"));
            AudioManager.Instance.audioSource.Stop();
            foreach (GameObject gameObject in hide)
            {
                gameObject.SetActive(false);
            }
        }
    }
    public void CampaignComplete()
    {
        win.SetActive(true);
        nextLv.gameObject.SetActive(true);
        playerWin = true;
        currentGem.text = PlayerPrefs.GetInt("Gem").ToString();
        int gemNow = PlayerPrefs.GetInt("Gem");
        PlayerPrefs.SetInt("Gem", PlayerPrefs.GetInt("Gem") + 10);
        int gemNow2 = PlayerPrefs.GetInt("Gem");
        DOVirtual.Int(gemNow, gemNow2, 2f, (x) =>
        {
            currentGem.text = x.ToString();
        });
    }

    public void BackToLevelScene()
    {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
        //AudioManager.Instance.audioSource.Play();
    }   
}
