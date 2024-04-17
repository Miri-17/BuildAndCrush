using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    #region
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int defense = 4;
    [SerializeField] private AudioClip crushSE;
    #endregion

    /// <summary>
    /// 雑魚狼のアニメーター
    /// </summary>
    private Animator zakoAnimator = null;

    private AudioSource audioSource;

    /// <summary>
    /// クラッシャーの接近判定
    /// </summary>
    private bool enterCrusher = false;

    /// <summary>
    /// 狼ダッシュ
    /// </summary>
    [SerializeField] private float wolfRunSpeed = 500f;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        zakoAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (enterCrusher)
        {
            transform.Translate(-1 * wolfRunSpeed * Time.deltaTime, 0, 0);
            zakoAnimator.SetBool("run",true);
            StartCoroutine("DestroyZakoWolf", 3.0f);
        }

        if (this.transform.position.x < -280.0f)
        {
            Destroy(this.gameObject);
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
    }

    private void Crush()
    {
        /*GetComponent<ParticleSystem>().Play();*/

        CapsuleCollider2D capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        Destroy(capsuleCollider2D);
        Destroy(spriteRenderer);

        StartCoroutine(DelayedDestroy(0f));
    }

    private IEnumerator DestroyZakoWolf(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(DelayedDestroy(0f));
        // Destroy(this.gameObject);
    }

    private IEnumerator DelayedDestroy(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //クラッシャーが接近判定の範囲内に入ったときの挙動
        if (other.CompareTag("Crusher"))
        {
            enterCrusher = true;
        }     
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //地面以外にぶつかったときの方向転換（多分間違えてる）
        if (!other.collider.CompareTag("Ground"))
        {
            if (gameObject.transform.rotation == Quaternion.Euler(0, 0, 0))
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}