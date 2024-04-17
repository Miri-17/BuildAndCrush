using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audioClip;

    private bool firstPushY = false;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // audioSource.clip = audioClip[0];
        audioSource.loop = false;
        // audioSource.Play();
        audioSource.PlayOneShot(audioClip[0]);
    }

    private void Update()
    {
        if (!firstPushY && Input.GetButtonDown("select"))
        {
            firstPushY = true;

            if (GameDirector.Instance.crusherWin)
            {
                switch (GameDirector.Instance.crusherIndex)
                {
                    case 0:
                        if (GameDirector.Instance.builderIndex == 0)
                        {
                            Debug.Log("Go to EndingAE");
                            SceneManager.LoadScene("EndingAE");
                        }
                        else
                        {
                            Debug.Log("Go to Ending_Crossover_CrusherWin");
                            SceneManager.LoadScene("Ending_Crossover_CrusherWin");
                        }
                        break;
                    case 1:
                        if (GameDirector.Instance.builderIndex == 1)
                        {
                            Debug.Log("Go to EndingBF");
                            SceneManager.LoadScene("EndingBF");
                        }
                        else
                        {
                            Debug.Log("Go to Ending_Crossover_CrusherWin");
                            SceneManager.LoadScene("Ending_Crossover_CrusherWin");
                        }
                        break;
                    case 2:
                        if (GameDirector.Instance.builderIndex == 2)
                        {
                            Debug.Log("Go to EndingCG");
                            SceneManager.LoadScene("EndingCG");
                        }
                        else
                        {
                            Debug.Log("Go to Ending_Crossover_CrusherWin");
                            SceneManager.LoadScene("Ending_Crossover_CrusherWin");
                        }
                        break;
                    case 3:
                        if (GameDirector.Instance.builderIndex == 3)
                        {
                            Debug.Log("Go to EndingDH");
                            SceneManager.LoadScene("EndingDH");
                        }
                        else
                        {
                            Debug.Log("Go to Ending_Crossover_CrusherWin");
                            SceneManager.LoadScene("Ending_Crossover_CrusherWin");
                        }
                        break;
                    default:
                        Debug.Log("Go to EndingAE");
                        SceneManager.LoadScene("EndingAE");
                        break;
                }
            }
            else
            {
                switch (GameDirector.Instance.crusherIndex)
                {
                    case 0:
                        if (GameDirector.Instance.builderIndex == 0)
                        {
                            // Debug.Log("Go to EndingEA");
                            SceneManager.LoadScene("EndingEA");
                        }
                        else
                        {
                            // Debug.Log("Go to Ending_Crossover_BuilderWin");
                            SceneManager.LoadScene("Ending_Crossover_BuilderWin");
                        }
                        break;
                    case 1:
                        if (GameDirector.Instance.builderIndex == 1)
                        {
                            // Debug.Log("Go to EndingFB");
                            SceneManager.LoadScene("EndingFB");
                        }
                        else
                        {
                            // Debug.Log("Go to Ending_Crossover_BuilderWin");
                            SceneManager.LoadScene("Ending_Crossover_BuilderWin");
                        }
                        break;
                    case 2:
                        if (GameDirector.Instance.builderIndex == 2)
                        {
                            // Debug.Log("Go to EndingGC");
                            SceneManager.LoadScene("EndingGC");
                        }
                        else
                        {
                            // Debug.Log("Go to Ending_Crossover_BuilderWin");
                            SceneManager.LoadScene("Ending_Crossover_BuilderWin");
                        }
                        break;
                    case 3:
                        if (GameDirector.Instance.builderIndex == 3)
                        {
                            // Debug.Log("Go to EndingHD");
                            SceneManager.LoadScene("EndingHD");
                        }
                        else
                        {
                            // Debug.Log("Go to Ending_Crossover_BuilderWin");
                            SceneManager.LoadScene("Ending_Crossover_BuilderWin");
                        }
                        break;
                    default:
                        // Debug.Log("Go to EndingEA");
                        SceneManager.LoadScene("EndingEA");
                        break;
                }
            }
        }

        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioClip[1];
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
