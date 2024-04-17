using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrusherManager : MonoBehaviour
{
    [SerializeField]
    private CrusherDatabase crusherDB;

    // public Text crusherNameText;
    [SerializeField]
    private Text crusherNameText;

    #region
    [Header("クラッシャー変更時に鳴らす音")]
    [SerializeField]
    private AudioSource audioSource = null;
    [SerializeField]
    private AudioClip sound;
    [Header("クラッシャーの通り名"), SerializeField]
    private Text crusherNicknameText;
    [Header("クラッシャーの説明"), SerializeField]
    private Text crusherDescriptionText;
    [Header("選択しているクラッシャーの画像")]
    [SerializeField]
    private RectTransform selection;
    [SerializeField]
    private float[] selectionLocation;
    #endregion

    #region
    private string[] crusherNicknames =
    {
        "鮮血の狩人",
        "激烈な叛逆者",
        "月影の守人",
        "慈愛の発明家",
    };
    private string[] crusherDescription =
    {
        "世にはびこる悪を撃ち続け、特徴的な赤いマントを翻す姿から、「鮮血の赤ずきん」という通り名をもつ普通の少女。\n\n"
        + "祖母を手にかけた狼という種族に対して強い恨みを抱いている。\n",

        "不思議の国で唯一生き残ったハートのトランプ。\n\n"
        + "非常に気性が荒いものの、アリスの心に呼応し歪み始めた世界をあるべき姿に戻すべく、彼女から女王の座を奪還するために帽子屋たちと手を組んだ。\n",

        "サイバーパンクな月からやってきた機械人間。\n高度な文明が発展した月で、幼い頃からかぐや姫の護衛をしていた。\n\n"
        + "感情が統制され、冷徹であるが、かぐや姫の流刑をきっかけに自身の中の恋愛感情に気づく。\n\n"
        + "かぐや姫を取り返しに地球へ降り立つ天人。それは月のためなのか個人の感情なのか。\n",

        "お菓子を生み出す魔法使い。\n\n自分の魔法や道具で多くの人を幸せにしたいと願い、現在は技術者として魔法がなくてもお菓子を生み出せる機械を作っている。\n\n"
        + "とても優しく献身的だが、おっちょこちょいな部分があり、自分の住んでいた機械仕掛けのお菓子工場をヘンゼルとグレーテルに奪われてしまう...。\n",
    };
    #endregion

    private void Start()
    {
        UpdateCrusher(GameDirector.Instance.crusherIndex);
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
                GameDirector.Instance.crusherIndex++;

                if (GameDirector.Instance.crusherIndex >= crusherDB.CrusherCount)
                {
                    GameDirector.Instance.crusherIndex = 0;
                }
            }
            else if (horizontalInput < 0)
            {
                GameDirector.Instance.crusherIndex--;

                if (GameDirector.Instance.crusherIndex < 0)
                {
                    GameDirector.Instance.crusherIndex = crusherDB.CrusherCount - 1;
                }
            }

            UpdateCrusher(GameDirector.Instance.crusherIndex);
        }
    }

    // クラッシャーの情報をアップデートする関数.
    private void UpdateCrusher(int selectedOption)
    {
        Crusher crusher = crusherDB.GetCrusher(selectedOption);
        // 名前の変更.
        crusherNameText.text = crusher.crusherName;
        // Live2Dの変更.
        Instantiate(crusher.crusherL2D, new Vector3(-3.8f, 0.0f, 0.0f), Quaternion.identity);
        // クラッシャーの通り名の変更.
        crusherNicknameText.text = crusherNicknames[selectedOption];
        // クラッシャーの説明の変更
        crusherDescriptionText.text = crusherDescription[selectedOption];
        if (selectedOption == 2 || selectedOption == 3)
        {
            crusherDescriptionText.fontSize = 42;
        }
        else
        {
            crusherDescriptionText.fontSize = 50;
        }
        // 選択しているクラッシャーの周りで光っている画像の位置の変更
        selection.anchoredPosition = new Vector2(selectionLocation[selectedOption], -565);
    }
}
