using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinerSkins : MonoBehaviour
{
    public List<Toggle> minerBody;
    public List<Toggle> minerHead;
    public List<Toggle> minerWeapon;
    public Button next;
    public Button back;
    public GameObject buyBody;
    public GameObject buyHead;
    public GameObject buyWeapon;

    public void MinerB1()
    {
        if (minerBody[0].isOn)
            PlayerPrefs.SetString("MinerBody", "Body1");
    }
    public void MinerB2()
    {
        if (minerBody[1].isOn)
            PlayerPrefs.SetString("MinerBody", "Body2");
    }
    public void MinerB3()
    {
        if (minerBody[2].isOn)
            PlayerPrefs.SetString("MinerBody", "Body3");
    }
    public void MinerH1()
    {
        if (minerHead[0].isOn)
            PlayerPrefs.SetString("MinerHead", "Head1");
    }
    public void MinerH2()
    {
        if (minerHead[1].isOn)
            PlayerPrefs.SetString("MinerHead", "Head2");
    }
    public void MinerH3()
    {
        if (minerHead[2].isOn)
            PlayerPrefs.SetString("MinerHead", "Head3");
    }
    public void MinerW1()
    {
        if (minerWeapon[0].isOn)
            PlayerPrefs.SetString("MinerWeapon", "Weapon1");
    }
    public void MinerW2()
    {
        if (minerWeapon[1].isOn)
            PlayerPrefs.SetString("MinerWeapon", "Weapon2");
    }
    public void MinerW3()
    {
        if (minerWeapon[2].isOn)
            PlayerPrefs.SetString("MinerWeapon", "Weapon3");
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
