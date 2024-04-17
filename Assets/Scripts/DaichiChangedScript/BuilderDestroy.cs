using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class BuilderDestroy : MonoBehaviour
{
    #region

    [SerializeField] private AudioClip crushSE;
    [SerializeField] private int defense = 5;
    [SerializeField] private string backGroundName = "FindBuilderDefeat";

    #endregion
    
    private GameObject backGround;
    private Image backGroundImage;
    private AudioSource audioSource;
    private Animator _animator;

    private bool desroyCount = true;

    private CancellationToken _ct;
    private void Start()
    {
        backGround = GameObject.Find(backGroundName);
        Debug.Log(backGround);
        audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        backGroundImage = backGround.GetComponent<Image>();
        backGroundImage.DOColor(new Color(1, 0, 0, 0), 0.1f);
        _ct = destroyCancellationToken;
    }

    public void TakeDamage(int damage)
    {
        defense -= damage;

        if (audioSource != null)
        {
            audioSource.PlayOneShot(crushSE);
        }

        if (defense < 5)
        {
            Damage();
        }

        if (defense <= 0 && desroyCount)
        {
            Crush();
            desroyCount = false;
        }
    }

    private void Damage()
    {
        _animator.SetBool("damage",true);
    }

    private async void Crush()
    {
        //吹っ飛び、画面揺れ、シーン切り替え
        _animator.SetBool("defeat", true);
        backGroundImage.DOColor(new Color(1, 0.157f, 0.329f, 0.5f), 0.5f);
        await UniTask.Delay(TimeSpan.FromSeconds(0.8f), cancellationToken:_ct);
        transform.DORotate(new Vector3(0, 0, -75), 2);
        transform.DOLocalPath(
            new[]
            {
                new Vector3(100, 30f, 0),
                new Vector3(200f, -10f, 0f),
            },
            2f, PathType.CatmullRom).SetRelative().SetLoops(1, LoopType.Incremental).SetEase(Ease.Linear);
        await UniTask.Delay(TimeSpan.FromSeconds(1.9f), cancellationToken:_ct);


        backGroundImage.DOColor(new Color(1, 0.157f, 0.329f, 0), 0.8f);
        transform.DOLocalPath(
            new[]
            {
                new Vector3(30, 10f, 0),
                new Vector3(60f, -10f, 0f),
            },
            0.8f, PathType.CatmullRom).SetRelative().SetLoops(1, LoopType.Incremental);
        await UniTask.Delay(TimeSpan.FromSeconds(0.8f), cancellationToken: _ct);

        // SceneManager.LoadSceneAsync("CrusherResult");
        GameDirector.Instance.crusherWin = true;
        GameDirector.Instance.builderWin = false;
        SceneManager.LoadSceneAsync("Result");
    }
}