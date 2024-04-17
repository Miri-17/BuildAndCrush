using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class TenjinAttackEffect : MonoBehaviour
{
    private CancellationToken _ct;
    // Start is called before the first frame update
    async void Start()
    {
        _ct = destroyCancellationToken;
        transform.DOLocalMove(Vector3.right * 0.1f, 0.1f);
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f),cancellationToken : _ct);
        Destroy(gameObject);
    }
}
