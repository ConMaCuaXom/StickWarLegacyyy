using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public Button shop;
    public Button upgrade;
    public Button skins;
    public Button back;
    public Button resetLV;
    public Button unLockAllLV;

    public void OpenSkins()
    {
        SceneManager.LoadScene("Skins", LoadSceneMode.Single);
    }
    public void Back()
    {
        SceneManager.LoadScene("Open", LoadSceneMode.Single);
    }

    public void OpenUpgrade()
    {
        SceneManager.LoadScene("Upgrade", LoadSceneMode.Single);
    }

    public void ResetLV()
    {
        PlayerPrefs.SetInt("LvUnlock", 1);
    }

    public void UnlockAllLV()
    {
        PlayerPrefs.SetInt("LvUnlock", 10);
    }
}
