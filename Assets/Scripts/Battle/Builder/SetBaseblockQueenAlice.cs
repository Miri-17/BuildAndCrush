using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBaseblockQueenAlice : MonoBehaviour
{
    [Header("spawnPointの大元"), SerializeField]
    private GameObject spawnPointOriginal;
    [Header("ベースブロックの大元"), SerializeField]
    private GameObject baseBlockOriginal;

    private SpawnPoint[,] spawnPointsXY;

    [SerializeField]
    private BuilderController builderController;

    // ゲームスタートと同時にspawnPointのインスタンスを作成し、spawnPointsXYに代入する
    // public void SetSpawnPoint1()
    public void SetSpawnPoint()
    {
        float startPosX = -272.0f;
        float startPosY = 128.0f;
        float space = 32.0f;
        int index = Random.Range(0, 4);

        spawnPointsXY = new SpawnPoint[18, 9];

        for (int j = 0; j < 9; j++)
        {
            for (int i = 0; i < 18; i++)
            {
                GameObject spObj = Instantiate(spawnPointOriginal, new Vector3(0, 0, 0), Quaternion.identity);
                spObj.transform.parent = builderController.wagon.transform.Find("Grid").transform;
                spObj.transform.localPosition = new Vector3(startPosX + space * i, startPosY - space * j, 0);
                spawnPointsXY[i, j] = spObj.GetComponent<SpawnPoint>();
                spawnPointsXY[i, j].x = i;
                spawnPointsXY[i, j].y = j;
            }
        }

        switch(index)
        {
            case 0:
                SetBaseBlock0();
                break;
            case 1:
                SetBaseBlock1();
                break;
            case 2:
                SetBaseBlock2();
                break;
            default:
                SetBaseBlock3();
                break;
        }
    }

    private void SetBaseBlock0()
    {
        //   0  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17
        // 0 □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □
        // 1 □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □
        // 2 □  □  □  □  □  □  □  □  □  □  □  □  □  ■  □  □  □  □
        // 3 □  □  □  □  □  □  □  □  □  □  □  ■  □  □  □  □  □  □
        // 4 □  □  □  □  □  □  □  □  □  ■  □  □  □  □  □  □  □  □
        // 5 □  □  □  □  □  □  □  ■  □  □  □  □  □  □  □  □  □  □
        // 6 □  □  □  □  □  ■  □  □  □  □  □  □  □  ■  □  □  □  □
        // 7 □  □  □  ■  □  □  □  □  □  □  □  ■  ■  ■  □  □  □  □
        // 8 □  □  □  □  □  □  □  □  □  □  □  ■  ■  ■  □  □  □  □

        // ベースブロックが配置されるマスのspawnPointのisOccupiedをtrueにしておく
        spawnPointsXY[3, 7].isOccupied = true;
        spawnPointsXY[5, 6].isOccupied = true;
        spawnPointsXY[7, 5].isOccupied = true;
        spawnPointsXY[9, 4].isOccupied = true;
        spawnPointsXY[11, 3].isOccupied = true;
        spawnPointsXY[11, 7].isOccupied = true;
        spawnPointsXY[11, 8].isOccupied = true;
        spawnPointsXY[12, 7].isOccupied = true;
        spawnPointsXY[12, 8].isOccupied = true;
        spawnPointsXY[13, 2].isOccupied = true;
        spawnPointsXY[13, 6].isOccupied = true;
        spawnPointsXY[13, 7].isOccupied = true;
        spawnPointsXY[13, 8].isOccupied = true;

        // isOccupiedがtrueのところにベースブロックを配置する
        for (int j = 0; j < 9; j++)
        {
            for (int i = 0; i < 18; i++)
            {
                if (spawnPointsXY[i, j].isOccupied == true)
                {
                    GameObject bbObj = Instantiate(baseBlockOriginal, new Vector3(0, 0, 0), Quaternion.identity);
                    bbObj.transform.parent = builderController.wagon.transform;
                    bbObj.transform.localPosition = spawnPointsXY[i, j].transform.localPosition;
                }
            }
        }
    }

    private void SetBaseBlock1()
    {
        //   0  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17
        // 0 □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □
        // 1 □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □
        // 2 ■  □  □  □  ■  □  □  □  ■  □  □  □  □  □  □  □  □  □
        // 3 □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  ■  □
        // 4 □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □
        // 5 □  □  □  □  □  □  □  □  □  □  □  □  ■  □  □  □  □  □
        // 6 □  □  □  ■  ■  ■  □  □  □  □  □  ■  ■  □  □  □  □  □
        // 7 □  □  □  □  □  □  □  □  ■  ■  ■  ■  ■  □  □  □  □  □
        // 8 □  □  □  □  □  □  □  ■  ■  ■  ■  ■  ■  □  □  □  □  □

        // ベースブロックが配置されるマスのspawnPointのisOccupiedをtrueにしておく
        spawnPointsXY[0, 2].isOccupied = true;
        spawnPointsXY[3, 6].isOccupied = true;
        spawnPointsXY[4, 2].isOccupied = true;
        spawnPointsXY[4, 6].isOccupied = true;
        spawnPointsXY[5, 6].isOccupied = true;
        spawnPointsXY[7, 8].isOccupied = true;
        spawnPointsXY[8, 2].isOccupied = true;
        spawnPointsXY[8, 7].isOccupied = true;
        spawnPointsXY[8, 8].isOccupied = true;
        spawnPointsXY[9, 7].isOccupied = true;
        spawnPointsXY[9, 8].isOccupied = true;
        spawnPointsXY[10, 7].isOccupied = true;
        spawnPointsXY[10, 8].isOccupied = true;
        spawnPointsXY[11, 6].isOccupied = true;
        spawnPointsXY[11, 7].isOccupied = true;
        spawnPointsXY[11, 8].isOccupied = true;
        spawnPointsXY[12, 5].isOccupied = true;
        spawnPointsXY[12, 6].isOccupied = true;
        spawnPointsXY[12, 7].isOccupied = true;
        spawnPointsXY[12, 8].isOccupied = true;
        spawnPointsXY[16, 3].isOccupied = true;

        // isOccupiedがtrueのところにベースブロックを配置する
        for (int j = 0; j < 9; j++)
        {
            for (int i = 0; i < 18; i++)
            {
                if (spawnPointsXY[i, j].isOccupied == true)
                {
                    GameObject bbObj = Instantiate(baseBlockOriginal, new Vector3(0, 0, 0), Quaternion.identity);
                    bbObj.transform.parent = builderController.wagon.transform;
                    bbObj.transform.localPosition = spawnPointsXY[i, j].transform.localPosition;
                }
            }
        }
    }

    private void SetBaseBlock2()
    {
        //   0  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17
        // 0 □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □
        // 1 □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □
        // 2 □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □
        // 3 □  □  □  □  □  □  ■  ■  ■  ■  □  □  □  □  □  □  □  □
        // 4 □  □  □  □  □  □  □  □  □  ■  ■  □  □  □  □  □  □  □
        // 5 ■  ■  ■  ■  □  □  □  □  □  ■  ■  ■  □  □  □  □  □  □
        // 6 □  □  □  □  □  □  □  □  □  ■  ■  ■  ■  □  □  □  □  □
        // 7 □  □  □  □  □  ■  ■  ■  ■  ■  ■  ■  ■  ■  □  □  □  □
        // 8 □  □  □  □  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  □  □  □

        // ベースブロックが配置されるマスのspawnPointのisOccupiedをtrueにしておく
        for (int i = 5; i < 14; i++)
        {
            spawnPointsXY[i, 7].isOccupied = true;
        }
        for (int i = 4; i < 15; i++)
        {
            spawnPointsXY[i, 8].isOccupied = true;
        }
        spawnPointsXY[0, 5].isOccupied = true;
        spawnPointsXY[1, 5].isOccupied = true;
        spawnPointsXY[2, 5].isOccupied = true;
        spawnPointsXY[3, 5].isOccupied = true;
        spawnPointsXY[6, 3].isOccupied = true;
        spawnPointsXY[7, 3].isOccupied = true;
        spawnPointsXY[8, 3].isOccupied = true;
        spawnPointsXY[9, 3].isOccupied = true;
        spawnPointsXY[9, 4].isOccupied = true;
        spawnPointsXY[9, 5].isOccupied = true;
        spawnPointsXY[9, 6].isOccupied = true;
        spawnPointsXY[10, 4].isOccupied = true;
        spawnPointsXY[10, 5].isOccupied = true;
        spawnPointsXY[10, 6].isOccupied = true;
        spawnPointsXY[11, 5].isOccupied = true;
        spawnPointsXY[11, 6].isOccupied = true;
        spawnPointsXY[12, 6].isOccupied = true;

        // isOccupiedがtrueのところにベースブロックを配置する
        for (int j = 0; j < 9; j++)
        {
            for (int i = 0; i < 18; i++)
            {
                if (spawnPointsXY[i, j].isOccupied == true)
                {
                    GameObject bbObj = Instantiate(baseBlockOriginal, new Vector3(0, 0, 0), Quaternion.identity);
                    bbObj.transform.parent = builderController.wagon.transform;
                    bbObj.transform.localPosition = spawnPointsXY[i, j].transform.localPosition;
                }
            }
        }
    }

    private void SetBaseBlock3()
    {
        //   0  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17
        // 0 □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □
        // 1 □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □
        // 2 □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □
        // 3 □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □
        // 4 □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □
        // 5 □  □  □  □  ■  □  □  □  □  □  □  □  ■  □  □  □  □  □
        // 6 □  □  □  ■  ■  ■  □  □  □  □  □  ■  ■  ■  □  □  □  □
        // 7 □  □  ■  ■  ■  ■  ■  □  □  □  ■  ■  ■  ■  ■  □  □  □
        // 8 □  ■  ■  ■  ■  ■  ■  ■  □  ■  ■  ■  ■  ■  ■  ■  □  □

        // ベースブロックが配置されるマスのspawnPointのisOccupiedをtrueにしておく
        for (int i = 2; i < 15; i++)
        {
            if (!(i >= 7 && i <= 9))
            {
                spawnPointsXY[i, 7].isOccupied = true;
            }
        }
        for (int i = 1; i < 16; i++)
        {
            if (i != 8)
            {
                spawnPointsXY[i, 8].isOccupied = true;
            }
        }
        spawnPointsXY[3, 6].isOccupied = true;
        spawnPointsXY[4, 5].isOccupied = true;
        spawnPointsXY[4, 6].isOccupied = true;
        spawnPointsXY[5, 6].isOccupied = true;
        spawnPointsXY[11, 6].isOccupied = true;
        spawnPointsXY[12, 5].isOccupied = true;
        spawnPointsXY[12, 6].isOccupied = true;
        spawnPointsXY[13, 6].isOccupied = true;

        // isOccupiedがtrueのところにベースブロックを配置する
        for (int j = 0; j < 9; j++)
        {
            for (int i = 0; i < 18; i++)
            {
                if (spawnPointsXY[i, j].isOccupied == true)
                {
                    GameObject bbObj = Instantiate(baseBlockOriginal, new Vector3(0, 0, 0), Quaternion.identity);
                    bbObj.transform.parent = builderController.wagon.transform;
                    bbObj.transform.localPosition = spawnPointsXY[i, j].transform.localPosition;
                }
            }
        }
    }
}
