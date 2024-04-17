using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    private void Start()
    {
        if (GameDirector.Instance == null)
        {
            Debug.Log("GameDirector is missing.");
            Destroy(this);
        }
    }

    private void Update()
    {
        if (GameDirector.Instance.timeLimit <= 0)
        {
            if (GameDirector.Instance.crusherScore >= GameDirector.Instance.builderScore)
            {
                GameDirector.Instance.crusherWin = true;
                GameDirector.Instance.builderWin = false;
            }
            else
            {
                GameDirector.Instance.crusherWin = false;
                GameDirector.Instance.builderWin = true;
            }
            SceneManager.LoadScene("Result");
        }
    }
}
