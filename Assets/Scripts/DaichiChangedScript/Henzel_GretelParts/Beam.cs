using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class Beam : MonoBehaviour
{
    //ビームの出る時間
    [SerializeField] private float beamShotStart = 2.07f;
    //閉じる時間
    [SerializeField] private float beamShotEnd = 1.04f;
    [SerializeField] private AudioClip beamShotFirstSE;
    [SerializeField] private AudioClip beamShotSecondSE;
    

    private BoxCollider2D _collider2D;
    private Animator _animator;
    private AudioSource _audioSource;

    private CancellationToken _ct;
    async void Start()
    {
        _ct = destroyCancellationToken;
        _audioSource = GetComponent<AudioSource>();
        _collider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        while (gameObject != null)
        {
            _animator.SetBool("shot",true);
            await UniTask.Delay(TimeSpan.FromSeconds(beamShotStart),cancellationToken: _ct);
            _collider2D.enabled = true;
            _audioSource.PlayOneShot(beamShotFirstSE);
            _audioSource.PlayOneShot(beamShotSecondSE);
            await UniTask.Delay(TimeSpan.FromSeconds(beamShotEnd), cancellationToken: _ct);
            _collider2D.enabled = false;
            _animator.SetBool("shot",false);
            await UniTask.Delay(TimeSpan.FromSeconds(3f), cancellationToken: _ct);
        }
    }
}