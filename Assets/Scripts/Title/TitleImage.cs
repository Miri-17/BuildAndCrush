using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleImage : MonoBehaviour
{
    [SerializeField]
    private RectTransform titleImage;
    [Header("横・縦に拡大する速さ"), SerializeField]
    private float speed = 5000.0f;

    private bool firstPushY = false;

    private void Start()
    {
        // titleImage変数がnullだったら
        if (titleImage == null)
        {
            // 現在のGameObjectからRectTransformコンポーネントを取得する
            titleImage = GetComponent<RectTransform>();
        }
    }

    private void Update()
    {
        if (!firstPushY && Input.GetButtonDown("select"))
        {
            firstPushY = true;
        }

        if (firstPushY)
        {
            Vector2 newSize = titleImage.sizeDelta;
            newSize.x += speed * Time.deltaTime * 5;
            newSize.y += speed * Time.deltaTime * 5;
            titleImage.sizeDelta = newSize;
        }
    }
}
