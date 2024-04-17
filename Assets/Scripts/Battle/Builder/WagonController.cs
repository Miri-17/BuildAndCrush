using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WagonController : MonoBehaviour
{
    
    #region
    // BuilderControllerでも使う変数
    public CrusherTriggerCheck crusherEnterCheck;
    public CrusherTriggerCheck crusherExitCheck;
    // StageGeneratorでも使う変数
    public GameObject wagonContinuePosition;
    #endregion

    // BuilderControllerのOnGoButtonClicked()で値を変更する変数
    [HideInInspector]
    public float xSpeed = 0.0f;
    [HideInInspector]
    public float speed = 180.0f;

    [SerializeField]
    private GameObject explosionEffect;

    private Rigidbody2D rb2D = null;
    private BuilderController builderController;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        builderController = GameObject.Find("BuilderController").GetComponent<BuilderController>();
    }

    private void Update()
    {
        if (crusherExitCheck.isOn)
        {
            DestroyWagon();
        }
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(xSpeed, 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GameOver")
        {
            // Debug.Log("GameOver");
            GameDirector.Instance.crusherWin = false;
            GameDirector.Instance.builderWin = true;
            // SceneManager.LoadScene("BuilderResult");
            SceneManager.LoadScene("Result");
        }
    }
    
    private void DestroyWagon()
    {
        builderController.goButtonFirstClick = false;
        GameDirector.Instance.wagonCrushCounts++;
        Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// ワゴンの速さを返す
    /// </summary>
    /// <returns>xSpeed</returns>
    public float GetWagonVelocity()
    {
        return xSpeed;
    }
}
