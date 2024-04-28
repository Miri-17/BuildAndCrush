using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementController : MonoBehaviour
{
    [SerializeField]
    private Image[] crusherAchievementImages;
    [SerializeField]
    private Image[] builderAchievementImages;
    [SerializeField]
    private GameObject warningPanel;
    [SerializeField]
    private AudioSource SEaudioSource;
    [SerializeField]
    private AudioClip SEaudioClip;

    private void Awake()
    {
        LoadPlayerPrefs();
    }

    private void Start()
    {
        warningPanel.SetActive(false);
        UpdateAchievementImage();
    }

    // Achievement情報をロードする.
    private void LoadPlayerPrefs()
    {
        for (int i = 0; i < GameDirector.Instance.girlAchievements.Length; i++)
        {
            GameDirector.Instance.girlAchievements[i] = PlayerPrefs.GetInt("girlAchievement" + i + "Data", 0);
            GameDirector.Instance.queenOfHeartsAchievements[i] = PlayerPrefs.GetInt("queenOfHeartsAchievement" + i + "Data", 0);
            GameDirector.Instance.tenjinAchievements[i] = PlayerPrefs.GetInt("tenjinAchievement" + i + "Data", 0);
            GameDirector.Instance.witchAchievements[i] = PlayerPrefs.GetInt("witchAchievement" + i + "Data", 0);
            GameDirector.Instance.wolfAchievements[i] = PlayerPrefs.GetInt("wolfAchievement" + i + "Data", 0);
            GameDirector.Instance.queenAliceAchievements[i] = PlayerPrefs.GetInt("queenAliceAchievement" + i + "Data", 0);
            GameDirector.Instance.mikadoAchievements[i] = PlayerPrefs.GetInt("mikadoAchievement" + i + "Data", 0);
            GameDirector.Instance.hanzelGretelAchievements[i] = PlayerPrefs.GetInt("hanzelGretelAchievement" + i + "Data", 0);
        }
    }

    private void UpdateAchievementImage()
    {
        // crusherAchievemetImageを表示するか表示しないか決める.
        if (GameDirector.Instance.girlAchievements.All(value => value != 0))
        {
            crusherAchievementImages[0].enabled = true;
        }
        else
        {
            crusherAchievementImages[0].enabled = false;
        }
        if (GameDirector.Instance.queenOfHeartsAchievements.All(value => value != 0))
        {
            crusherAchievementImages[1].enabled = true;
        }
        else
        {
            crusherAchievementImages[1].enabled = false;
        }
        if (GameDirector.Instance.tenjinAchievements.All(value => value != 0))
        {
            crusherAchievementImages[2].enabled = true;
        }
        else
        {
            crusherAchievementImages[2].enabled = false;
        }
        if (GameDirector.Instance.witchAchievements.All(value => value != 0))
        {
            crusherAchievementImages[3].enabled = true;
        }
        else
        {
            crusherAchievementImages[3].enabled = false;
        }

        // builderAchievemetImageを表示するか表示しないか決める.
        if (GameDirector.Instance.wolfAchievements.All(value => value != 0))
        {
            builderAchievementImages[0].enabled = true;
        }
        else
        {
            builderAchievementImages[0].enabled = false;
        }
        if (GameDirector.Instance.queenAliceAchievements.All(value => value != 0))
        {
            builderAchievementImages[1].enabled = true;
        }
        else
        {
            builderAchievementImages[1].enabled = false;
        }
        if (GameDirector.Instance.mikadoAchievements.All(value => value != 0))
        {
            builderAchievementImages[2].enabled = true;
        }
        else
        {
            builderAchievementImages[2].enabled = false;
        }
        if (GameDirector.Instance.hanzelGretelAchievements.All(value => value != 0))
        {
            builderAchievementImages[3].enabled = true;
        }
        else
        {
            builderAchievementImages[3].enabled = false;
        }
    }
    
    public void PushResetButton()
    {
        warningPanel.SetActive(true);
    }

    public void PushResetYesButton()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        LoadPlayerPrefs();
        UpdateAchievementImage();
        SEaudioSource.PlayOneShot(SEaudioClip);
        warningPanel.SetActive(false);
    }

    public void PushResetNoButton()
    {
        warningPanel.SetActive(false);
    }
}
