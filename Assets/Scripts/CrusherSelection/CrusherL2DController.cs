using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrusherL2DController : MonoBehaviour
{
    private Animator animator = null;
    private string[] crusherNames = { "Girl", "QueenOfHearts", "Tenjin", "Witch" };
    private string crusherName;

    private void Start()
    {
        animator = GetComponent<Animator>();
        crusherName = crusherNames[GameDirector.Instance.crusherIndex];
        if (SceneManager.GetActiveScene().name == "Result")
        {
            if (GameDirector.Instance.crusherWin)
            {
                animator.Play(crusherName + "@winL2D");
            }
            else
            {
                animator.Play(crusherName + "@loseL2D");
            }
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Result")
        {
            // 横ボタンが押されたらLive2Dモデルを消去する.
            if (Input.GetButtonDown("Horizontal"))
            {
                Destroy(gameObject);
            }
            // 決定ボタンが押されたら選択時のアニメーションを行う.
            if (Input.GetButtonDown("select"))
            {
                animator.Play(crusherName + "@selectL2D");
            }
        }
    }
}
