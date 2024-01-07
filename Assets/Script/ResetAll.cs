using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAll : MonoBehaviour
{
    public List<PriceSkins> skins;
    public Gem gem;
    public void ResetGem()
    {
        PlayerPrefs.SetInt("Gem", 0);
        gem.UpdateGem();
    }

    public void ResetSkins()
    {
        foreach (var skinss in skins)
        {
            PlayerPrefs.SetString(skinss.name.ToString(), "Unpaid");
        }
    }
}
