using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherTriggerCheck : MonoBehaviour
{
    [HideInInspector]
    public bool isOn = false;

    private string crusherTag = "Crusher";

    #region
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == crusherTag)
        {
            isOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == crusherTag)
        {
            isOn = false;
        }
    }
    #endregion
}
