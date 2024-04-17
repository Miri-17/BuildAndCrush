using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelectionSE : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource = null;
    [SerializeField]
    private AudioClip[] sounds = new AudioClip[3];

    #region
    private bool firstPushY = false;
    private bool firstPushB = false;
    #endregion

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            audioSource.PlayOneShot(sounds[0]);
        }

        if (!firstPushY && Input.GetButtonDown("select"))
        {
            firstPushY = true;
            audioSource.PlayOneShot(sounds[1]);
        }

        if (!firstPushB && Input.GetButtonDown("Jump"))
        {
            firstPushB = true;
            audioSource.PlayOneShot(sounds[2]);
        }
    }
}
