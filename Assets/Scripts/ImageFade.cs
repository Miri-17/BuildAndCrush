using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFade : MonoBehaviour
{
    [SerializeField]
    private float fadeDuration = 1.0f;
    [SerializeField]
    private bool isFadeIn = false;

    private Image fadeImage = null;
    private bool isCompleted = false;
    private float red;
    private float green;
    private float blue;
    private float currentAlpha;
    private float fadeSpeed;

    private void Start()
    {
        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        currentAlpha = fadeImage.color.a;

        fadeSpeed = 1.0f / fadeDuration;
    }

    private void Update()
    {
        if (isCompleted)
        {
            return;
        }

        if (isFadeIn)
        {
            if (currentAlpha < 1.0f)
            {
                currentAlpha += fadeSpeed * Time.deltaTime;
                if (currentAlpha >= 1.0f)
                {
                    isCompleted = true;
                    currentAlpha = 1.0f;
                }
            }
        }
        else
        {
            if (currentAlpha > 0.0f)
            {
                currentAlpha -= fadeSpeed * Time.deltaTime;
                if (currentAlpha <= 0.0f)
                {
                    isCompleted = true;
                    currentAlpha = 0.0f;
                }
            }
        }
        fadeImage.color = new Color(red, green, blue, currentAlpha);
    }
}