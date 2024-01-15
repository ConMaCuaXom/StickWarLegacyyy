using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDynamicSound : MonoBehaviour
{
    public void Initialized()
    {
        AudioManager.Instance.CreateDynamicSound(transform);
    }

    public void OnDestroy()
    {
        AudioManager.Instance.AddToPoolAgain(transform);
    }
}
