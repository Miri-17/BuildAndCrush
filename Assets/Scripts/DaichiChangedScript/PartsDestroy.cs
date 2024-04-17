using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class PartsDestroy : MonoBehaviour
{
    #region

    [Header("ParticleSystemの有無")] [SerializeField]
    private bool usingParticleSystem = false;

    //  [Header("crushSEの有無")] [SerializeField]
    //private bool usingCrushSE = false;

    [SerializeField] private AudioClip crushSE;
    [SerializeField] private Sprite[] partsArr;
    [SerializeField] private int defense = 4;

    #endregion

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private ParticleSystem crushParticleSystem;
    private int i = 1;

    [SerializeField]private CapsuleCollider2D _capsuleCollider2D;
    [SerializeField]private BoxCollider2D _boxCollider2D;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        crushParticleSystem = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public async void TakeDamage(int damage)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(crushSE);
        }

        defense -= damage;
        if (defense <= 0)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            Crush();
        }

        if (i < partsArr.Length)
        {
            spriteRenderer.sprite = partsArr[i];
            i++;
        }
    }

    private async void Crush()
    {
        if (usingParticleSystem)
        {
            crushParticleSystem.Play();
        }
        CapsuleCollider2D capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        this.tag = "ObstacleGround";
        if (_capsuleCollider2D != null)
        {
            Destroy(_capsuleCollider2D);
        }

        if (_boxCollider2D != null)
        {
            
        Destroy(_boxCollider2D);
        }
        Destroy(spriteRenderer);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f),cancellationToken: destroyCancellationToken);
        Destroy(gameObject);
        //StartCoroutine(DelayedDestroy(1.0f));
    }

    /*private IEnumerator DelayedDestroy(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        Destroy(gameObject);
    }*/
}