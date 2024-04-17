using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningImageGenerator : MonoBehaviour
{
    #region
    [SerializeField]
    private Image[] image;
    [SerializeField]
    private OpeningImageNumbers[] imageNumbers;
    #endregion

    private int index0;
    private int index1;

    private void Start()
    {
        if (GameDirector.Instance != null)
        {
            index0 = GameDirector.Instance.crusherIndex;
            index1 = (GameDirector.Instance.crusherIndex < GameDirector.Instance.builderIndex) ? GameDirector.Instance.builderIndex - 1 : GameDirector.Instance.builderIndex;
            Debug.Log(index0);
            Debug.Log(index1);
            image[0].sprite = imageNumbers[0].sprites[index0].sprite[index1];
            image[1].sprite = imageNumbers[1].sprites[index0].sprite[index1];
        }
        else
        {
            Debug.Log("GameDirector is missing");
            Destroy(this);
        }
    }
}

[System.Serializable]
public class OpeningImageNumbers
{
    public OpeningSprites[] sprites;
}

[System.Serializable]
public class OpeningSprites
{
    // 漫画の1コマ
    public Sprite[] sprite;
}
