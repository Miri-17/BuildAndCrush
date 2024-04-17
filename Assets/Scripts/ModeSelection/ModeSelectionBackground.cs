using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeSelectionBackground : MonoBehaviour
{
    [SerializeField]
    private Sprite[] backgrounds;

    private Image background = null;
    private int index = 0;

    private void Start()
    {
        background = GetComponent<Image>();
        // 始めは index = 0 の background に設定
        background.sprite = backgrounds[index];
    }

    private void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");

            if (horizontalInput > 0)
            {
                index++;
                if (index >= backgrounds.Length)
                {
                    index = 0;
                }

                background.sprite = backgrounds[index];
            }
            else if (horizontalInput < 0)
            {
                index--;
                if (index < 0)
                {
                    index = backgrounds.Length - 1;
                }

                background.sprite = backgrounds[index];
            }
        }
    }
}
