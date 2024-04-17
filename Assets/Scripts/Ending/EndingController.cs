using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingController : MonoBehaviour
{
    [HideInInspector]
    public bool fadeBGM = false;

    [SerializeField]
    private ComicsManager comicsManager;
    [SerializeField]
    private ImageFade imageFade;

    private bool firstPushY = false;
    private bool firstPushR = false;

    private void Start()
    {
        imageFade.enabled = false;
    }

    private void Update()
    {
        if (!firstPushY && comicsManager.isCompleted && Input.GetButtonDown("select"))
        {
            firstPushY = true;
            StartCoroutine("GoNextScene");
        }

        if (!comicsManager.isSkipEnabled)
        {
            return;
        }

        if (!firstPushR && Input.GetButtonDown("Fire1"))
        {
            firstPushR = true;
            StartCoroutine("GoNextScene");
        }
    }

    private IEnumerator GoNextScene()
    {
        fadeBGM = true;
        imageFade.enabled = true;
        yield return new WaitForSeconds(1.0f);
        //Debug.Log("Go back to ModeSelection");
        SceneManager.LoadScene("ModeSelection");
    }
}
