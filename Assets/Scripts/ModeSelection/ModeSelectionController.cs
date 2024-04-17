using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeSelectionController : MonoBehaviour
{
    #region
    private int index = 0;
    private bool firstPushY = false;
    private bool firstPushB = false;
    private bool push = false;
    #endregion

    private void Update()
    {
        float horizontalKey = Input.GetAxisRaw("Horizontal");
        if (!push && horizontalKey > 0)
        {
            push = true;
            ++index;
            if (index > 2)
            {
                index = 0;
            }
            Debug.Log(index);
        }
        if (!push && horizontalKey < 0)
        {
            push = true;
            --index;
            if (index < 0)
            {
                index = 2;
            }
            Debug.Log(index);
        }

        // 1フレームのうち horizontal key が押されていない時があったら
        if (horizontalKey == 0)
        {
            // horizontal key を押せるようにする
            push = false;
        }

        if (!firstPushY && Input.GetButtonDown("select"))
        {
            firstPushY = true;

            switch (index) {
                case 0:
                    GameDirector.Instance.crusherIndex = 0;
                    Debug.Log("Go to CrusherSelection");
                    SceneManager.LoadScene("CrusherSelection");
                    break;
                case 1:
                    Debug.Log("Go to SoundList");
                    SceneManager.LoadScene("SoundList");
                    break;
                case 2:
                    Debug.Log("Go to Credits");
                    SceneManager.LoadScene("Credits");
                    break;
            }
        }

        if (!firstPushB && Input.GetButtonDown("Jump"))
        {
            firstPushB = true;
            //Debug.Log("Go back to Title");
            SceneManager.LoadScene("Title");
        }
    }
}
