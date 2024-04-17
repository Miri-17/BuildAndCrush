using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private float windTimeSpan = 3.0f;
    [SerializeField] private GameObject windEffect;
    private Animator _animator;
    private BoxCollider2D _collider2D;
    private CancellationToken _ct;

    async void Start()
    {
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<BoxCollider2D>();
        this.transform.Rotate(new Vector3(0, 180, 0));
        _ct = destroyCancellationToken;
        
        while (gameObject != null)
        {
            _animator.SetBool("wind" , true);
            _collider2D.enabled = true;
            windEffect.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(windTimeSpan), cancellationToken: _ct);
            _animator.SetBool("wind" , false);
            _collider2D.enabled = false;
            windEffect.SetActive(false);
            await UniTask.Delay(TimeSpan.FromSeconds(windTimeSpan), cancellationToken: _ct);
        }
    }
}