using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeConfig : MonoBehaviour
{
    public Character character;
    public Toggle upgrade1;    
    public Toggle upgrade2;
    public Toggle upgrade3;
    public Button resetLock;
    public GameObject background1;
    public GameObject background2;
    public GameObject background3;
    public GameObject checkMark1;
    public GameObject checkMark2;
    public GameObject checkMark3;
    public TextMeshProUGUI skillPoint;
   
    private void Awake()
    {
        if (PlayerPrefs.GetString(this.gameObject.name.ToString() + upgrade1.ToString()) == "Lock")
        {
            upgrade1.interactable = false;
            background1.SetActive(false);
            checkMark1.SetActive(true);
        }
        if (PlayerPrefs.GetString(this.gameObject.name.ToString() + upgrade2.ToString()) == "Lock")
        {
            upgrade2.interactable = false;
            background2.SetActive(false);
            checkMark2.SetActive(true);
        }
        if (PlayerPrefs.GetString(this.gameObject.name.ToString() + upgrade3.ToString()) == "Lock")
        {
            upgrade3.interactable = false;
            background3.SetActive(false);
            checkMark3.SetActive(true);
        }

    }
    public void ResetAllLock()
    {
        PlayerPrefs.SetString(this.gameObject.name.ToString() + upgrade1.ToString(), "UnLock");
        PlayerPrefs.SetString(this.gameObject.name.ToString() + upgrade2.ToString(), "UnLock");
        PlayerPrefs.SetString(this.gameObject.name.ToString() + upgrade3.ToString(), "UnLock");
        PlayerPrefs.SetFloat(this.gameObject.name.ToString() + "MaxHP", 0);
        PlayerPrefs.SetFloat(this.gameObject.name.ToString() + "ATK", 0);
        PlayerPrefs.SetFloat(this.gameObject.name.ToString() + "Range", 0);
        PlayerPrefs.SetFloat(this.gameObject.name.ToString() + "MoveSpeed", 0);
        PlayerPrefs.SetFloat(this.gameObject.name.ToString() + "MaxTiny", 0);
        PlayerPrefs.SetFloat(this.gameObject.name.ToString() + "MaxGoldTake", 0);
        PlayerPrefs.SetInt("SkillPoint", 0);
    }

    public void AddMaxHP(float number)
    {
        if (PlayerPrefs.GetInt("SkillPoint") >= 1)
            PlayerPrefs.SetFloat(this.gameObject.name.ToString() + "MaxHP",PlayerPrefs.GetFloat(this.gameObject.name.ToString() + "MaxHP") + ((character.hp * number) / 100) * 1.0f);       
    }

    public void AddATK(float number)
    {
        if (PlayerPrefs.GetInt("SkillPoint") >= 1)
            PlayerPrefs.SetFloat(this.gameObject.name.ToString() + "ATK", PlayerPrefs.GetFloat(this.gameObject.name.ToString() + "ATK") + ((character.attackDamage * number) / 100) * 1.0f);
    }

    public void AddRange(float number)
    {
        if (PlayerPrefs.GetInt("SkillPoint") >= 1)
            PlayerPrefs.SetFloat(this.gameObject.name.ToString() + "Range", PlayerPrefs.GetFloat(this.gameObject.name.ToString() + "Range") + ((character.attackRange * number) / 100) * 1.0f);
    }

    public void AddMoveSpeed(float number)
    {
        if (PlayerPrefs.GetInt("SkillPoint") >= 1)
            PlayerPrefs.SetFloat(this.gameObject.name.ToString() + "MoveSpeed", PlayerPrefs.GetFloat(this.gameObject.name.ToString() + "MoveSpeed") + ((character.moveSpeed * number) / 100) * 1.00f);
    }

    public void AddMaxTiny(int number)
    {
        if (PlayerPrefs.GetInt("SkillPoint") >= 1)
            PlayerPrefs.SetInt(this.gameObject.name.ToString() + "MaxTiny", number);
    }

    public void AddMaxGoldTake(float number)
    {
        if (PlayerPrefs.GetInt("SkillPoint") >= 1)
            PlayerPrefs.SetFloat(this.gameObject.name.ToString() + "MaxGoldTake", ((character.goldInMinerMax * number) / 100) * 1f);
    }

    public void NoInteractable(Toggle toggle)
    {
        if (PlayerPrefs.GetInt("SkillPoint") < 1)
            toggle.isOn = false;
        else
        {
            PlayerPrefs.SetInt("SkillPoint", PlayerPrefs.GetInt("SkillPoint") - 1);
            toggle.interactable = false;
            PlayerPrefs.SetString(this.gameObject.name.ToString() + toggle.ToString(), "Lock");
            skillPoint.text = PlayerPrefs.GetInt("SkillPoint").ToString();
        }       
    }   
}
