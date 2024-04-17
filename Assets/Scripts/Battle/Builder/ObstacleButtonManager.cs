using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ObstacleButtonManager : MonoBehaviour
{
    // Clickerでも使用する変数.
    public GameObject[] obstaclePrefabs;
    public int index;

    // Clickerでも使用する変数.
    [HideInInspector]
    public bool isButtonDown = false;

    #region 
    [SerializeField]
    private BuilderController builderController;
    [SerializeField]
    private float[] weight;
    [SerializeField]
    private Image fillImage;
    [Header("何秒置きに障害物が置けるか"), SerializeField]
    private float[] duration;
    [SerializeField]
    private Image fillWeighingImage;
    [SerializeField]
    private Clicker clicker;
    #endregion

    private Button button;
    private float fillAmount;


    private void Start()
    {
        button = this.GetComponent<Button>();
        if (GameDirector.Instance != null)
        {
            fillWeighingImage.DOFillAmount(GameDirector.Instance.currentFill, 0.0f).SetLink(gameObject);
            fillAmount = weight[GameDirector.Instance.builderIndex] / 90;
        }
        else
        {
            Debug.Log("GameDirector is missing.");
            Destroy(this);
        }
    }

    private void Update()
    {
        if (builderController.isSetWagon[index] == true)
        {
            builderController.isSetWagon[index] = false;
            button.interactable = true;
        }
        if (builderController.isRunningWagon[index] == true)
        {
            builderController.isRunningWagon[index] = false;
            button.interactable = false;
        }

        if (button.interactable == true && clicker.isOtherButtonDown[index] == 2)
        {
            clicker.isOtherButtonDown[index] = 0;
        }
        else if (clicker.isOtherButtonDown[index] == 1)
        {
            button.interactable = false;
        }
        else if (clicker.isOtherButtonDown[index] == 2)
        {
            button.interactable = true;
        }
    }

    /// <summary>
    /// 障害物ボタンを押した時の挙動を設定するメソッド.
    /// </summary>
    public void PushObstacleButton()
    {
        isButtonDown = true;
        button.interactable = false;

        builderController.wagon.transform.Find("Grid").transform.gameObject.SetActive(true);
        builderController.wagonController.speed -= weight[GameDirector.Instance.builderIndex];
        // speed を90以下にはしない処理.
        if (builderController.wagonController.speed <= 90)
        {
            builderController.wagonController.speed = 90;
        }
    }

    /// <summary>
    /// 障害物が再度置けるようになることを示すバーの挙動を設定するメソッド.
    /// </summary>
    public void SetPartsGenerationBar()
    {
        fillImage.DOFillAmount(0.0f, 0.0f).SetLink(gameObject).OnComplete(() =>
        {
            fillImage.DOFillAmount(1.0f, duration[GameDirector.Instance.builderIndex]).SetLink(gameObject).OnComplete(() =>
            {
                if (builderController.isRunningWagon[index] == true)
                {
                    button.interactable = false;
                }
                else
                {
                    button.interactable = true;
                }
            });
        });
    }

    /// <summary>
    /// ワゴンの重量を示すバーの挙動を設定するメソッド.
    /// </summary>
    public void SetWeighingBar()
    {
        GameDirector.Instance.currentFill += fillAmount;
        fillWeighingImage.DOFillAmount(GameDirector.Instance.currentFill, 1.0f).SetLink(gameObject);
    }

    private bool Print()
    {
        Debug.Log(index);
        return true;
    }
}
