using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralObstacle : MonoBehaviour
{
    #region
    [SerializeField]
    private int defense = 3;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite[] spriteArr;
    [SerializeField]
    private int[] changeSpriteDefense;
    [SerializeField]
    private AudioClip crushSE;
    [SerializeField]
    private float delayTime = 1.0f;
    [SerializeField]
    private Collider2D[] col2D;
    #endregion

    private AudioSource audioSource;
    private int spriteNum = 1;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        defense -= damage;
        if (defense <= 0)
        {
            Crush();
        }

        if (defense <= changeSpriteDefense[spriteNum])
        {
            spriteRenderer.sprite = spriteArr[spriteNum];
            spriteNum++;
        }

       /* if (spriteNum < spriteArr.Length)
        {
            foreach (int i in changeSpriteDefense)
            {
                if ()
            }
        }*/

        /*if (spriteNum < arr.Length)
        {
            spriteRenderer.sprite = arr[spriteNum];
            spriteNum++;
        }*/
    }

    private void Crush()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(crushSE);
        }

        GetComponent<ParticleSystem>().Play();

        for (int i = 0; i < col2D.Length; i++)
        {
            Destroy(col2D[i]);
        }
        Destroy(spriteRenderer);

        StartCoroutine(DelayedDestroy(delayTime));
    }
    
    private IEnumerator DelayedDestroy(float t)
    {
        yield return new WaitForSeconds(t);

        Destroy(gameObject);
    }
}
