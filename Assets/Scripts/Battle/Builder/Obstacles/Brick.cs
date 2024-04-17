using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    #region
    [SerializeField]
    private Sprite[] brickArr;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private int defense = 4;
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
            Crush();
        }

        if (i < brickArr.Length)
        {
            spriteRenderer.sprite = brickArr[i];
            i++;
        }
    }

    private void Crush()
    {
        GetComponent<ParticleSystem>().Play();

        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        Destroy(boxCollider2D);
        Destroy(spriteRenderer);

        StartCoroutine(DelayedDestroy(2.0f));
    }

    private IEnumerator DelayedDestroy(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        Destroy(gameObject);
    }
}
