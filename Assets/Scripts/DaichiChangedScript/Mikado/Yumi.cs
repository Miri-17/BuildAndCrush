using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Yumi : MonoBehaviour
{
    [SerializeField] public GameObject arrow;
    [SerializeField] private float shotArrowSpan = 1f;

    [SerializeField] private Transform shotArrowPoint;

    private Animator _animator;

    private CancellationToken _ct;

    private async void Start()
    {
        _ct = destroyCancellationToken;
        while (gameObject != null)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(shotArrowSpan), cancellationToken: _ct);
            Instantiate(arrow, shotArrowPoint);
            await UniTask.Delay(TimeSpan.FromSeconds(shotArrowSpan), cancellationToken: _ct);
        }
    }
}