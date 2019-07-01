using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DestroyAfterSec : MonoBehaviour
{
    public float Time = 1;
    public bool OnlyDisable;

    private void Start()
    {
        if (OnlyDisable)
        {
            Invoke(() => gameObject.SetActive(false), Time);
            return;
        }
        Destroy(gameObject, Time);

            
    }

    private IEnumerator Invoke(Action action,float seconds)
    {
        yield return new WaitForSeconds(seconds);
        action();
    }
}
