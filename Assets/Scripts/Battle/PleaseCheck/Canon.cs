using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Canon : MonoBehaviour
{
    /*#region
    [SerializeField]
    private Sprite[] Arr;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private int defense = 4;
    #endregion

    public BoxCollider2D col1;

    public AudioClip se1;
    public AudioClip attacked;
    AudioSource audioSource;

    private int i = 1;
    public GameObject cannonballPrefab;
    public Transform firePoint;
    public float fireForce = 10.0f;
    public float fireInterval = 1.0f;

    private void Start()
    {

        audioSource = GetComponent<AudioSource>();
        StartCoroutine(AutoFireCannonball());
        transform.Rotate(new Vector3(0, 0, 90));
    }

    IEnumerator AutoFireCannonball()
    {
        while (true)
        {
            FireCannonball();

            yield return new WaitForSeconds(fireInterval);
        }
    }

    void FireCannonball()
    {
        GameObject cannonball = Instantiate(cannonballPrefab, firePoint.position, firePoint.rotation);
    }
        public void TakeDamage(int damage)
    {
        defense -= damage;
        if (defense <= 0)
        {
            Crush();
        }

        if (i < Arr.Length)
        {
            if (audioSource != null)
            {
                audioSource.PlayOneShot(attacked);
            }
            spriteRenderer.sprite = Arr[i];
            i++;
        }
    }

    private void Crush()
    {
        GetComponent<ParticleSystem>().Play();

        GetComponent<SpriteRenderer>().enabled = false;
        col1.enabled = false;
        if (audioSource != null)
        {
            audioSource.PlayOneShot(se1);
        }
        StartCoroutine(DelayedDestroy(1.0f));

        //Destroy(gameObject);
    }

    private IEnumerator DelayedDestroy(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        Destroy(gameObject);
    }
*/

    #region

    [SerializeField] private Sprite[] canonArr;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int defense = 8;
    [SerializeField] private AudioClip crushSE;
    [SerializeField] private Transform shotPos;
    [SerializeField] private GameObject shotBurret;
    [SerializeField] private float shotDelay;

    #endregion

    private AudioSource audioSource;
    private int i = 1;

    private async void Start()
    {
        audioSource = GetComponent<AudioSource>();
        while (this.gameObject != null)
        {
            Instantiate(shotBurret, shotPos.position, shotPos.rotation);
            await UniTask.Delay(TimeSpan.FromSeconds(shotDelay));
        }
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

        if (i < canonArr.Length)
        {
            spriteRenderer.sprite = canonArr[i];
            i++;
        }
    }

    private void Crush()
    {
        GetComponent<ParticleSystem>().Play();

        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        Destroy(boxCollider2D);
        Destroy(spriteRenderer);

        StartCoroutine(DelayedDestroy(1.0f));
    }

    private IEnumerator DelayedDestroy(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        Destroy(gameObject);
    }
}