using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingBGM : MonoBehaviour
{
    private AudioSource audioSource;
    private float fadeOutSeconds = 1.0f;
    private float fadeDeltaTime = 0;
    /*private bool fadeInOut = false;*/

    [SerializeField]
    private EndingController endingController;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1;
    }

    private void Update()
    {
        if (endingController.fadeBGM)
        {
            fadeDeltaTime += Time.deltaTime;
            if (fadeDeltaTime > fadeOutSeconds)
            {
                fadeDeltaTime = fadeOutSeconds;
                /*fadeInOut = false;*/
            }
            audioSource.volume = 1 - fadeDeltaTime / fadeOutSeconds;
        }
    }

    /*private IEnumerator VolumeDown()
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
    }*/
}
