using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcherSkins : MonoBehaviour
{
    public List<Toggle> archerBody;
    public List<Toggle> archerHead;
    public List<Toggle> archerWeapon;
    public Button next;
    public Button back;
    public GameObject buyBody;
    public GameObject buyHead;
    public GameObject buyWeapon;

    public void ArcherB1()
    {
        if (archerBody[0].isOn)
            PlayerPrefs.SetString("ArcherBody", "Body1");
    }
    public void ArcherB2()
    {
        if (archerBody[1].isOn)
            PlayerPrefs.SetString("ArcherBody", "Body2");
    }
    public void ArcherB3()
    {
        if (archerBody[2].isOn)
            PlayerPrefs.SetString("ArcherBody", "Body3");
    }
    public void ArcherH1()
    {
        if (archerHead[0].isOn)
            PlayerPrefs.SetString("ArcherHead", "Head1");
    }
    public void ArcherH2()
    {
        if (archerHead[1].isOn)
            PlayerPrefs.SetString("ArcherHead", "Head2");
    }
    public void ArcherH3()
    {
        if (archerHead[2].isOn)
            PlayerPrefs.SetString("ArcherHead", "Head3");
    }
    public void ArcherW1()
    {
        if (archerWeapon[0].isOn)
            PlayerPrefs.SetString("ArcherWeapon", "Weapon1");
    }
    public void ArcherW2()
    {
        if (archerWeapon[1].isOn)
            PlayerPrefs.SetString("ArcherWeapon", "Weapon2");
    }
    public void ArcherW3()
    {
        if (archerWeapon[2].isOn)
            PlayerPrefs.SetString("ArcherWeapon", "Weapon3");
    }
    public void NextPage()
    {
        if (buyHead.activeInHierarchy)
        {
            buyBody.SetActive(false);
            buyHead.SetActive(false);
            buyWeapon.SetActive(true);
        }
        if (buyBody.activeInHierarchy)
        {
            buyBody.SetActive(false);
            buyHead.SetActive(true);
            buyWeapon.SetActive(false);
        }
    }

    public void PreviousPage()
    {
        if (buyHead.activeInHierarchy)
        {
            buyBody.SetActive(true);
            buyHead.SetActive(false);
            buyWeapon.SetActive(false);
        }
        if (buyWeapon.activeInHierarchy)
        {
            buyBody.SetActive(false);
            buyHead.SetActive(true);
            buyWeapon.SetActive(false);
        }
    }
}
