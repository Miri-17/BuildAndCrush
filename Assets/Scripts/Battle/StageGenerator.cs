using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    #region
    [SerializeField]
    private SpriteRenderer ground;
    [SerializeField]
    private Sprite[] grounds;

    [SerializeField]
    private SpriteRenderer background;
    [SerializeField]
    private Sprite[] backgrounds;

    [SerializeField]
    private GameObject[] crusherPrefab;
    [SerializeField]
    private GameObject[] builderPrefab;
    [SerializeField]
    private GameObject startPoint;
    [SerializeField]
    private GameObject[] continuePoint;
    [SerializeField]
    private BuilderController builderController;
    #endregion

    private GameObject crusher;
    private GameObject builder;
    private CrusherController crusherController;

    private float[] builderY = { -137.0f, -132.7f, -143.7f, -144.7f};

    private void Awake()
    {
        if (GameDirector.Instance != null)
        {
            ground.sprite = grounds[GameDirector.Instance.builderIndex];
            background.sprite = backgrounds[GameDirector.Instance.builderIndex];

            crusher = Instantiate(crusherPrefab[GameDirector.Instance.crusherIndex], new Vector3(0, -144, 0), Quaternion.identity);
            crusher.transform.parent = GameObject.Find("Crusher").transform;

            // デバッグ用
            // builder = Instantiate(builderPrefab[GameDirector.Instance.builderIndex], new Vector3(0, 0, 0), Quaternion.identity);
            builder = Instantiate(builderPrefab[GameDirector.Instance.builderIndex], new Vector3(5270.0f, builderY[GameDirector.Instance.builderIndex], -2), Quaternion.identity);
            builder.transform.parent = GameObject.Find("Builder").transform;
        }
        else
        {
            Debug.Log("GameDirector is missing!");
            Destroy(this);
        }
    }

    private void Start()
    {
        crusherController = crusher.GetComponent<CrusherController>();
        if (crusherController == null)
        {
            Debug.Log("CrusherController is missing!");
        }
    }

    private void Update()
    {
        if (crusherController != null && crusherController.IsContinueWaiting())
        {
            // crusher.transform.position = continuePoint[0].transform.position;
            crusher.transform.position = continuePoint[1].transform.position;
            crusherController.ContinueCrusher();
        }

        if (builderController.wagonControllerRun != null)
        {
            // ワゴンに乗ったらコンティニュー位置を変更.
            if (builderController.wagonControllerRun.crusherEnterCheck.isOn)
            {
                continuePoint[1] = builderController.wagonControllerRun.wagonContinuePosition;
            }

            // ワゴンから降りたらコンティニュー位置を変更したいが...
            // if (builderController.wagonControllerRun.crusherExitCheck.isOn)
            // {
            // }
        }
    }
}
