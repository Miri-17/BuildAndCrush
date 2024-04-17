using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderVisible : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D col;

    private void OnBecameVisible()
    {
        col.enabled = true;
    }

    private void OnBecameInvisible()
    {
        col.enabled = false;
    }
}
