using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuilderManager : MonoBehaviour
{
    [SerializeField]
    private BuilderDatabase builderDB;

    // public Text builderNameText;
    [SerializeField]
    private Text builderNameText;
    // public SpriteRenderer builderArtworkSprite;

    //private int selectedOption = 0;
    #region 
    [Header("クラッシャー変更時に鳴らす音")]
    [SerializeField]
    private AudioSource audioSource = null;
    [SerializeField]
    private AudioClip sound;
    [Header("ビルダーの通り名"), SerializeField]
    private Text builderNicknameText;
    [Header("ビルダーの説明"), SerializeField]
    private Text builderDescriptionText;
    [Header("選択しているビルダーの画像")]
    [SerializeField]
    private RectTransform selection;
    [SerializeField]
    private float[] selectionLocation;
    #endregion

    #region
    private string[] builderNicknames =
    {
        "利己主義の詐欺師",
        "歪な統治者",
        "陽光の君",
        "破天荒な迷子",
    };
    private string[] builderDescription =
    {
        "あちこちで詐欺を働いている狼グループの頭領。\n\n"
        + "詐欺行為は世のため人のためだと謳ってはいるが、その実情は仲間すら信用しない徹底的な利己主義である。\n",

        "不思議の国を統べる女王。\n終わらない己の夢に閉じ込められ、現実との境が曖昧になっている。\n\n"
        + "大人らしく冷静に振る舞うが、子供のように癇癪を起こす一面も。\n国は彼女の内面のように荒れている。\n",

        "国をすべる活発な主君。\n明るく堂々としており文武両道。\n決して弱い姿を見せずに民を導いている。\n\n"
        + "御門という立場に重圧を感じていたところでかぐや姫に出会い、身分で人を判断しない素直なところに一目惚れした。\n\n"
        + "かぐや姫とは仲良くなってきたものの、想いを伝えられずにいる。\n",

        "工業都市にある貧民街の子供。\n生活が貧しく親から家を追い出されてしまった。\n\n"
        + "兄のヘンゼルは穏やかで慎重派な一方、妹のグレーテルは元気ないたずらっ子で、ヘンゼルにはいつも手を焼かされている。\n\n"
        + "偶然魔女の家を発見し、魔女のいない間に装置を動かして家を乗っ取ってしまった。\n"
        + "魔女の装置を使って何やら計画しているようだ。\n",
    };
    #endregion

    private void Start()
    {
        UpdateBuilder(GameDirector.Instance.builderIndex);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            audioSource.PlayOneShot(sound);

            if (horizontalInput > 0)
            {
                GameDirector.Instance.builderIndex++;

                if (GameDirector.Instance.builderIndex >= builderDB.BuilderCount)
                {
                    GameDirector.Instance.builderIndex = 0;
                }
            }
            else if (horizontalInput < 0)
            {
                GameDirector.Instance.builderIndex--;

                if (GameDirector.Instance.builderIndex < 0)
                {
                    GameDirector.Instance.builderIndex = builderDB.BuilderCount - 1;
                }
            }
            UpdateBuilder(GameDirector.Instance.builderIndex);
        }
    }

    // ビルダーの情報をアップデートする関数.
    private void UpdateBuilder(int selectedOption)
    {
        Builder builder = builderDB.GetBuilder(selectedOption);
        // 名前の変更.
        builderNameText.text = builder.builderName;
        // Live2Dの変更
        Instantiate(builder.builderL2D, new Vector3(3.8f, 0.0f, 0.0f), Quaternion.identity);
        // ビルダーの通り名の変更.
        builderNicknameText.text = builderNicknames[selectedOption];
        // ビルダーの説明の変更
        builderDescriptionText.text = builderDescription[selectedOption];
        if (selectedOption == 2)
        {
            builderNameText.fontSize = 150;
            builderDescriptionText.fontSize = 42;
        }
        else if (selectedOption == 3)
        {
            builderNameText.fontSize = 88;
            builderDescriptionText.fontSize = 40;
        }
        else
        {
            builderNameText.fontSize = 150;
            builderDescriptionText.fontSize = 50;
        }
        // 選択しているビルダーの周りで光っている画像の位置の変更
        selection.anchoredPosition = new Vector2(selectionLocation[GameDirector.Instance.builderIndex], -565);
    }
}
