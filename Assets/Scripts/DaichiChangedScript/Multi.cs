using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multi : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("displays connected: " + Display.displays.Length);

        Display.displays[0].Activate();
        Display.displays[1].Activate();

        ////Secondary Displayの解像度設定（タイトルバー隠す）
        Invoke("DelayWindow", 0f);
    }

    private void DelayWindow()
    {
        WindowController.windowReplace(Application.productName, 100, 100, 640, 480, false);
        WindowController.windowReplace("Unity Secondary Display", 2560, 0, 3840, 2160, true);
    }
}
