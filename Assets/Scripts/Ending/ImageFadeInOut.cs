using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFadeInOut : MonoBehaviour
{
    [SerializeField]
    private float fadeInDuration = 1.0f;
    [SerializeField]
    private float fadeOutDuration = 4.0f;

    private Image fadeImage = null;
    private bool isFadedIn = false;
    private bool isFadedOut = false;
    private float red;
    private float green;
    private float blue;
    private float currentAlpha;
    private float fadeInSpeed;
    private float fadeOutSpeed;

    private void Start()
    {
        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        currentAlpha = fadeImage.color.a;

        fadeInSpeed = 1.0f / fadeInDuration;
        fadeOutSpeed = 1.0f / fadeOutDuration;
    }

    private void Update()
    {
        if (isFadedOut)
        {
            return;
        }

        if (!isFadedIn)
        {
            if (currentAlpha < 1.0f)
            {
                currentAlpha += fadeInSpeed * Time.deltaTime;
                if (currentAlpha >= 1.0f)
                {
                    isFadedIn = true;
                    currentAlpha = 1.0f;
                }
            }
        }
        else
        {
            if (currentAlpha > 0.0f)
            {
                currentAlpha -= fadeOutSpeed * Time.deltaTime;
                if (currentAlpha <= 0.0f)
                {
                    isFadedOut = true;
                    currentAlpha = 0.0f;
                }
            }
        }
        fadeImage.color = new Color(red, green, blue, currentAlpha);
    }
}
