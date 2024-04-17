using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Robot : MonoBehaviour
{
    private CancellationToken _ct;

    async void Start()
    {
        this.transform.Rotate(new Vector3(0, 0, -10));
        _ct = destroyCancellationToken;
        transform.DOMoveY(100, 2).SetRelative(true).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutCubic).SetLink(gameObject);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f),cancellationToken: _ct);
        transform.DORotate(new Vector3(0, 0, 20), 2).SetRelative(true).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutBack).SetLink(gameObject);
    }
}
