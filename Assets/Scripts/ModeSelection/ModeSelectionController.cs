using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeSelectionController : MonoBehaviour
{
    // RuleExplanationController でも使う関数
    [HideInInspector]
    public bool firstPushHorizontal = false;

    #region
    [SerializeField]
    private Image background;
    [SerializeField]
    private Sprite[] backgrounds;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private AudioSource SEaudioSource;
    [SerializeField]
    private AudioClip[] SEaudioClips;
    #endregion

    #region
    private int index = 0;
    private bool firstPushY = false;
    private bool firstPushB = false;
    // private bool firstPushHorizontal = false;
    private float horizontalKey = 0;
    private bool firstPushL = false;
    #endregion

    private void Start()
    {
        background.sprite = backgrounds[index];
        panel.SetActive(false);
    }

    private void Update()
    {
        if (!panel.activeSelf)
        {
            // 1フレームのうち horizontal key が押されていない時があったら
            if (firstPushHorizontal)
            {
                // horizontal key を押せるようにする
                firstPushHorizontal = false;
            }
            if (!firstPushHorizontal && Input.GetButtonDown("Horizontal"))
            {
                firstPushHorizontal = true;
                horizontalKey = Input.GetAxisRaw("Horizontal");

                if (horizontalKey > 0)
                {
                    index++;
                    if (index >= backgrounds.Length)
                    {
                        index = 0;
                    }

                    background.sprite = backgrounds[index];
                }
                else if (horizontalKey < 0)
                {
                    index--;
                    if (index < 0)
                    {
                        index = backgrounds.Length - 1;
                    }

                    background.sprite = backgrounds[index];
                }

                SEaudioSource.PlayOneShot(SEaudioClips[0]);
            }

            if (!firstPushY && Input.GetButtonDown("select"))
            {
                firstPushY = true;
                SEaudioSource.PlayOneShot(SEaudioClips[1]);

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
                SEaudioSource.PlayOneShot(SEaudioClips[2]);
                SceneManager.LoadScene("Title");
            }

            if (!firstPushL && Input.GetButtonDown("Fire1"))
            {
                firstPushL = true;
                panel.SetActive(true);
            }
        }
        else
        {
            if (firstPushL && Input.GetButtonDown("Jump"))
            {
                firstPushL = false;
                panel.SetActive(false);
            }
        }
    }

    public void PushButton()
    {
        if (panel.activeSelf)
        {
            firstPushL = false;
            panel.SetActive(false);
        }
        else
        {
            firstPushL = true;
            panel.SetActive(true);
        }
    }
}
