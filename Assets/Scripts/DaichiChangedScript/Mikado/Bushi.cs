using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Bushi : MonoBehaviour
{
    #region

    [SerializeField] private int defense = 4;
    [SerializeField] private AudioClip crushSE;
    [SerializeField] private float attackSpan = 0.5f;
    [SerializeField] private GameObject swordHitCollider;
    [SerializeField] private GameObject surprised_GameObject;

    #endregion

    private Animator _animator;

    private AudioSource audioSource;
    private bool guard = true;

    private CancellationToken _ct;
    private float attackCounter;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _ct = destroyCancellationToken;
        attackCounter = 0;
        swordHitCollider.SetActive(false);
    }


    public async void TakeDamage(int damage)
    {
        Debug.Log(damage);
        if (audioSource != null)
        {
            audioSource.PlayOneShot(crushSE);
        }

        if (guard == false)
        {
            defense -= damage;
        }
        else if (guard)
        {
            _animator.SetBool("guard", true);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f),cancellationToken: _ct);
            _animator.SetBool("guard", false);
        }

        if (defense <= 0)
        {
            Crush();
        }

        if (defense < 4)
        {
        }
    }

    private void Crush()
    {
        StartCoroutine(DelayedDestroy(0f));
    }

    private IEnumerator DelayedDestroy(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        Destroy(gameObject);
    }

    private async void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Crusher"))
        {
            guard = false;
            while (!guard && attackCounter == 0)
            {
                attackCounter ++;
                surprised_GameObject.SetActive(true);
                _animator.SetBool("attack", true);
                _animator.SetBool("guard", false);
                await UniTask.Delay(TimeSpan.FromSeconds(attackSpan * 0.5), cancellationToken: _ct);
                surprised_GameObject.SetActive(false);
                swordHitCollider.SetActive(true);
                await UniTask.Delay(TimeSpan.FromSeconds(attackSpan * 0.5), cancellationToken: _ct);
                swordHitCollider.SetActive(false);
                await UniTask.Delay(TimeSpan.FromSeconds(attackSpan), cancellationToken: _ct);
                _animator.SetBool("attack", false);
                await UniTask.Delay(TimeSpan.FromSeconds(attackSpan), cancellationToken: _ct);
                attackCounter = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Crusher"))
        {
            guard = true;
        }
    }
}