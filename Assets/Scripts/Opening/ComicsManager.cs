using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ComicsManager : MonoBehaviour
{
    #region // OpeningControllerでも使う変数
    [HideInInspector]
    public bool isCompleted = false;
    [HideInInspector]
    public bool isSkipEnabled = true;
    #endregion

    public ComicsPanels[] comicsPanels;

    [SerializeField]
    private Text comicsText;
    [SerializeField]
    private float fadeDuration = 0.3f;
    [SerializeField]
    private Text skipText;

    private bool firstPushY = false;
    private bool isSetSprite = false;
    private bool isCompletedFade = false;
    private int panelNumber = 0;
    private int frameNumber = 0;
    private int sentencesNumber = 0;
    private int charactersNumber = 0;
    private string displaySentence = "";
    private float currentAlpha = 0.0f;
    private float fadeSpeed;
    private float feedTime = 0.05f;
    private float time = 0.0f;
    private float textCurrentAlpha = 1.0f;

    private void Start()
    {
        comicsPanels[panelNumber].panel.SetActive(true);

        comicsPanels[panelNumber].comicsFrames[frameNumber].comicsImage.sprite = comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSprite;
        comicsPanels[panelNumber].comicsFrames[frameNumber].comicsImage.color = new Color(comicsPanels[panelNumber].comicsFrames[frameNumber].comicsImage.color.r,
                                                                                          comicsPanels[panelNumber].comicsFrames[frameNumber].comicsImage.color.g,
                                                                                          comicsPanels[panelNumber].comicsFrames[frameNumber].comicsImage.color.b, currentAlpha);
        fadeSpeed = 1.0f / fadeDuration;

        displaySentence = comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber][charactersNumber].ToString();
        comicsText.text = displaySentence;
    }

    private void Update()
    {
        // if the image of one flame is not completely displayed
        if (!isSetSprite)
        {
            // continue to fade in the image
            if (comicsPanels[panelNumber].comicsFrames[frameNumber].isFadeIn)
            {
                if (currentAlpha < 1.0f)
                {
                    currentAlpha += fadeSpeed * Time.deltaTime;
                    if (currentAlpha >= 1.0f)
                    {
                        isSetSprite = true;
                        currentAlpha = 1.0f;
                    }
                }
            }
            else
            {
                if (currentAlpha > 0.0f)
                {
                    currentAlpha -= fadeSpeed / 3 * Time.deltaTime;
                    if (currentAlpha <= 0.0f)
                    {
                        isSetSprite = true;
                        currentAlpha = 0.0f;
                    }
                }
            }
            comicsPanels[panelNumber].comicsFrames[frameNumber].comicsImage.color = new Color(comicsPanels[panelNumber].comicsFrames[frameNumber].comicsImage.color.r,
                                                                                          comicsPanels[panelNumber].comicsFrames[frameNumber].comicsImage.color.g,
                                                                                          comicsPanels[panelNumber].comicsFrames[frameNumber].comicsImage.color.b, currentAlpha);
        }

        // 最後のパネルの最後の画像、文章が表示されていたら
        if (isSetSprite && panelNumber >= comicsPanels.Length - 1 && frameNumber >= comicsPanels[panelNumber].comicsFrames.Length - 1
                            && sentencesNumber >= comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences.Length - 1)
        {   
            // スキップをできなくする
            isSkipEnabled = false;
        }

        // textのフェードインが完了していない && スキップ可能状態になっていなければ
        if (!isCompletedFade && !isSkipEnabled)
        {
            // 文字をフェードさせる
            if (textCurrentAlpha > 0.0f)
            {
                textCurrentAlpha -= fadeSpeed * 2 * Time.deltaTime; //
                if (textCurrentAlpha <= 0.0f)
                {
                    isCompletedFade = true;
                    textCurrentAlpha = 0.0f;
                }
                skipText.color = new Color(skipText.color.r, skipText.color.g, skipText.color.b, textCurrentAlpha);
            }
        }

        // 最後のコマの画像、文字が配置され終わっていたら
        if (isSetSprite && panelNumber >= comicsPanels.Length - 1 && frameNumber >= comicsPanels[panelNumber].comicsFrames.Length - 1
                            && sentencesNumber >= comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences.Length - 1
                            && charactersNumber >= comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber].Length - 1)
        {
            // 漫画を完了させる
            isCompleted = true;
            return;
        }

        // Panelの全てのスプライトが配置し終わっていなかったら
        if (frameNumber < comicsPanels[panelNumber].comicsFrames.Length - 1)
        {
            // 1コマに対する文が全て配置されていなければ
            if (sentencesNumber < comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences.Length - 1)
            {
                // 文が全て配置されていなければ
                if (charactersNumber < comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber].Length - 1)
                {
                    // 最初にYボタンを押した時に
                    if (!firstPushY && Input.GetButtonDown("select"))
                    {
                        firstPushY = true;
                        StartCoroutine("ResetFirstPushY");
                        // 文の完全配置ができる
                        charactersNumber = comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber].Length - 1;
                        comicsText.text = comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber];
                        return;
                    }

                    // 文字ごとに配置する
                    time += Time.deltaTime;
                    if (time >= feedTime)
                    {
                        time = 0.0f;

                        charactersNumber++;
                        displaySentence = displaySentence + comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber][charactersNumber];
                        comicsText.text = displaySentence;
                    }
                }
                // 文が全て配置されていれば
                else
                {
                    // 最初にYボタンを押した時に
                    if (!firstPushY && Input.GetButtonDown("select"))
                    {
                        firstPushY = true;
                        StartCoroutine("ResetFirstPushY");
                        // 文字に関する変数をリセットする
                        sentencesNumber++;
                        charactersNumber = 0;
                        // 次の文章を取得する
                        displaySentence = comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber][charactersNumber].ToString();
                        comicsText.text = displaySentence;
                    }
                }
            }
            // 1コマに対する文が全て配置されていれば
            else
            {
                // 文が全て配置されていなければ
                if (charactersNumber < comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber].Length - 1)
                {
                    // 最初にYボタンを押した時に
                    if (!firstPushY && Input.GetButtonDown("select"))
                    {
                        firstPushY = true;
                        StartCoroutine("ResetFirstPushY");
                        // 文の完全配置ができる
                        charactersNumber = comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber].Length - 1;
                        comicsText.text = comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber];
                        return;
                    }

                    // 文字ごとに配置する
                    time += Time.deltaTime;
                    if (time >= feedTime)
                    {
                        time = 0.0f;

                        charactersNumber++;

                        displaySentence = displaySentence + comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber][charactersNumber];
                        comicsText.text = displaySentence;
                    }
                }
                // 文が全て配置されていれば
                else
                {
                    // 最初にYボタンを押した時に
                    if (isSetSprite && !firstPushY && Input.GetButtonDown("select"))
                    {
                        firstPushY = true;
                        StartCoroutine("ResetFirstPushY");

                        frameNumber++;  // 次のフレームに移り
                        // だんだん画像を出現させる
                        isSetSprite = false;
                        if (comicsPanels[panelNumber].comicsFrames[frameNumber].isFadeIn)
                        {
                            currentAlpha = 0.0f;
                        }
                        else
                        {
                            currentAlpha = 1.0f;
                        }
                        sentencesNumber = 0;
                        charactersNumber = 0;
                        
                        comicsPanels[panelNumber].comicsFrames[frameNumber].comicsImage.sprite = comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSprite;
                        comicsPanels[panelNumber].comicsFrames[frameNumber].comicsImage.color = new Color(comicsPanels[panelNumber].comicsFrames[frameNumber].comicsImage.color.r,
                                                                                                          comicsPanels[panelNumber].comicsFrames[frameNumber].comicsImage.color.g,
                                                                                                          comicsPanels[panelNumber].comicsFrames[frameNumber].comicsImage.color.b, currentAlpha);

                        displaySentence = comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber][charactersNumber].ToString();
                        comicsText.text = displaySentence;
                    }
                }
            }
        }
        // Panelの全てのスプライトが配置し終わっていたら
        else
        {
            // 1コマに対する文が全て配置されていなければ
            if (sentencesNumber < comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences.Length - 1)
            {
                // 文が全て配置されていなければ
                if (charactersNumber < comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber].Length - 1)
                {
                    // 最初にYボタンを押した時に
                    if (!firstPushY && Input.GetButtonDown("select"))
                    {
                        // 文の完全配置ができる
                        firstPushY = true;
                        StartCoroutine("ResetFirstPushY");

                        charactersNumber = comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber].Length - 1;
                        comicsText.text = comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber];
                        return;
                    }

                    // 文字ごとに配置する
                    time += Time.deltaTime;
                    if (time >= feedTime)
                    {
                        time = 0.0f;

                        charactersNumber++;
                        displaySentence = displaySentence + comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber][charactersNumber];
                        comicsText.text = displaySentence;
                    }
                }
                // 文が全て配置されていれば
                else
                {
                    // 最初にYボタンを押した時に
                    if (!firstPushY && Input.GetButtonDown("select"))
                    {
                        firstPushY = true;
                        StartCoroutine("ResetFirstPushY");
                        // 文字に関する変数をリセットする
                        sentencesNumber++;
                        charactersNumber = 0;
                        // 次の文章を取得する
                        displaySentence = comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber][charactersNumber].ToString();
                        comicsText.text = displaySentence;
                    }
                }
            }
            // 1コマに対する文が全て配置されていれば
            else
            {
                // 文が全て配置されていなければ
                if (charactersNumber < comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber].Length - 1)
                {
                    // 最初にYボタンを押した時に
                    if (!firstPushY && Input.GetButtonDown("select"))
                    {
                        firstPushY = true;
                        StartCoroutine("ResetFirstPushY");
                        // 文の完全配置ができる
                        charactersNumber = comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber].Length - 1;
                        comicsText.text = comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber];
                        return;
                    }

                    // 文字ごとに配置する
                    time += Time.deltaTime;
                    if (time >= feedTime)
                    {
                        time = 0.0f;

                        charactersNumber++;
                        displaySentence = displaySentence + comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber][charactersNumber];
                        comicsText.text = displaySentence;
                    }
                }
                // 文が全て配置されていれば
                else
                {
                    // 最初にYボタンを押した時に
                    if (isSetSprite && !firstPushY && Input.GetButtonDown("select"))
                    {
                        firstPushY = true;
                        StartCoroutine("ResetFirstPushY");

                        comicsPanels[panelNumber].panel.SetActive(false);

                        panelNumber++;      // 次のパネルに移り
                        frameNumber = 0;    // フレームをリセットし
                        // だんだん画像を出現させる
                        isSetSprite = false;
                        if (comicsPanels[panelNumber].comicsFrames[frameNumber].isFadeIn)
                        {
                            currentAlpha = 0.0f;
                        }
                        else
                        {
                            currentAlpha = 1.0f;
                        }
                        sentencesNumber = 0;
                        charactersNumber = 0;

                        comicsPanels[panelNumber].panel.SetActive(true);

                        comicsPanels[panelNumber].comicsFrames[frameNumber].comicsImage.sprite = comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSprite;
                        comicsPanels[panelNumber].comicsFrames[frameNumber].comicsImage.color = new Color(comicsPanels[panelNumber].comicsFrames[frameNumber].comicsImage.color.r,
                                                                                                          comicsPanels[panelNumber].comicsFrames[frameNumber].comicsImage.color.g,
                                                                                                          comicsPanels[panelNumber].comicsFrames[frameNumber].comicsImage.color.b, currentAlpha);

                        displaySentence = comicsPanels[panelNumber].comicsFrames[frameNumber].comicsSentences[sentencesNumber][charactersNumber].ToString();
                        comicsText.text = displaySentence;
                    }
                }
            }
        }
    }

    // Yボタンの連続押し防止
    private IEnumerator ResetFirstPushY()
    {
        yield return new WaitForSeconds(0.1f);
        firstPushY = false;
    }
}

[System.Serializable]
public class ComicsPanels
{
    // パネルごとにページが切り替わる
    public GameObject panel;
    public ComicsFrames[] comicsFrames;
}

[System.Serializable]
public class ComicsFrames
{
    // trueならフェードイン、falseならフェードアウト
    public bool isFadeIn = true;
    public Image comicsImage;
    // 漫画の1コマ
    public Sprite comicsSprite;
    // 漫画の1コマに対するセリフ
    public string[] comicsSentences;
}