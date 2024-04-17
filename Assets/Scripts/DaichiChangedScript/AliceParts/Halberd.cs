using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Halberd : MonoBehaviour
{
    #region

    [SerializeField] private Sprite[] halberdArr;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int defense = 6;
    [SerializeField] private AudioClip crushSE;

    #endregion

    private AudioSource audioSource;
    private int i = 1;

    [SerializeField] private float halberdRotateSpeed = 1f;

    private void Start()
    {
        //Dotweenによる挙動
        transform.DORotate(new Vector3(0, 0, 360), halberdRotateSpeed, RotateMode.LocalAxisAdd)
            .SetEase(Ease.InBack)
            .SetLoops(-1);
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(crushSE);
        }

        defense -= damage;
        if (defense <= 0)
        {
            Crush();
        }

        if (i < halberdArr.Length)
        {
            spriteRenderer.sprite = halberdArr[i];
            i++;
        }
    }

    private void Crush()
    {
        GetComponent<ParticleSystem>().Play();

        CapsuleCollider2D　capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        Destroy(capsuleCollider2D);
        Destroy(spriteRenderer);

        StartCoroutine(DelayedDestroy(0f));
    }

    private IEnumerator DelayedDestroy(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        Destroy(gameObject);
    }
}