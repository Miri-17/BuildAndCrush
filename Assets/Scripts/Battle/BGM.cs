using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audioClip;
    [SerializeField]
    private AudioClip[] audioClipWagon;

    private AudioSource audioSource0;
    private AudioSource audioSource1;
    private int nowAS = 0;
    private float fadeOutSeconds = 1.0f;
    private float fadeDeltaTime = 0;
    private bool fadeInOut = false;

    [SerializeField]
    private BuilderController builderController;

    private void Start()
    {
        audioSource0 = gameObject.AddComponent<AudioSource>();
        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource0.clip = audioClip[GameDirector.Instance.builderIndex];
        audioSource1.clip = audioClipWagon[GameDirector.Instance.builderIndex];

        audioSource0.loop = true;
        audioSource1.loop = true;

        audioSource0.Play();
        audioSource1.Play();

        audioSource0.volume = 1;
        audioSource1.volume = 0;
    }

    private void Update()
    {
        if (fadeInOut)
        {
            if (nowAS == 0)
            {
                fadeDeltaTime += Time.deltaTime;
                if (fadeDeltaTime > fadeOutSeconds)
                {
                    fadeDeltaTime = fadeOutSeconds;
                    fadeInOut = false;
                }
                audioSource0.volume = fadeDeltaTime / fadeOutSeconds;
                audioSource1.volume = 1 - fadeDeltaTime / fadeOutSeconds;
            }
            else if (nowAS == 1)
            {
                fadeDeltaTime += Time.deltaTime;
                if (fadeDeltaTime > fadeOutSeconds)
                {
                    fadeDeltaTime = fadeOutSeconds;
                    fadeInOut = false;
                }
                audioSource1.volume = fadeDeltaTime / fadeOutSeconds;
                audioSource0.volume = 1 - fadeDeltaTime / fadeOutSeconds;
            }
        }

        if (builderController.wagonControllerRun != null)
        {
            // ワゴンに乗ったら音楽を変更.
            if (builderController.wagonControllerRun.crusherEnterCheck.isOn)
            {
                ChangeBGM(1);
            }

            //ワゴンから降りたら音楽を変更.
            if (builderController.wagonControllerRun.crusherExitCheck.isOn)
            {
                Debug.Log("changeBGM0");
                ChangeBGM(0);
            }
        }
    }

    /// <summary>
    /// BGMを変更するスクリプト.
    /// 引数: 0...wagonに乗っていない時のBGM / 1...wagonに乗っている時のBGM.
    /// </summary>
    /// <param name="nas"></param>
    private void ChangeBGM(int nas)
    {
        if (nowAS != nas)
        {
            nowAS = nas;
            fadeDeltaTime = 0;
            fadeInOut = true;
        }
    }
}
