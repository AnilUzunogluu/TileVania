using System;
using System.Collections;
using UnityEngine;

public static class Utilities
{
    public static void DelayedExecute(MonoBehaviour monoBehaviour, float delay, Action action)
    {
        monoBehaviour.StartCoroutine(DelayedExecuteCoroutine(delay, action));
    }
    private static IEnumerator DelayedExecuteCoroutine(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    
    public static void ApplyToAll<T>(this T[] value, Action<T> action)
    {
        var count = value.Length;
        for (int i = 0; i < count; i++)
        {
            action?.Invoke(value[i]);
        }
    }
}