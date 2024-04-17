using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundListController : MonoBehaviour
{
    // SoundListUIControllerでも使う変数
    public AudioSource soundAudioSource;

    // SoundListUIControllerでも使う変数
    [HideInInspector]
    public bool firstPushY = false;

    #region
    [SerializeField]
    private SoundListUIController soundListUIController;
    [SerializeField]
    private AudioClip[] soundAudioClip;
    #endregion

    private bool firstPushB = false;

    private void Update()
    {
        if (!firstPushY && Input.GetButtonDown("select"))
        {
            firstPushY = true;
            soundAudioSource.PlayOneShot(soundAudioClip[soundListUIController.index]);
        }

        if (!firstPushB && Input.GetButtonDown("Jump"))
        {
            firstPushB = true;
            // Debug.Log("Go back to ModeSelection");
            SceneManager.LoadScene("ModeSelection");
        }
    }
}
