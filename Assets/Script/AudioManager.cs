using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-800)]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    public AudioSource audioSource = null;

    public Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(Instance);
    }

    

    public void BackgroundSoundChange(string soundName)
    {
        if (!audioClips.ContainsKey(soundName))
        {
            AudioClip audio = Resources.Load<AudioClip>($"Audio/{soundName}");
            audioClips.Add(soundName, audio);
        }
        audioSource.clip = audioClips[soundName];
        audioSource.Play();
    }

    public void PlayOneShot(string soundName)
    {
        if (!audioClips.ContainsKey(soundName))
        {
            AudioClip audio = Resources.Load<AudioClip>($"Audio/{soundName}");
            audioClips.Add(soundName, audio);
        }
        audioSource.PlayOneShot(audioClips[soundName]);
    }
}
