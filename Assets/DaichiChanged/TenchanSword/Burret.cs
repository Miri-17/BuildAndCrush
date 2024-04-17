using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Burret : MonoBehaviour
{
    async void Start()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.02f));
        Destroy(gameObject);
    }
}