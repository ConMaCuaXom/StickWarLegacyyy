using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gem : MonoBehaviour
{
    public Text gem;

    private void Awake()
    {
        if (PlayerPrefs.GetString("FirstTime") != "No")
        {
            PlayerPrefs.SetInt("Gem", 0);
        }
        gem.text = PlayerPrefs.GetInt("Gem").ToString();
    }

    public void UpdateGem()
    {
        gem.text = PlayerPrefs.GetInt("Gem").ToString();
    }

    public void AddGem()
    {
        PlayerPrefs.SetInt("Gem", 1000);
        gem.text = PlayerPrefs.GetInt("Gem").ToString();
    }

}
