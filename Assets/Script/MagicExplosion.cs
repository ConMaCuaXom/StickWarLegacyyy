using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagicExplosion : MonoBehaviour
{
    public float timeForHide = 2f;
    public float timeCurrent;
    public ParticleSystem particleSystemm;

    private void Awake()
    {
        particleSystemm = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        timeCurrent = 0;
    }

    private void Update()
    {
        timeCurrent += Time.deltaTime;
        if (timeCurrent >= timeForHide)
        {
            timeCurrent = 0;
            particleSystemm.gameObject.SetActive(false);
        }
            
    }
}
