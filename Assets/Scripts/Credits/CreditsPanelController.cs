using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsPanelController : MonoBehaviour
{
    #region
    [Header("文字送りのスピード"), SerializeField]
    private float scrollSpeed = 140.0f;
    [Header("パネルの最後の中心位置"), SerializeField]
    private float endPos = 3000.0f;
    [SerializeField]
    private RectTransform rectTransform;
    [Header("戻る音のaudioSource"), SerializeField]
    private AudioSource audioSource = null;
    [Header("戻る音"), SerializeField]
    private AudioClip sound;
    [Header("Bボタンで戻る"), SerializeField]
    private Text text;
    [Header("textのフェードアウト時間"), SerializeField]
    private float fadeDuration = 0.3f;
    [SerializeField]
    private ImageFade imageFade;
    #endregion

    #region
    private bool isEnded = false;
    private Vector3 panelPosition;
    private Coroutine creditsCoroutine;
    private bool firstPushB = false;
    // text のアルファ値
    private float textCurrentAlpha = 1.0f;
    // text のフェードアウト速度
    private float fadeSpeed;
    // text がフェード仕切ったか
    private bool isCompletedFade = false;
    #endregion

    private void Start()
    {
        imageFade.enabled = false;
        panelPosition = rectTransform.anchoredPosition;
        audioSource = GetComponent<AudioSource>();
        fadeSpeed = 1.0f / fadeDuration;
    }

    private void Update()
    {
        if (isEnded)
        {
            // Bボタンで戻るtextをフェードさせる
            if (!isCompletedFade && textCurrentAlpha > 0.0f)
            {
                textCurrentAlpha -= fadeSpeed * Time.deltaTime;
                if (textCurrentAlpha <= 0.0f)
                {
                    isCompletedFade = true;
                    textCurrentAlpha = 0.0f;
                }
                text.color = new Color(text.color.r, text.color.g, text.color.b, textCurrentAlpha);
            }
            creditsCoroutine = StartCoroutine(GoBackToScene());
        }
        else
        {
            // isEnded ではなく(Bボタンを押されているわけではなく)、endPosに達したら
            if (rectTransform.anchoredPosition.y >= endPos)
            {
                isEnded = true;
            }

            // isEnded ではなく(endPosに達しているわけではなく)、Bボタンが押されたら
            if (!firstPushB && Input.GetButtonDown("Jump"))
            {
                firstPushB = true;
                isEnded = true;
                audioSource.PlayOneShot(sound);
            }
        }

        // isEnded = true でも、endPosまで行っていなければ移動させること.
        if (rectTransform.anchoredPosition.y < endPos)
        {
            panelPosition.y += scrollSpeed * Time.deltaTime;
            rectTransform.anchoredPosition = panelPosition;
        }
    }

    private IEnumerator GoBackToScene()
    {
        imageFade.enabled = true;
        yield return new WaitForSeconds(1.0f);
        //Debug.Log("Go to ModeSelection");
        SceneManager.LoadScene("ModeSelection");
    }
}
