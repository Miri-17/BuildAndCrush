using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossoverOpeningController : MonoBehaviour
{
    [SerializeField]
    private CrossoverComicsManager comicsManager;
    [SerializeField]
    private ImageFade imageFade;

    private AudioSource startSound;
    // スタートボタンが押されたか
    private bool firstPushY = false;
    // スキップボタンが押されたか
    private bool firstPushR = false;

    private void Start()
    {
        startSound = GetComponent<AudioSource>();
        imageFade.enabled = false;
    }

    private void Update()
    {
        // コミックが終了していて、スタートボタンが初めて押されたら
        if (!firstPushY && comicsManager.isCompleted && Input.GetButtonDown("select"))
        {
            firstPushY = true;
            StartCoroutine("GoNextScene");
        }

        // スキップ可能なら
        if (!comicsManager.isSkipEnabled)
        {
            return;
        }

        // スキップボタンが初めて押されたら
        if (!firstPushR && Input.GetButtonDown("Fire1"))
        {
            firstPushR = true;
            StartCoroutine("GoNextScene");
        }
    }

    private IEnumerator GoNextScene()
    {
        startSound.Play();
        imageFade.enabled = true;
        yield return new WaitForSeconds(0.5f);
        //Debug.Log("Go to Battle");
        SceneManager.LoadScene("Battle");
    }
}
