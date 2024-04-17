using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGenerator : MonoBehaviour
{
    [SerializeField]
    private Image[] images;
    [SerializeField, Header("0...オオカミ, 1...アリス, 2...御門, 3...ヘングレ")]
    private BuilderObstacles[] builderObstacles;

    private void Awake()
    {
        if (GameDirector.Instance != null)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].sprite = builderObstacles[GameDirector.Instance.builderIndex].obstacleSprites[i];
            }
        }
        else
        {
            Debug.Log("GameDirector is missing.");
            Destroy(this);
        }
    }
}

[System.Serializable]
public class BuilderObstacles
{
    public Sprite[] obstacleSprites;
}