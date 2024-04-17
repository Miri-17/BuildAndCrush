using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBaseblockMikado : MonoBehaviour
{
    [Header("spawnPointの大元"), SerializeField]
    private GameObject spawnPointOriginal;
    [Header("ベースブロックの大元"), SerializeField]
    private GameObject baseBlockOriginal;

    private SpawnPoint[,] spawnPointsXY;

    [SerializeField]
    private BuilderController builderController;

    // ゲームスタートと同時にspawnPointのインスタンスを作成し、spawnPointsXYに代入する
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
        // 2 □  □  □  □  □  □  □  □  □  □  □  ■  ■  ■  □  □  □  □
        // 3 ■  ■  □  □  □  □  □  □  □  □  □  ■  ■  ■  □  □  □  □
        // 4 □  □  □  □  □  □  □  ■  ■  ■  ■  ■  ■  ■  □  □  □  □
        // 5 □  □  □  □  □  □  □  ■  ■  ■  ■  ■  ■  ■  □  □  □  □
        // 6 □  □  □  □  □  ■  ■  ■  ■  ■  ■  ■  ■  ■  □  □  □  □
        // 7 □  □  □  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  □  □  □  □
        // 8 □  □  □  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  □  □  □  □

        // ベースブロックが配置されるマスのspawnPointのisOccupiedをtrueにしておく
        for (int i = 11; i < 14; i++)
        {
            for (int j = 2; j < 4; j++)
            {
                spawnPointsXY[i, j].isOccupied = true;
            }
        }
        for (int i = 7; i < 14; i++)
        {
            for (int j = 4; j < 6; j++)
            {
                spawnPointsXY[i, j].isOccupied = true;
            }
        }
        for (int i = 5; i < 14; i++)
        {
            spawnPointsXY[i, 6].isOccupied = true;
        }
        for (int i = 3; i < 14; i++)
        {
            for (int j = 7; j < 9; j++)
            {
                spawnPointsXY[i, j].isOccupied = true;
            }
        }
        spawnPointsXY[0, 3].isOccupied = true;
        spawnPointsXY[1, 3].isOccupied = true;

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
        // 2 □  □  □  □  □  □  □  ■  □  □  □  □  □  □  □  □  □  ■
        // 3 □  □  □  □  □  □  ■  ■  ■  □  □  □  □  ■  □  □  ■  ■
        // 4 □  □  □  □  □  □  ■  ■  ■  ■  □  □  □  □  □  □  □  □
        // 5 □  □  □  □  ■  ■  ■  ■  ■  ■  ■  ■  ■  □  □  □  □  □
        // 6 □  □  □  □  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  □  □  □  □
        // 7 □  □  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  □  □  □  □
        // 8 □  □  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  □  □  □  □

        // ベースブロックが配置されるマスのspawnPointのisOccupiedをtrueにしておく
        for (int i = 4; i < 13; i++)
        {
            spawnPointsXY[i, 5].isOccupied = true;
        }
        for (int i = 4; i < 14; i++)
        {
            spawnPointsXY[i, 6].isOccupied = true;
        }
        for (int i = 2; i < 14; i++)
        {
            for (int j = 7; j < 9; j++)
            {
                spawnPointsXY[i, j].isOccupied = true;
            }
        }
        spawnPointsXY[6, 3].isOccupied = true;
        spawnPointsXY[6, 4].isOccupied = true;
        spawnPointsXY[7, 2].isOccupied = true;
        spawnPointsXY[7, 3].isOccupied = true;
        spawnPointsXY[7, 4].isOccupied = true;
        spawnPointsXY[8, 3].isOccupied = true;
        spawnPointsXY[8, 4].isOccupied = true;
        spawnPointsXY[9, 4].isOccupied = true;
        spawnPointsXY[13, 3].isOccupied = true;
        spawnPointsXY[16, 3].isOccupied = true;
        spawnPointsXY[17, 2].isOccupied = true;
        spawnPointsXY[17, 3].isOccupied = true;

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
        // 0 ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■
        // 1 ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■  ■
        // 2 ■  ■  ■  ■  ■  ■  ■  □  □  ■  □  □  □  □  □  ■  ■  ■
        // 3 □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □
        // 4 □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □
        // 5 □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □
        // 6 ■  □  □  □  □  ■  ■  ■  ■  ■  ■  □  □  ■  ■  ■  ■  ■
        // 7 □  □  □  □  ■  ■  ■  ■  ■  ■  ■  □  □  □  □  □  □  □
        // 8 □  □  □  ■  ■  ■  ■  ■  ■  ■  ■  □  □  □  □  □  □  □

        // ベースブロックが配置されるマスのspawnPointのisOccupiedをtrueにしておく
        for (int i = 0; i < 18; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                spawnPointsXY[i, j].isOccupied = true;
            }
        }
        for (int i = 0; i < 18; i++)
        {
            if (!(i >= 7 && i < 15))
            {
                spawnPointsXY[i, 2].isOccupied = true;
            }
        }
        for (int i = 5; i < 18; i++)
        {
            if (!(i == 11 || i == 12))
            {
                spawnPointsXY[i, 6].isOccupied = true;
            }
        }
        for (int i = 4; i < 11; i++)
        {
            spawnPointsXY[i, 7].isOccupied = true;
        }
        for (int i = 3; i < 11; i++)
        {
            spawnPointsXY[i, 8].isOccupied = true;
        }
        spawnPointsXY[0, 6].isOccupied = true;
        spawnPointsXY[9, 2].isOccupied = true;

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
        // 2 □  □  □  □  □  □  □  □  □  □  □  ■  ■  ■  □  □  □  □
        // 3 □  □  □  □  □  □  □  □  □  □  ■  □  □  □  □  □  □  ■
        // 4 □  □  □  □  □  □  □  □  □  ■  □  □  □  □  □  □  □  □
        // 5 ■  ■  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □  □
        // 6 □  □  □  □  □  □  ■  ■  □  □  □  □  □  □  □  □  ■  ■
        // 7 □  □  □  □  □  ■  ■  ■  ■  □  □  □  □  □  □  □  □  □
        // 8 □  □  □  □  ■  ■  ■  ■  ■  □  □  □  □  □  □  □  □  □

        // ベースブロックが配置されるマスのspawnPointのisOccupiedをtrueにしておく
        spawnPointsXY[0, 5].isOccupied = true;
        spawnPointsXY[1, 5].isOccupied = true;
        spawnPointsXY[4, 8].isOccupied = true;
        spawnPointsXY[5, 7].isOccupied = true;
        spawnPointsXY[5, 8].isOccupied = true;
        spawnPointsXY[6, 6].isOccupied = true;
        spawnPointsXY[6, 7].isOccupied = true;
        spawnPointsXY[6, 8].isOccupied = true;
        spawnPointsXY[7, 6].isOccupied = true;
        spawnPointsXY[7, 7].isOccupied = true;
        spawnPointsXY[7, 8].isOccupied = true;
        spawnPointsXY[8, 7].isOccupied = true;
        spawnPointsXY[8, 8].isOccupied = true;
        spawnPointsXY[9, 4].isOccupied = true;
        spawnPointsXY[10, 3].isOccupied = true;
        spawnPointsXY[11, 2].isOccupied = true;
        spawnPointsXY[12, 2].isOccupied = true;
        spawnPointsXY[13, 2].isOccupied = true;
        spawnPointsXY[16, 6].isOccupied = true;
        spawnPointsXY[17, 3].isOccupied = true;
        spawnPointsXY[17, 6].isOccupied = true;

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
