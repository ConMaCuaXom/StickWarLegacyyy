using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-600)]
public class SoundBackground : MonoBehaviour
{
    public string soundName;


    private void Awake()
    {
        AudioManager.Instance.BackgroundSoundChange(soundName);
    }

}
