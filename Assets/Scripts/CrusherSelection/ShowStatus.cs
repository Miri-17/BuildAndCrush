using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowStatus : MonoBehaviour
{
    #region
    [Header("ステータス画像"), SerializeField]
    private Sprite[] statuses;
    [Header("難易度テキスト"), SerializeField]
    private Text difficultyLevelText;
    [Header("型テキスト"), SerializeField]
    private Text abilityText;
    #endregion

    #region
    private Image status = null;
    private int index = 0;
    private bool firstPushR = false;
    private string[] difficultyLevels =
    {
        "難易度★☆☆",
        "難易度★★☆",
        "難易度★★★",
        "難易度★★☆",
    };
    private string[] abilities =
    {
        "遠距離バランス型：弾速が速く扱いやすい。足が少し速い。",
        "近距離パワー型：攻撃を溜めると威力と射程がとても上がる。",
        "近距離スピード型：攻撃速度に優れているが、射程が短い。足が速い。",
        "中距離特殊型：弾速が遅い。物体に接近して攻撃すると追加ダメージ。足が少し遅い。",
    };
    #endregion

    private void Start()
    {
        status = GetComponent<Image>();
        status.sprite = statuses[0];
        this.gameObject.GetComponent<CanvasGroup>().alpha = 0;

        if (SceneManager.GetActiveScene().name == "CrusherSelection")
        {
            difficultyLevelText.text = difficultyLevels[0];
            abilityText.text = abilities[0];
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");

            if (horizontalInput > 0)
            {
                index++;
                if (index >= statuses.Length)
                {
                    index = 0;
                }

                status.sprite = statuses[index];
                if (SceneManager.GetActiveScene().name == "CrusherSelection")
                {
                    difficultyLevelText.text = difficultyLevels[index];
                    abilityText.text = abilities[index];
                    if (index == 3)
                    {
                        abilityText.fontSize = 44;
                    }
                    else
                    {
                        abilityText.fontSize = 50;
                    }
                }
            }
            else if (horizontalInput < 0)
            {
                index--;
                if (index < 0)
                {
                    index = statuses.Length - 1;
                }

                status.sprite = statuses[index];
                if (SceneManager.GetActiveScene().name == "CrusherSelection")
                {
                    difficultyLevelText.text = difficultyLevels[index];
                    abilityText.text = abilities[index];
                    if (index == 3)
                    {
                        abilityText.fontSize = 44;
                    }
                    else
                    {
                        abilityText.fontSize = 50;
                    }
                }
            }
        }

        // Rボタンが押されたらステータスの表示または非表示を行う.
        if (!firstPushR && Input.GetButtonDown("Fire1"))
        {
            firstPushR = true;
            if (this.gameObject.GetComponent<CanvasGroup>().alpha == 0)
            {
                // Debug.Log("ステータス表示");
                this.gameObject.GetComponent<CanvasGroup>().alpha = 1;
            }
            else
            {
                // Debug.Log("ステータス非表示");
                this.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            }
        }
        else
        {
            // 連続押し防止用
            firstPushR = false;
        }
    }
}
