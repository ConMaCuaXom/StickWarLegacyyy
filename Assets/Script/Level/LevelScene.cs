using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelScene : MonoBehaviour
{
    public int level;
    public GameObject lockObj;
    public Button button;

    private void Awake()
    {        
        if (level > PlayerPrefs.GetInt("LvUnlock"))
            lockObj.SetActive(true);
        else
            button.onClick.AddListener(GoToScene1);
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }


    public void GoToScene1()
    {
        LevelManager.currentLv = level;
        SceneManager.LoadScene("Scene1", LoadSceneMode.Single);       
    }
}
