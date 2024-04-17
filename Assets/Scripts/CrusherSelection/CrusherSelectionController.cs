using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrusherSelectionController : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource = null;
    [Header("0...進む音, 1...戻る音"), SerializeField]
    private AudioClip[] sounds = new AudioClip[2];

    private bool firstPushY = false;
    private bool firstPushB = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // 次へボタンを押したら
        if (!firstPushY && Input.GetButtonDown("select"))
        {
            firstPushY = true;
            audioSource.PlayOneShot(sounds[0]);

            // GameDirectorがあればBuilderSelectionへ遷移する
            if (GameDirector.Instance != null)
            {
                GameDirector.Instance.builderIndex = 0;
                // Debug.Log("Go to BuilderSelection");
                StartCoroutine("GoNextScene");
            }
            else
            {
                Debug.Log("GameDirector is missing!");
            }
        }

        // 戻るボタンを押したら
        if (!firstPushB && Input.GetButtonDown("Jump"))
        {
            firstPushB = true;
            audioSource.PlayOneShot(sounds[1]);
            //Debug.Log("Go back to ModeSelection");
            SceneManager.LoadScene("ModeSelection");
        }
    }

    private IEnumerator GoNextScene()
    {
        yield return new WaitForSeconds(1.6f);
        //Debug.Log("Go to BuilderSelection");
        SceneManager.LoadScene("BuilderSelection");
    }
}
