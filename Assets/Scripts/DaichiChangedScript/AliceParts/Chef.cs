using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Chef : MonoBehaviour
{
    [SerializeField] private GameObject dish;
    [SerializeField] private float throwDishSpan = 1f;

    [FormerlySerializedAs("throwDishTransform")] [SerializeField]
    private Transform throwDishPoint;

    private Animator _animator;

    private CancellationToken _ct;

    async private void Start()
    {
        
        _animator = GetComponent<Animator>();
        _ct = destroyCancellationToken;
        
        while (gameObject != null)
        {
            _animator.SetBool("throwing", true);
            await UniTask.Delay(TimeSpan.FromSeconds(throwDishSpan), cancellationToken: _ct);
            Instantiate(dish, throwDishPoint);
            await UniTask.Delay(TimeSpan.FromSeconds(throwDishSpan), cancellationToken: _ct);
            Instantiate(dish, throwDishPoint);
            await UniTask.Delay(TimeSpan.FromSeconds(throwDishSpan), cancellationToken: _ct);
            _animator.SetBool("throwing", false);
            Instantiate(dish, throwDishPoint);
            await UniTask.Delay(TimeSpan.FromSeconds(throwDishSpan), cancellationToken: _ct);
        }
    }
}