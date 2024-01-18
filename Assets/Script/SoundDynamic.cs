using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDynamic : MonoBehaviour
{
    public Transform target;
    public AudioSource audioSource = null;   

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (target != null)
            transform.position = target.position;        
    }

    public void Initialize(Transform target)
    {
        this.target = target;
        DontDestroyOnLoad(gameObject);
    }

    public void SoundBackgroundChange(string soundName)
    {
        if (!AudioManager.Instance.audioClips.ContainsKey(soundName))
        {
            AudioClip audio = Resources.Load<AudioClip>($"Audio/{soundName}");
            AudioManager.Instance.audioClips.Add(soundName, audio);
        }
        audioSource.clip = AudioManager.Instance.audioClips[soundName];
        audioSource.Play();
    }

    public void PlayOneShot(string soundName)
    {
        audioSource.volume = PlayerPrefs.GetInt("SoundVolumn");
        if (!AudioManager.Instance.audioClips.ContainsKey(soundName))
        {
            AudioClip audio = Resources.Load<AudioClip>($"Audio/{soundName}");
            AudioManager.Instance.audioClips.Add(soundName, audio);
        }
        audioSource.PlayOneShot(AudioManager.Instance.audioClips[soundName]);
    }


}
