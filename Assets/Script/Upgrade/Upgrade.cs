using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public Button backToPreviousScene;
    public Button backToScreen1;
    public Button nextToScreen2;
    public Button addSkillPoint;
    public GameObject screen1;
    public GameObject screen2;
    public TextMeshProUGUI skillPoint;

    private void Awake()
    {
        UpdateSkillPoint();
    }
    public void BackToLevelScene()
    {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }

    public void BackToScreen1()
    {
        screen1.SetActive(true);
        screen2.SetActive(false);
    }

    public void NextToScreen2()
    {
        screen1.SetActive(false);
        screen2.SetActive(true);
    }

    public void UpdateSkillPoint()
    {
        skillPoint.text = PlayerPrefs.GetInt("SkillPoint").ToString();
    }

    public void Add100SkillPoint()
    {
        PlayerPrefs.SetInt("SkillPoint", 100);
        skillPoint.text = PlayerPrefs.GetInt("SkillPoint").ToString();
    }
}
