using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumnSetting : MonoBehaviour
{
    public Toggle musicVolumn = null;
    public Toggle soundVolumn = null;

    private void Awake()
    {
        
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("MusicVolumn") == 1)
            musicVolumn.isOn = true;
        if (PlayerPrefs.GetInt("MusicVolumn") == 0)
            musicVolumn.isOn = false;
        if (PlayerPrefs.GetInt("SoundVolumn") == 1)
            soundVolumn.isOn = true;
        if (PlayerPrefs.GetInt("SoundVolumn") == 0)
            soundVolumn.isOn = false;
    }

    public void ChangeMusicVolumn()
    {
        if (musicVolumn.isOn)
        {
            AudioManager.Instance.audioSource.volume = 1;
            PlayerPrefs.SetInt("MusicVolumn", 1);
        }           
        else
        {
            AudioManager.Instance.audioSource.volume = 0;
            PlayerPrefs.SetInt("MusicVolumn", 0);
        }
        PlayerPrefs.SetString("FirstMusic", "No");
            
    }
    public void ChangeSoundVolumn()
    {
        if (soundVolumn.isOn)                   
            PlayerPrefs.SetInt("SoundVolumn", 1);        
        else                   
            PlayerPrefs.SetInt("SoundVolumn", 0);
        PlayerPrefs.SetString("FirstSound", "No");
    }
}
