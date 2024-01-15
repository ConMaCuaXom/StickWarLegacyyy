using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyThis : MonoBehaviour
{
    public Toggle buy;
    public GameObject select;
    public GameObject owned;
    public GameObject price;
    public PriceSkins priceSkins;
    public Gem gem;
    public bool skinDefault;


    private void Start()
    {
        if (PlayerPrefs.GetString(priceSkins.name.ToString()) == "Paid" || skinDefault == true)
        {
            price.SetActive(false);
            owned.SetActive(true);
        }
        else
        {
            buy.enabled = false;
        }
        if (PlayerPrefs.GetInt(priceSkins.name.ToString() + "S") == 1)       
            buy.isOn = true;
        if (PlayerPrefs.GetInt(priceSkins.name.ToString() + "S") == 0)
            buy.isOn = false;
        if (skinDefault == true && PlayerPrefs.GetInt(priceSkins.name.ToString() + "S") == 0)
            buy.isOn = false;
    }

    private void Update()
    {
        
    }
    public void Buy()
    {
        if (PlayerPrefs.GetInt("Gem") >= priceSkins.price)
        {
            price.SetActive(false);
            //select.SetActive(true);
            owned.SetActive(true);
            PlayerPrefs.SetInt("Gem", PlayerPrefs.GetInt("Gem") - priceSkins.price);
            gem.UpdateGem();
            PlayerPrefs.SetString(priceSkins.name.ToString(), "Paid");
            buy.enabled = true;
            AudioManager.Instance.PlayOneShot("Buy_Skins",1);
        }       
    }

    public void Select()
    {       
        select.SetActive(true);
        PlayerPrefs.SetInt(priceSkins.name.ToString() + "S", 1);
        AudioManager.Instance.PlayOneShot("Select_Skins",1);
    }

    public void NoSelect()
    {
        select.SetActive(false);
        PlayerPrefs.SetInt(priceSkins.name.ToString() + "S", 0);
    }

    public void SelectOrNo()
    {
        if (buy.isOn)
            Select();
        else
            NoSelect();
    }
}
