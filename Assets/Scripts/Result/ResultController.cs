using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audioClip;

    #region
    private AudioSource audioSource;
    private bool firstPushY = false;
    private bool isChangedCrusherAchievement = false;
    private bool isChangedBuilderAchievement = false;
    #endregion

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.PlayOneShot(audioClip[0]);
        UpdateAchievement();
    }

    private void Update()
    {
        if (!firstPushY && Input.GetButtonDown("select"))
        {
            firstPushY = true;

            if (GameDirector.Instance.crusherWin)
            {
                switch (GameDirector.Instance.crusherIndex)
                {
                    case 0:
                        if (GameDirector.Instance.builderIndex == 0)
                        {
                            Debug.Log("Go to EndingAE");
                            SceneManager.LoadScene("EndingAE");
                        }
                        else
                        {
                            Debug.Log("Go to Ending_Crossover_CrusherWin");
                            SceneManager.LoadScene("Ending_Crossover_CrusherWin");
                        }
                        break;
                    case 1:
                        if (GameDirector.Instance.builderIndex == 1)
                        {
                            Debug.Log("Go to EndingBF");
                            SceneManager.LoadScene("EndingBF");
                        }
                        else
                        {
                            Debug.Log("Go to Ending_Crossover_CrusherWin");
                            SceneManager.LoadScene("Ending_Crossover_CrusherWin");
                        }
                        break;
                    case 2:
                        if (GameDirector.Instance.builderIndex == 2)
                        {
                            Debug.Log("Go to EndingCG");
                            SceneManager.LoadScene("EndingCG");
                        }
                        else
                        {
                            Debug.Log("Go to Ending_Crossover_CrusherWin");
                            SceneManager.LoadScene("Ending_Crossover_CrusherWin");
                        }
                        break;
                    case 3:
                        if (GameDirector.Instance.builderIndex == 3)
                        {
                            Debug.Log("Go to EndingDH");
                            SceneManager.LoadScene("EndingDH");
                        }
                        else
                        {
                            Debug.Log("Go to Ending_Crossover_CrusherWin");
                            SceneManager.LoadScene("Ending_Crossover_CrusherWin");
                        }
                        break;
                    default:
                        Debug.Log("Go to EndingAE");
                        SceneManager.LoadScene("EndingAE");
                        break;
                }
            }
            else
            {
                switch (GameDirector.Instance.crusherIndex)
                {
                    case 0:
                        if (GameDirector.Instance.builderIndex == 0)
                        {
                            // Debug.Log("Go to EndingEA");
                            SceneManager.LoadScene("EndingEA");
                        }
                        else
                        {
                            // Debug.Log("Go to Ending_Crossover_BuilderWin");
                            SceneManager.LoadScene("Ending_Crossover_BuilderWin");
                        }
                        break;
                    case 1:
                        if (GameDirector.Instance.builderIndex == 1)
                        {
                            // Debug.Log("Go to EndingFB");
                            SceneManager.LoadScene("EndingFB");
                        }
                        else
                        {
                            // Debug.Log("Go to Ending_Crossover_BuilderWin");
                            SceneManager.LoadScene("Ending_Crossover_BuilderWin");
                        }
                        break;
                    case 2:
                        if (GameDirector.Instance.builderIndex == 2)
                        {
                            // Debug.Log("Go to EndingGC");
                            SceneManager.LoadScene("EndingGC");
                        }
                        else
                        {
                            // Debug.Log("Go to Ending_Crossover_BuilderWin");
                            SceneManager.LoadScene("Ending_Crossover_BuilderWin");
                        }
                        break;
                    case 3:
                        if (GameDirector.Instance.builderIndex == 3)
                        {
                            // Debug.Log("Go to EndingHD");
                            SceneManager.LoadScene("EndingHD");
                        }
                        else
                        {
                            // Debug.Log("Go to Ending_Crossover_BuilderWin");
                            SceneManager.LoadScene("Ending_Crossover_BuilderWin");
                        }
                        break;
                    default:
                        // Debug.Log("Go to EndingEA");
                        SceneManager.LoadScene("EndingEA");
                        break;
                }
            }
        }

        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioClip[1];
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    // Achievement情報をセーブするメソッド
    private void UpdateAchievement()
    {
        isChangedCrusherAchievement = false;
        isChangedBuilderAchievement = false;

        // もしクラッシャーが勝っていたら
        if (GameDirector.Instance.crusherWin)
        {
            switch (GameDirector.Instance.crusherIndex)
            {
                case 0:
                    // そのクラッシャーのachievements配列において、ビルダーに対するindexの要素が0だったら
                    if (GameDirector.Instance.girlAchievements[GameDirector.Instance.builderIndex] == 0)
                    {
                        isChangedCrusherAchievement = true;
                        // 1にする
                        GameDirector.Instance.girlAchievements[GameDirector.Instance.builderIndex] = 1;
                    }
                    break;
                case 1:
                    if (GameDirector.Instance.queenOfHeartsAchievements[GameDirector.Instance.builderIndex] == 0)
                    {
                        isChangedCrusherAchievement = true;
                        GameDirector.Instance.queenOfHeartsAchievements[GameDirector.Instance.builderIndex] = 1;
                    }
                    break;
                case 2:
                    if (GameDirector.Instance.tenjinAchievements[GameDirector.Instance.builderIndex] == 0)
                    {
                        isChangedCrusherAchievement = true;
                        GameDirector.Instance.tenjinAchievements[GameDirector.Instance.builderIndex] = 1;
                    }
                    break;
                case 3:
                    if (GameDirector.Instance.witchAchievements[GameDirector.Instance.builderIndex] == 0)
                    {
                        isChangedCrusherAchievement = true;
                        GameDirector.Instance.witchAchievements[GameDirector.Instance.builderIndex] = 1;
                    }
                    break;
                default:
                    break;
            }
        }
        // もしビルダーが勝っていたら
        if (GameDirector.Instance.builderWin)
        {
            switch (GameDirector.Instance.builderIndex)
            {
                case 0:
                    // そのビルダーのachievements配列において、クラッシャーに対するindexの要素が0だったら
                    if (GameDirector.Instance.wolfAchievements[GameDirector.Instance.crusherIndex] == 0)
                    {
                        isChangedBuilderAchievement = true;
                        // 1にする
                        GameDirector.Instance.wolfAchievements[GameDirector.Instance.crusherIndex] = 1;
                    }
                    break;
                case 1:
                    if (GameDirector.Instance.queenAliceAchievements[GameDirector.Instance.crusherIndex] == 0)
                    {
                        isChangedBuilderAchievement = true;
                        GameDirector.Instance.queenAliceAchievements[GameDirector.Instance.crusherIndex] = 1;
                    }
                    break;
                case 2:
                    if (GameDirector.Instance.mikadoAchievements[GameDirector.Instance.crusherIndex] == 0)
                    {
                        isChangedBuilderAchievement = true;
                        GameDirector.Instance.mikadoAchievements[GameDirector.Instance.crusherIndex] = 1;
                    }
                    break;
                case 3:
                    if (GameDirector.Instance.hanzelGretelAchievements[GameDirector.Instance.crusherIndex] == 0)
                    {
                        isChangedBuilderAchievement = true;
                        GameDirector.Instance.hanzelGretelAchievements[GameDirector.Instance.crusherIndex] = 1;
                    }
                    break;
                default:
                    break;
            }
        }

        // クラッシャー情報の変更があったら
        if (isChangedCrusherAchievement)
        {
            switch (GameDirector.Instance.crusherIndex)
            {
                case 0:
                    for (int i = 0; i < GameDirector.Instance.girlAchievements.Length; i++)
                    {
                        PlayerPrefs.SetInt("girlAchievement" + i + "Data", GameDirector.Instance.girlAchievements[i]);
                    }
                    break;
                case 1:
                    for (int i = 0; i < GameDirector.Instance.queenOfHeartsAchievements.Length; i++)
                    {
                        PlayerPrefs.SetInt("queenOfHeartsAchievement" + i + "Data", GameDirector.Instance.queenOfHeartsAchievements[i]);
                    }
                    break;
                case 2:
                    for (int i = 0; i < GameDirector.Instance.tenjinAchievements.Length; i++)
                    {
                        PlayerPrefs.SetInt("tenjinAchievement" + i + "Data", GameDirector.Instance.tenjinAchievements[i]);
                    }
                    break;
                case 3:
                    for (int i = 0; i < GameDirector.Instance.witchAchievements.Length; i++)
                    {
                        PlayerPrefs.SetInt("witchAchievement" + i + "Data", GameDirector.Instance.witchAchievements[i]);
                    }
                    break;
                default:
                    break;
            }
            // Achievement情報をセーブする.
            PlayerPrefs.Save();
        }

        // ビルダーの情報の変更があったら
        if (isChangedBuilderAchievement)
        {
            switch (GameDirector.Instance.builderIndex)
            {
                case 0:
                    for (int i = 0; i < GameDirector.Instance.wolfAchievements.Length; i++)
                    {
                        PlayerPrefs.SetInt("wolfAchievement" + i + "Data", GameDirector.Instance.wolfAchievements[i]);
                    }
                    break;
                case 1:
                    for (int i = 0; i < GameDirector.Instance.queenAliceAchievements.Length; i++)
                    {
                        PlayerPrefs.SetInt("queenAliceAchievement" + i + "Data", GameDirector.Instance.queenAliceAchievements[i]);
                    }
                    break;
                case 2:
                    for (int i = 0; i < GameDirector.Instance.mikadoAchievements.Length; i++)
                    {
                        PlayerPrefs.SetInt("mikadoAchievement" + i + "Data", GameDirector.Instance.mikadoAchievements[i]);
                    }
                    break;
                case 3:
                    for (int i = 0; i < GameDirector.Instance.hanzelGretelAchievements.Length; i++)
                    {
                        PlayerPrefs.SetInt("hanzelGretelAchievement" + i + "Data", GameDirector.Instance.hanzelGretelAchievements[i]);
                    }
                    break;
                default:
                    break;
            }
            // Achievement情報をセーブする.
            PlayerPrefs.Save();
        }
    }
}
