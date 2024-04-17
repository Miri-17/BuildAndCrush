using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Spike : MonoBehaviour
{
    #region
    [SerializeField]
    private Sprite[] spikeArr;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private int defense = 6;
    [SerializeField]
    private AudioClip crushSE;
    #endregion

    private AudioSource audioSource;
    private int i = 1;

    private void Start()
    {
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
            //Crush();
        }

        if (i < spikeArr.Length)
        {
            spriteRenderer.sprite = spikeArr[i];
            i++;
        }
    }

    /*private void Crush()
    {
        GetComponent<ParticleSystem>().Play();

        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        Destroy(boxCollider2D);
        Destroy(spriteRenderer);

        StartCoroutine(DelayedDestroy(1.0f));
    }*/

    private IEnumerator DelayedDestroy(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        Destroy(gameObject);
    }

    private async void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Crusher"))
        { 
            transform.DOMoveY(-1000, 5f).SetRelative();
            await UniTask.Delay(TimeSpan.FromSeconds(6f));
            Destroy(gameObject);
        }
    }
}
