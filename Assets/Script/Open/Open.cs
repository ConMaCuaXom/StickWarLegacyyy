using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings.Switch;

public class Open : MonoBehaviour
{
    public Button play;   

    private void Awake()
    {
        PlayerPrefs.SetInt("LvUnlock", 1);
        play.onClick.AddListener(PlayGame);
        if (PlayerPrefs.GetString("FirstTime") != "No")
        {
            PlayerPrefs.GetInt("LvUnlock", 1);
        }
    }

    private void OnDisable()
    {
        play.onClick.RemoveAllListeners();
    }
    private void PlayGame()
    {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }   
}
