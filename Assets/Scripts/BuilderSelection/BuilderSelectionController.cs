using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuilderSelectionController : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private Image builderStatus;
    [SerializeField]
    private AudioSource audioSource = null;
    [SerializeField]
    private AudioClip[] sounds = new AudioClip[2];

    private ShowStatus showStatusScript;
    private bool firstPushY = false;
    private bool firstPushB = false;
    // 次のシーンに行く処理になったらパネルを表示させないようにするための変数.
    private bool goNextScene = false;

    private void Start()
    {
        if (GameDirector.Instance == null)
        {
            Debug.Log("GameDirector is missing!");
            Destroy(this);
        }
        showStatusScript = builderStatus.GetComponent<ShowStatus>();
        panel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // パネル非表示状態で初めてYボタンが押されたら
        if (!goNextScene && !panel.activeSelf && !firstPushY && Input.GetButtonDown("select"))
        {
            firstPushY = true;
            panel.SetActive(true);
            showStatusScript.enabled = false;
        }
        // パネルが表示状態で、初めてYボタンが押されたら
        else if (panel.activeSelf && !firstPushY && Input.GetButtonDown("select"))
        {
            firstPushY = true;
            audioSource.PlayOneShot(sounds[0]);
            panel.SetActive(false);
            goNextScene = true;

            GameDirector.Instance.crusherScore = 0;
            GameDirector.Instance.builderScore = 0;
            GameDirector.Instance.wagonCrushCounts = 0;
            GameDirector.Instance.crusherKillCounts = 0;
            GameDirector.Instance.timeLimit = 600.0f;
            GameDirector.Instance.currentFill = 0.0f;

            // 次のシーンへ遷移する
            switch (GameDirector.Instance.crusherIndex)
            {
                case 0:
                    if (GameDirector.Instance.builderIndex == 0)
                    {
                        Debug.Log("Go to OpeningAE");
                        StartCoroutine(GoNextScene("OpeningAE"));
                    }
                    else
                    {
                        Debug.Log("Go to Opening_Crossover");
                        StartCoroutine(GoNextScene("Opening_Crossover"));
                    }
                    break;
                case 1:
                    if (GameDirector.Instance.builderIndex == 1)
                    {
                        Debug.Log("Go to OpeningBF");
                        StartCoroutine(GoNextScene("OpeningBF"));
                    }
                    else
                    {
                        Debug.Log("Go to Opening_Crossover");
                        StartCoroutine(GoNextScene("Opening_Crossover"));
                    }
                    break;
                case 2:
                    if (GameDirector.Instance.builderIndex == 2)
                    {
                        Debug.Log("Go to OpeningCG");
                        StartCoroutine(GoNextScene("OpeningCG"));
                    }
                    else
                    {
                        Debug.Log("Go to Opening_Crossover");
                        StartCoroutine(GoNextScene("Opening_Crossover"));
                    }
                    break;
                case 3:
                    if (GameDirector.Instance.builderIndex == 3)
                    {
                        Debug.Log("Go to OpeningDH");
                        StartCoroutine(GoNextScene("OpeningDH"));
                    }
                    else
                    {
                        Debug.Log("Go to Opening_Crossover");
                        StartCoroutine(GoNextScene("Opening_Crossover"));
                    }
                    break;
                default:
                    Debug.Log("Go to OpeningAE");
                    StartCoroutine(GoNextScene("OpeningAE"));
                    break;
            }
        }
        // パネルが表示されていない状態で戻るボタンが押されたらクラッシャー選択画面に戻る.
        else if (!panel.activeSelf && !firstPushB && Input.GetButtonDown("Jump"))
        {
            firstPushB = true;
            audioSource.PlayOneShot(sounds[1]);

            GameDirector.Instance.crusherIndex = 0;
            Debug.Log("Go back to CrusherSelection");
            SceneManager.LoadScene("CrusherSelection");
        }
        // パネルが表示されている状態で戻るボタンが押されたらパネルを閉じる.
        else if (panel.activeSelf && !firstPushB && Input.GetButtonDown("Jump"))
        {
            firstPushB = true;
            panel.SetActive(false);
            showStatusScript.enabled = true;
        }
        else
        {
            firstPushY = false;
            firstPushB = false;
        }
    }

    private IEnumerator GoNextScene(string scene)
    {
        yield return new WaitForSeconds(1.5f);

        // Debug.Log("Go to Opening");
        SceneManager.LoadScene(scene);
    }
}
