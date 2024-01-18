using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-800)]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    public AudioSource audioSource = null;
    public GameObject dynamicSoundAsset = null;      
    public float soundVolume;

    public Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    public Dictionary<Transform,SoundDynamic> dynamicSoundActive = new Dictionary<Transform,SoundDynamic>();
    public List<GameObject> dynamicSoundPool = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(Instance);
        if (PlayerPrefs.GetString("FirstMusic") != "No")
            PlayerPrefs.SetInt("MusicVolumn", 1);
        if (PlayerPrefs.GetString("FirstSound") != "No")
            PlayerPrefs.SetInt("SoundVolumn", 1);
        if (PlayerPrefs.GetInt("MusicVolumn") == 1)
            audioSource.volume = 1;
        if (PlayerPrefs.GetInt("MusicVolumn") == 0)
            audioSource.volume = 0;       
    }

   

    public void BackgroundSoundChange(string soundName)
    {
        
        if (!audioClips.ContainsKey(soundName))
        {
            AudioClip audio = Resources.Load<AudioClip>($"Audio/{soundName}");
            audioClips.Add(soundName, audio);
        }
        if (audioSource.clip == audioClips[soundName])
            return;
        audioSource.clip = audioClips[soundName];
        //audioSource.volume = musicVolumn;
        audioSource.Play();       
    }

    public void PlayOneShot(string soundName, float ScaleVolumn)
    {
        if (!audioClips.ContainsKey(soundName))
        {
            AudioClip audio = Resources.Load<AudioClip>($"Audio/{soundName}");
            audioClips.Add(soundName, audio);
        }
        audioSource.PlayOneShot(audioClips[soundName], ScaleVolumn);
    }

    public void CreateDynamicSound(Transform target)
    {
        GameObject go = null;
        SoundDynamic sound = null;
        if (dynamicSoundPool != null && dynamicSoundPool.Count > 0)
        {
            go = dynamicSoundPool[0];
            dynamicSoundPool.RemoveAt(0);
            sound = go.GetComponent<SoundDynamic>();
        }
            
        if (sound == null)
        {
            go = Instantiate(dynamicSoundAsset);
            sound = go.GetComponent<SoundDynamic>();
        }
            
        dynamicSoundActive.Add(target, sound);
        sound.Initialize(target);
    }

    public void AddToPoolAgain(Transform target)
    {
        if (dynamicSoundActive.ContainsKey(target) && dynamicSoundActive[target] != null)
        {            
            dynamicSoundPool.Add(dynamicSoundActive[target].gameObject);
            dynamicSoundActive.Remove(target);
        }
    }
}
