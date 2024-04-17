using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hogun : MonoBehaviour
{
    public float speed = 0.1f; // 下方向への速度
    public float lifetime = 2.0f; // 存在時間（秒）

    private float elapsedTime = 0.0f;

    void Update()
    {
        // 下方向に移動
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // 経過時間をカウント
        elapsedTime += Time.deltaTime;

        // 存在時間がlifetimeを超えたら削除
        if (elapsedTime >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
