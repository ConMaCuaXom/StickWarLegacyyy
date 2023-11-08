using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtis 
{
    public static void DelayCall(this MonoBehaviour mono, float time, Action Callback)
    {
        mono.StartCoroutine(IEDelayCall(time, Callback));
    }


    public static IEnumerator IEDelayCall(float time, Action Callback)
    {
        yield return new WaitForSeconds(time);
        Callback?.Invoke();
    }
}
