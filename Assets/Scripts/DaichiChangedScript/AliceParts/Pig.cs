using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Pig : MonoBehaviour
{
    #region

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int defense = 4;
    [SerializeField] private AudioClip crushSE;
    [SerializeField]　private float pigRunSpeed = 100;

    #endregion

    private AudioSource audioSource;

    /// <summary>
    /// 本棚時の当たり判定
    /// </summary>
    private BoxCollider2D bookCollider2D = null;

    /// <summary>
    /// 豚さん時の当たり判定
    /// </summary>
    private CapsuleCollider2D pigCollider2D = null;

    private Animator pigAnimator = null;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pigAnimator = GetComponent<Animator>();
        bookCollider2D = GetComponent<BoxCollider2D>();
        pigCollider2D = GetComponent<CapsuleCollider2D>();
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

        if (defense < 4)
        {
            pigAnimator.SetBool("hit", true);
            transform.DOMoveX(-1 * pigRunSpeed,2)
                .SetRelative()
                .SetLoops(-1,LoopType.Incremental)
                .SetLink(gameObject);
            this.gameObject.tag = "Obstacle";
            Destroy(bookCollider2D);
        }
    }

    private void Crush()
    {
        Destroy(spriteRenderer);

        StartCoroutine(DelayedDestroy(0f));
    }

    private IEnumerator DelayedDestroy(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(gameObject);
    }
}