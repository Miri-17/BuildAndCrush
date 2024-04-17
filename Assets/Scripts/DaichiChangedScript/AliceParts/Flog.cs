using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Flog : MonoBehaviour
{
    #region MyNote

    /*
        ( ..)φメモメモ
        歌うやつ
        範囲内に入ったときにアニメの変更と音符のクローン生成
        そのあとにベロだし

        ＞ベロにクラッシャーが巻き込まれていたら
        飲み込んでから吐き出し。

        ＞歌うやつに戻る
        */

    #endregion

    #region Animator

    private Animator _animator;
    private string animSing = "sing";

    #endregion

    #region

    [SerializeField] private GameObject[] flogsThreeNotes_GameObjects;
    [SerializeField] private GameObject flogNote;
    [SerializeField] private Transform[] notesFirePoint;
    [SerializeField] private GameObject surprised_GameObject;
    [SerializeField] private float waitForSing = 1f;
    [SerializeField] private AudioClip singSE;

    #endregion


    /// <summary>
    /// 歌う動作の開始
    /// </summary>
    private bool singing_Bool = false;

    private CancellationToken _ct;
    private float singCounter = 0;
    private AudioSource _audioSource;


    void Start()
    {
        surprised_GameObject.SetActive(false);
        _animator = GetComponent<Animator>();
        _ct = destroyCancellationToken;
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Crusher"))
        {
            singing_Bool = true;
            SingingFlog();
        }
    }

    private async void SingingFlog()
    {
        if (singing_Bool)
        {
            surprised_GameObject.SetActive(true);

            await UniTask.Delay(TimeSpan.FromSeconds(waitForSing));

            surprised_GameObject.SetActive(false);

            

            if (gameObject != null)
            {
                while (singing_Bool && singCounter == 0)
                {
                    singCounter++;
                    _audioSource.PlayOneShot(singSE);
                    _animator.SetBool(animSing, true);
                    Instantiate(flogsThreeNotes_GameObjects[0], notesFirePoint[0]);
                    await UniTask.Delay(TimeSpan.FromSeconds(waitForSing), cancellationToken: _ct); //waitForSing秒待つ
                    Instantiate(flogsThreeNotes_GameObjects[1], notesFirePoint[1]);
                    await UniTask.Delay(TimeSpan.FromSeconds(waitForSing), cancellationToken: _ct); //waitForSing秒待つ
                    Instantiate(flogsThreeNotes_GameObjects[2], notesFirePoint[2]);
                    await UniTask.Delay(TimeSpan.FromSeconds(waitForSing), cancellationToken: _ct); //waitForSing秒待つ
                    _animator.SetBool(animSing, false);
                    await UniTask.Delay(TimeSpan.FromSeconds(waitForSing　* 3), cancellationToken: _ct); //waitForSing秒待つ
                    singCounter = 0;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Crusher"))
        {
            singing_Bool = false;
        }
    }
}