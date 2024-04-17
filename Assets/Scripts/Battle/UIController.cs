using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region
    [SerializeField, Header("制限時間のテキスト")]
    private Text[] timeText;
    [SerializeField, Header("クラッシャースコアのテキスト")]
    private Text[] crusherScoreText;
    [SerializeField, Header("ビルダースコアのテキスト")]
    private Text[] builderScoreText;

    // 追加
    [SerializeField, Header("クラッシャーのアイコン")]
    private RectTransform[] crusherIcon;
    [SerializeField, Header("ビルダーのアイコン")]
    private RectTransform[] builderIcon;
    #endregion

    private int crusherOldScore = 0;
    private int builderOldScore = 0;

    // 追加
    // これはBuilderの生成位置を変えた場合に可変性がないので注意してください.
    private float endPos = 5270.0f;

    private RectTransform[] crusherIconRT = new RectTransform[2];
    private RectTransform[] builderIconRT = new RectTransform[2];

    private void Start()
    {
        if (GameDirector.Instance == null)
        {
            Debug.Log("GameDirector is missing!");
            Destroy(this);
        }

        crusherScoreText[0].text = GameDirector.Instance.crusherScore.ToString();
        crusherScoreText[1].text = GameDirector.Instance.crusherScore.ToString();
        builderScoreText[0].text = GameDirector.Instance.builderScore.ToString();
        builderScoreText[1].text = GameDirector.Instance.builderScore.ToString();
        
        crusherIconRT[0] = crusherIcon[0].GetComponent<RectTransform>();
        crusherIconRT[1] = crusherIcon[1].GetComponent<RectTransform>();
        builderIconRT[0] = builderIcon[0].GetComponent<RectTransform>();
        builderIconRT[1] = builderIcon[1].GetComponent<RectTransform>();
        crusherIconRT[0].DOAnchorPos(new Vector3(-475, 0, 0), 0.0f).SetLink(gameObject);
        crusherIconRT[1].DOAnchorPos(new Vector3(-490, -60, 0), 0.0f).SetLink(gameObject);
        builderIconRT[0].DOAnchorPos(new Vector3(490, 0, 0), 0.0f).SetLink(gameObject);
        builderIconRT[1].DOAnchorPos(new Vector3(475, -60, 0), 0.0f).SetLink(gameObject);
    }

    private void Update()
    {
        GameDirector.Instance.timeLimit -= Time.deltaTime;
        timeText[0].text = GameDirector.Instance.timeLimit.ToString("f");
        timeText[1].text = GameDirector.Instance.timeLimit.ToString("f");
        if (GameDirector.Instance.timeLimit <= 0)
        {
            timeText[0].text = "Time Up!";
            timeText[1].text = "Time Up!";
            timeText[1].fontSize = 60;
        }

        crusherOldScore = (int)(GameDirector.Instance.crusherPosition * 999 / endPos);
        if (crusherOldScore < 1000 && crusherOldScore > GameDirector.Instance.crusherScore)
        {
            GameDirector.Instance.crusherScore = crusherOldScore;
            crusherScoreText[0].text = GameDirector.Instance.crusherScore.ToString();
            crusherScoreText[1].text = GameDirector.Instance.crusherScore.ToString();
        }

        builderOldScore = (int)((endPos - GameDirector.Instance.builderPosition) * 999 / endPos);
        if (builderOldScore >= 0 && builderOldScore < 1000 && builderOldScore > GameDirector.Instance.builderScore)
        {
            GameDirector.Instance.builderScore = builderOldScore;
            builderScoreText[0].text = GameDirector.Instance.builderScore.ToString();
            builderScoreText[1].text = GameDirector.Instance.builderScore.ToString();
        }

        crusherIcon[0].anchoredPosition = new Vector3(-475 + GameDirector.Instance.crusherPosition  * 961.53f / endPos, 0, 0);
        crusherIcon[1].anchoredPosition = new Vector3(-475 + GameDirector.Instance.crusherPosition * 958f / endPos, -60, 0);
        builderIcon[0].anchoredPosition = new Vector3(-560 + GameDirector.Instance.builderPosition * 961.53f / endPos, 0, 0);
        builderIcon[1].anchoredPosition = new Vector3(-560 + GameDirector.Instance.builderPosition * 958f / endPos, -60, 0);
    }
}
