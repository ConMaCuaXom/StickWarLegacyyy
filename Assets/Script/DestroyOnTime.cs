using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    public float time;
    public float timeToDestroy;

    private void Awake()
    {
        time = 0;
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= timeToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
