using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Dish : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float forceY = 10;

    [SerializeField] private float forceX = -10;
    [SerializeField] private AudioClip throwSE;
    [SerializeField] private bool useArrowModeA = false;
    [SerializeField] private bool useArrowModeB = false;

    private Tweener _tweener;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(throwSE);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
        if (useArrowModeA)
        {
            _tweener = transform.DORotate(new Vector3(0, 0, 10), 0.5f).SetRelative()
                .SetLoops(-1, LoopType.Incremental).SetLink(gameObject);
        }

        if (useArrowModeB)
        {
            _tweener = transform.DORotate(new Vector3(0, 0, 20), 0.5f).SetRelative()
                .SetLoops(-1, LoopType.Incremental).SetLink(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _tweener.Kill();
    }
}