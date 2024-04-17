using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherCameraController : MonoBehaviour
{
    private GameObject crusher;

    private void Start()
    {
        crusher = GameObject.FindGameObjectWithTag("Crusher");
    }

    private void Update()
    {
        Vector3 crusherPos = crusher.transform.position;
        if (crusherPos.x > -25.0f && crusherPos.x < 5070.0f)
        {
            transform.position = new Vector3(crusherPos.x, transform.position.y, transform.position.z);
        }
    }
}
