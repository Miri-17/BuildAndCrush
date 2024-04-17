using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUIController : MonoBehaviour
{
    [Header("0...builder, 1...crusher"), SerializeField]
    private Image[] backgroundImages;
    [Header("0...win, 1...lose"), SerializeField]
    private Sprite[] backgroundSprites;

    [Header("0...builder, 1...crusher"), SerializeField]
    private Image[] nameBackImages;
    [Header("0...win, 1...lose"), SerializeField]
    private Sprite[] nameBackSprites;

    private void Start()
    {
        if (GameDirector.Instance != null)
        {
            if (GameDirector.Instance.crusherWin)
            {
                backgroundImages[0].sprite = backgroundSprites[1];
                backgroundImages[1].sprite = backgroundSprites[0];
                nameBackImages[0].sprite = nameBackSprites[1];
                nameBackImages[1].sprite = nameBackSprites[0];
            }
            else
            {
                backgroundImages[0].sprite = backgroundSprites[0];
                backgroundImages[1].sprite = backgroundSprites[1];
                nameBackImages[0].sprite = nameBackSprites[0];
                nameBackImages[1].sprite = nameBackSprites[1];
            }
        }
        else
        {
            Debug.Log("GameDirector is missing!");
            Destroy(this);
        }
    }
}
