using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuilderL2DController : MonoBehaviour
{
    private Animator animator = null;
    private string[] builderNames = { "Wolf", "QueenAlice", "Mikado", "Hanzel_Gretel" };
    private string builderName;
    private int countPushY = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        builderName = builderNames[GameDirector.Instance.builderIndex];
        if (SceneManager.GetActiveScene().name == "Result")
        {
            if (GameDirector.Instance.builderWin)
            {
                animator.Play(builderName + "@winL2D");
            }
            else
            {
                animator.Play(builderName + "@loseL2D");
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

            if (Input.GetButtonDown("select"))
            {
                countPushY++;
                // 決定ボタンが注意が出た状態で押されたら選択時のアニメーションを行う.
                if (countPushY == 2)
                {
                    animator.Play(builderName + "@selectL2D");
                }
            }

            if (Input.GetButtonDown("Jump"))
            {
                countPushY--;
            }
        }
    }
}
