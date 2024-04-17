using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Bounce : MonoBehaviour
{
    [SerializeField] private float bounceTime = 1f;
    [SerializeField] private Vector2 bounceScale = new Vector2(2f, 2f);
    [SerializeField] private AudioClip bounceSE;
    private bool bounced = false;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!bounced && other.CompareTag("Crusher"))
        {
            _audioSource.PlayOneShot(bounceSE);
            transform.DOScale(bounceScale, bounceTime)
                .SetEase(Ease.OutElastic)
                .SetLoops(1);
            bounced = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (bounced && other.CompareTag("Crusher"))
        {
            transform.DOScale(new Vector2(1,1), bounceTime)
                .SetEase(Ease.OutElastic)
                .SetLoops(1);
            bounced = false;
        }
    }
    /*
    [Header("跳ね返す方向を選択してください")] [SerializeField]
    private BounceVector bounceVector;

    [SerializeField] private float bounceForce = 1000000f;

    private enum BounceVector
    {
        OverVector,
        UnderVector,
        LeftVector,
        RightVector
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (bounceVector)
        {
            case BounceVector.OverVector:
                other.rigidbody.AddForce(new Vector2(bounceForce, 0), ForceMode2D.Impulse);
                break;
            case BounceVector.UnderVector:
                other.rigidbody.AddForce(new Vector2(0, -bounceForce), ForceMode2D.Impulse);
                break;
            case BounceVector.RightVector:
                other.rigidbody.AddForce(new Vector2(bounceForce, 0), ForceMode2D.Impulse);
                break;
            case BounceVector.LeftVector:
                other.rigidbody.AddForce(new Vector2(-bounceForce, 0), ForceMode2D.Impulse);
                break;
        }

        if (other.collider.CompareTag("Crusher"))
        {
        }
    }*/
}