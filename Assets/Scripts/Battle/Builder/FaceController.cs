using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceController : MonoBehaviour
{
    [SerializeField]
    private Image[] faceImages;
    [SerializeField]
    private Sprite[] faceSprites;

    private void Start()
    {
        if (GameDirector.Instance == null)
        {
            Destroy(this);
        }
        faceImages[0].sprite = faceSprites[0];
        faceImages[1].sprite = faceSprites[0];
    }

    private void Update()
    {
        if (faceImages[0].sprite != faceSprites[0] && faceImages[1].sprite != faceSprites[0]
            && GameDirector.Instance.currentFill <= 0.0f)
        {
            faceImages[0].sprite = faceSprites[0];
            faceImages[1].sprite = faceSprites[0];
        }
        
        if (faceImages[0].sprite != faceSprites[1] && faceImages[1].sprite != faceSprites[1]
            && GameDirector.Instance.currentFill >= 0.5f)
        {
            faceImages[0].sprite = faceSprites[1];
            faceImages[1].sprite = faceSprites[1];
        }
        
        if (faceImages[0].sprite != faceSprites[2] && faceImages[1].sprite != faceSprites[2]
            && GameDirector.Instance.currentFill >= 0.8f)
        {
            faceImages[0].sprite = faceSprites[2];
            faceImages[1].sprite = faceSprites[2];
        }
    }
}
