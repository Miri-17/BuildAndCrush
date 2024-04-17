using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuilderResultController : MonoBehaviour
{
    #region
    [SerializeField]
    private BuilderDatabase builderDB;
    [SerializeField]
    private Text builderNameText;
    [SerializeField]
    private Text crusherFinalScoreText = null;
    [SerializeField]
    private Text builderFinalScoreText = null;
    [SerializeField]
    private Text wagonCrushCountsText = null;
    [SerializeField]
    private Text crusherKillCountsText = null;
    [SerializeField]
    private Image crusherFillImage = null;
    #endregion

    private void Start()
    {
        if (GameDirector.Instance != null)
        {
            UpdateCrusher(GameDirector.Instance.builderIndex);
            crusherFinalScoreText.text = GameDirector.Instance.crusherScore.ToString();
            builderFinalScoreText.text = GameDirector.Instance.builderScore.ToString();
            wagonCrushCountsText.text = GameDirector.Instance.wagonCrushCounts.ToString();
            crusherKillCountsText.text = GameDirector.Instance.crusherKillCounts.ToString();
            if (GameDirector.Instance.crusherScore == 0 && GameDirector.Instance.builderScore == 0)
            {
                crusherFillImage.fillAmount = 0.5f;
            }
            else
            {
                crusherFillImage.fillAmount = (float)GameDirector.Instance.crusherScore / (float)(GameDirector.Instance.crusherScore + GameDirector.Instance.builderScore);
                Debug.Log((float)GameDirector.Instance.crusherScore / (float)(GameDirector.Instance.crusherScore + GameDirector.Instance.builderScore));
            }
        }
        else
        {
            Debug.Log("GameDirector is missing!");
            Destroy(this);
        }
    }

    // クラッシャーの情報をアップデートする関数.
    private void UpdateCrusher(int selectedOption)
    {
        Builder builder = builderDB.GetBuilder(selectedOption);
        // 名前の変更.
        builderNameText.text = builder.builderName;
        // Live2Dの変更.
        Instantiate(builder.builderL2D, new Vector3(3.8f, 0.0f, 0.0f), Quaternion.identity);
    }
}
