using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherGoal : MonoBehaviour
{
    /*#region
    [SerializeField]
    private Sprite[] Arr;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private int defense = 6;
    #endregion

    private int i = 1;
    public BoxCollider2D col1;

    public AudioClip se1;
    AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        defense -= damage;
        // StartCoroutine(ApplyVibrationForDuration(10.0f));

        if (defense <= 0)
        {
            Crush();
        }

        if (i < Arr.Length)
        {
            spriteRenderer.sprite = Arr[i];
            i++;
        }
    }

    private void Crush()
    {
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

        GameDirector.Instance.crusherScore = 1000;

        // Destroy(gameObject);
    }*/
}
