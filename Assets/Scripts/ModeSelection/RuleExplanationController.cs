using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RuleExplanationController : MonoBehaviour
{
    public Explanations explanations;

    #region
    [SerializeField]
    private ModeSelectionController modeSelectionController;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private Text explanationText0;
    [SerializeField]
    private Text explanationText1;
    [SerializeField]
    private Text explanationText2;
    [SerializeField]
    private Button ruleExplanationButton;
    [SerializeField]
    private Button crusherExplanationButton;
    [SerializeField]
    private Button builderExplanationButton;
    [SerializeField]
    private Image explanationImage;
    [SerializeField]
    private AudioSource SEaudioSource;
    [SerializeField]
    private AudioClip SEaudioClip;
    #endregion

    #region
    // 0...ルール説明 / 1...クラッシャー説明 / 2...ビルダー説明
    private int explanationTitlesIndex = 0;
    private int explanationsIndex = 0;
    private float horizontalKey = 0;
    #endregion

    [SerializeField]
    private Text explanationTitleText;
    private string[] explanationTitle = new string[3]{ "ルール説明", "クラッシャーについて", "ビルダーについて",};

    private void Start()
    {
        ruleExplanationButton.interactable = false;
        explanationTitlesIndex = 0;
        explanationsIndex = 0;
        UpdateExplanations(explanationTitlesIndex, explanationsIndex);
    }

    private void Update()
    {
        if (panel.activeSelf)
        {
            // 1フレームのうち horizontal key が押されていない時があったら
            if (modeSelectionController.firstPushHorizontal)
            {
                // horizontal key を押せるようにする
                modeSelectionController.firstPushHorizontal = false;
            }
            if (!modeSelectionController.firstPushHorizontal && Input.GetButtonDown("Horizontal"))
            {
                modeSelectionController.firstPushHorizontal = true;
                horizontalKey = Input.GetAxisRaw("Horizontal");

                if (horizontalKey > 0)
                {
                    explanationsIndex++;

                    if (explanationTitlesIndex == 0 && explanationsIndex >= explanations.ruleExplanation.ruleExplanationSentences0.Length)
                    {
                        explanationTitlesIndex = 1;
                        explanationsIndex = 0;
                        ruleExplanationButton.interactable = true;
                        crusherExplanationButton.interactable = false;
                    }
                    else if (explanationTitlesIndex == 1 && explanationsIndex >= explanations.crusherExplanation.crusherExplanationSentences0.Length)
                    {
                        explanationTitlesIndex = 2;
                        explanationsIndex = 0;
                        crusherExplanationButton.interactable = true;
                        builderExplanationButton.interactable = false;
                    }
                    else if (explanationTitlesIndex == 2 && explanationsIndex >= explanations.builderExplanation.builderExplanationSentences0.Length)
                    {
                        explanationTitlesIndex = 0;
                        explanationsIndex = 0;
                        builderExplanationButton.interactable = true;
                        ruleExplanationButton.interactable = false;
                    }
                }
                else if (horizontalKey < 0)
                {
                    explanationsIndex--;

                    if (explanationsIndex < 0)
                    {
                        if (explanationTitlesIndex == 0)
                        {
                            explanationTitlesIndex = 2;
                            explanationsIndex = explanations.ruleExplanation.ruleExplanationSentences0.Length - 1;
                            ruleExplanationButton.interactable = true;
                            builderExplanationButton.interactable = false;
                        }
                        else if (explanationTitlesIndex == 1)
                        {
                            explanationTitlesIndex = 0;
                            explanationsIndex = explanations.crusherExplanation.crusherExplanationSentences0.Length - 1;
                            crusherExplanationButton.interactable = true;
                            ruleExplanationButton.interactable = false;
                        }
                        else if (explanationTitlesIndex == 2)
                        {
                            explanationTitlesIndex = 1;
                            explanationsIndex = explanations.builderExplanation.builderExplanationSentences0.Length - 1;
                            builderExplanationButton.interactable = true;
                            crusherExplanationButton.interactable = false;
                        }
                    }
                }

                SEaudioSource.PlayOneShot(SEaudioClip);
                UpdateExplanations(explanationTitlesIndex, explanationsIndex);
            }
        }
    }

    private void UpdateExplanations(int explanationTitlesIndex, int explanationsIndex)
    {
        // Debug.Log("explanationsIndex: " + explanationTitlesIndex + ", sentencesIndex: " + explanationsIndex);
        if (explanationTitlesIndex == 0)
        {
            explanationText0.text = explanations.ruleExplanation.ruleExplanationSentences0[explanationsIndex];
            explanationText1.text = explanations.ruleExplanation.ruleExplanationSentences1[explanationsIndex];
            explanationText2.text = explanations.ruleExplanation.ruleExplanationSentences2[explanationsIndex]; 
            explanationImage.sprite = explanations.ruleExplanation.ruleExplanationSprites[explanationsIndex];
        }
        else if (explanationTitlesIndex == 1)
        {
            explanationText0.text = explanations.crusherExplanation.crusherExplanationSentences0[explanationsIndex];
            explanationText1.text = explanations.crusherExplanation.crusherExplanationSentences1[explanationsIndex];
            explanationText2.text = explanations.crusherExplanation.crusherExplanationSentences2[explanationsIndex];
            explanationImage.sprite = explanations.crusherExplanation.crusherExplanationSprites[explanationsIndex];
        }
        else if (explanationTitlesIndex == 2)
        {
            explanationText0.text = explanations.builderExplanation.builderExplanationSentences0[explanationsIndex];
            explanationText1.text = explanations.builderExplanation.builderExplanationSentences1[explanationsIndex];
            explanationText2.text = explanations.builderExplanation.builderExplanationSentences2[explanationsIndex];
            explanationImage.sprite = explanations.builderExplanation.builderExplanationSprites[explanationsIndex];  
        }
        explanationTitleText.text = explanationTitle[explanationTitlesIndex];       
    }

    public void PushRuleExplanationButton()
    {
        ruleExplanationButton.interactable = false;
        crusherExplanationButton.interactable = true;
        builderExplanationButton.interactable = true;
        explanationTitlesIndex = 0;
        explanationsIndex = 0;
        UpdateExplanations(explanationTitlesIndex, explanationsIndex);
    }

    public void PushCrusherButton()
    {
        crusherExplanationButton.interactable = false;
        ruleExplanationButton.interactable = true;
        builderExplanationButton.interactable = true;
        explanationTitlesIndex = 1;
        explanationsIndex = 0;
        UpdateExplanations(explanationTitlesIndex, explanationsIndex);
    }

    public void PushBuilderButton()
    {
        builderExplanationButton.interactable = false;
        ruleExplanationButton.interactable = true;
        crusherExplanationButton.interactable = true;
        explanationTitlesIndex = 2;
        explanationsIndex = 0;
        UpdateExplanations(explanationTitlesIndex, explanationsIndex);
    }
}

[System.Serializable]
public class Explanations
{
    public RuleExplanation ruleExplanation;
    public CrusherExplanation crusherExplanation;
    public BuilderExplanation builderExplanation;
}

[System.Serializable]
public class RuleExplanation
{
    public string[] ruleExplanationSentences0;
    public string[] ruleExplanationSentences1;
    public string[] ruleExplanationSentences2;
    public Sprite[] ruleExplanationSprites;
}

[System.Serializable]
public class CrusherExplanation
{
    public string[] crusherExplanationSentences0;
    public string[] crusherExplanationSentences1;
    public string[] crusherExplanationSentences2;
    public Sprite[] crusherExplanationSprites;
}

[System.Serializable]
public class BuilderExplanation
{
    public string[] builderExplanationSentences0;
    public string[] builderExplanationSentences1;
    public string[] builderExplanationSentences2;
    public Sprite[] builderExplanationSprites;
}

