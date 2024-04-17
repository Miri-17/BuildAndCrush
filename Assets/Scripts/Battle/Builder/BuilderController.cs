using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BuilderController : MonoBehaviour
{
    // ObstacleButtonManagerでも使用する変数.
    [HideInInspector]
    public bool[] isSetWagon = { false, false, false, false, false, false, };
    [HideInInspector]
    public bool[] isRunningWagon = { false, false, false, false, false, false, };

    // SetBaseblock~~のスクリプトでも使用する変数.
    [HideInInspector]
    public GameObject wagon;
    // 走っているwagonのWagonController.
    // BGMでも使用する変数.
    [HideInInspector]
    public WagonController wagonControllerRun = null;
    // WagonControllerでも使用する変数.
    [HideInInspector]
    public bool goButtonFirstClick = false;
    // 生成されたPrefabのWagonController.
    // ObstacleButtonManagerでも使用する変数.
    [HideInInspector]
    public WagonController wagonController = null;

    #region
    [Header("ビルダー用カメラ"), SerializeField]
    private Camera builderCamera;
    [Header("wagonの大元"), SerializeField]
    private GameObject[] wagonOriginal;
    [Header("ワゴン爆発エフェクト"), SerializeField]
    private GameObject explosionEffect;
    [Header("オオカミのベースブロック配置スクリプト"), SerializeField]
    private SetBaseblockWolf setBaseblockWolf;
    [Header("女王アリスのベースブロック配置スクリプト"), SerializeField]
    private SetBaseblockQueenAlice setBaseblockQueenAlice;
    [Header("御門のベースブロック配置スクリプト"), SerializeField]
    private SetBaseblockMikado setBaseblockMikado;
    [Header("ヘンゼルとグレーテルのベースブロック配置スクリプト"), SerializeField]
    private SetBaseblockHanzelGretel setBaseblockHanzelGretel;
    [SerializeField]
    private Clicker clicker;
    [SerializeField]
    private Image fillWeighingImage;
    #endregion

    #region
    private Obstacles obstacleToPlace;
    private SpawnPoint[,] spawnPointsXY;
    #endregion


    private void Start()
    {
        if (GameDirector.Instance != null)
        {
            SetWagon(new Vector3(5734, 11, 0));
            fillWeighingImage.fillAmount = GameDirector.Instance.currentFill;
        }
        else
        {
            Debug.Log("GameDirector is missing!");
            Destroy(this);
        }
        GameDirector.Instance.builderPosition = 5734.0f;
    }

    private void Update()
    {
        // if (Input.GetMouseButtonDown(0) && obstacleToPlace != null)
        // {
        //     SpawnPoint nearestPoint = null;
        //     float shortestDistance = float.MaxValue;

        //     foreach (SpawnPoint spawnPoint in spawnPointsXY)
        //     {
        //         float dist = Vector2.Distance(spawnPoint.transform.position, builderCamera.ScreenToWorldPoint(Input.mousePosition));
        //         if (dist < shortestDistance)
        //         {
        //             shortestDistance = dist;
        //             nearestPoint = spawnPoint;
        //         }
        //     }

        //     // デバッグ用
        //     string objName = obstacleToPlace.name;
        //     Debug.Log("配置したオブジェクト名: " + objName);

        //     // 1×1のオブジェクトを配置する場合
        //     if (objName == "WoodBox" || objName == "Spike" || objName == "Canon" || objName == "Wolf" || objName == "Rose" || objName == "Brick")
        //     {
        //         if (nearestPoint.isOccupied == false)
        //         {
        //             Obstacles cloneObj = Instantiate(obstacleToPlace, nearestPoint.transform.position, Quaternion.identity);
        //             cloneObj.transform.parent = wagon.transform;
        //             obstacleToPlace = null;
        //             nearestPoint.isOccupied = true;
        //             wagon.transform.Find("Grid").transform.gameObject.SetActive(false);
        //             customCursor.gameObject.SetActive(false);
        //             Cursor.visible = true;
        //         }
        //     }

            // 1×2のオブジェクトを配置する場合
            // ・yの値が0だと配置できない
            // ・(x, y)=(4, 2)が埋まっている場合、(4, 3)には配置できない
            /*   0  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17
             * 0× × × × × × × × × × × × × × × × × ×
             * 1□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □
             * 2□ □ □ □ ■ □ □ □ □ □ □ □ □ □ □ □ □ □
             * 3□ □ □ □ × □ □ □ □ □ □ □ □ □ □ □ □ □
             * 4□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □
             * 5□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □
             * 6□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □
             * 7□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □
             * 8□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □
             */
            /*if (今はここに該当するオブジェクトがないためコメントアウト)
            {
                int spX = nearestPoint.x;
                int spY = nearestPoint.y;
                if (spY != 0 && nearestPoint.isOccupied == false && spawnPointsXY[spX, spY - 1].isOccupied == false)
                {
                    Obstacles cloneObj = Instantiate(obstacleToPlace, nearestPoint.transform.position, Quaternion.identity);
                    cloneObj.transform.parent = GameObject.Find("Wagon").transform;
                    obstacleToPlace = null;
                    nearestPoint.isOccupied = true;
                    spawnPointsXY[spX, spY - 1].isOccupied = true;    // 1つ上のマスも埋まっていることにする
                    grid.SetActive(false);
                    customCursor.gameObject.SetActive(false);
                    Cursor.visible = true;
                }
            }
            */
        // }

        if (wagon.transform.position.x < 5060)
        {
            SetWagon(new Vector3(5734, 11, 0));
        }
        
        if (wagonControllerRun != null)
        {
            GameDirector.Instance.builderPosition = wagonControllerRun.transform.position.x;
        }
    }

    // ワゴンを配置する座標を受け取りワゴンを配置する.
    private void SetWagon(Vector3 pos)
    {
        wagon = Instantiate(wagonOriginal[GameDirector.Instance.builderIndex], pos, Quaternion.identity);
        wagon.transform.parent = GameObject.Find("Builder").transform;
        switch (GameDirector.Instance.builderIndex)
        {
            case 0:
                setBaseblockWolf.SetSpawnPoint();
                break;
            case 1:
                setBaseblockQueenAlice.SetSpawnPoint();
                break;
            case 2:
                setBaseblockMikado.SetSpawnPoint();
                break;
            default:
                setBaseblockHanzelGretel.SetSpawnPoint();
                break;
        }

        wagonController = wagon.GetComponent<WagonController>();
        for (int i = 0; i < 6; i++)
        {
            isSetWagon[i] = true;
        }
    }

    // GoButtonがクリックされた時に呼ばれ、Wagonを移動させる.
    public void OnGoButtonClicked()
    {
        // ボタン連打の阻止.
        if (!goButtonFirstClick)
        {
            goButtonFirstClick = true;
            wagonControllerRun = wagonController;
            wagonControllerRun.xSpeed = -wagonControllerRun.speed;
            GameDirector.Instance.currentFill = 0.0f;
            fillWeighingImage.fillAmount = GameDirector.Instance.currentFill;
            
            for (int i = 0; i < 6; i++)
            {
                isRunningWagon[i] = true;
            }
        }
    }
}
