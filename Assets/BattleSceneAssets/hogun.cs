using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hogun : MonoBehaviour
{
    public float speed = 0.1f; // �������ւ̑��x
    public float lifetime = 2.0f; // ���ݎ��ԁi�b�j

    private float elapsedTime = 0.0f;

    void Update()
    {
        // �������Ɉړ�
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // �o�ߎ��Ԃ��J�E���g
        elapsedTime += Time.deltaTime;

        // ���ݎ��Ԃ�lifetime�𒴂�����폜
        if (elapsedTime >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
