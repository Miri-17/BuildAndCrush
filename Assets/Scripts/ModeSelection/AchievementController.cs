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

    private void Start()
    {
        // Achievement情報をロードする.
        for (int i = 0; i < GameDirector.Instance.achievements.Length; i++)
        {
            GameDirector.Instance.achievements[i] = PlayerPrefs.GetInt("achievement" + i + "_data");
        }

        // achievemetnImageを表示するか表示しないか決める.
        for (int i = 0; i < 4; i++)
        {
            if (GameDirector.Instance.achievements[i] == 0)
            {
                crusherAchievementImages[i].enabled = false;
            }
            else
            {
                crusherAchievementImages[i].enabled = true;
            }
        }
        for (int i = 4; i < GameDirector.Instance.achievements.Length; i++)
        {
            if (GameDirector.Instance.achievements[i] == 0)
            {
                builderAchievementImages[i % 4].enabled = false;
            }
            else
            {
                builderAchievementImages[i % 4].enabled = true;
            }
        }
    }
}
