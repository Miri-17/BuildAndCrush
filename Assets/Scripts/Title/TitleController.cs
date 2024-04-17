using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    private bool firstPushY = false;
    // スタートボタンを押した時に鳴る音
    private AudioSource startSound;
    [SerializeField]
    private Text text;

    private void Start()
    {
        startSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!firstPushY && Input.GetButtonDown("select"))
        {
            firstPushY = true;
            startSound.Play();
            text.enabled = false;
            StartCoroutine("GoNextScene");
        }
    }

    private IEnumerator GoNextScene()
    {
        yield return new WaitForSeconds(0.5f);
        // Debug.Log("Go to ModeSelection");
        SceneManager.LoadScene("ModeSelection");
    }
}
