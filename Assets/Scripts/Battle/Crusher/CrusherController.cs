using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

// : Horizontal -> a, d
// : Vertical   -> w, s
// : select   -> j
// : Jump     -> k

public class CrusherController : MonoBehaviour
{
    #region
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float walkSpeed = 100.0f;
    [SerializeField]
    private float runSpeed = 180.0f;
    [SerializeField]
    private float jumpForce = 250.0f;
    #endregion

    /// <summary>
    /// run初期値（ガムで使います）
    /// </summary>
    private float _runValue;
    /// <summary>
    /// walk初期値（ガムで使います）
    /// </summary>
    private float _walkValue;
    /// <summary>
    /// jump初期値（ガムで使います）
    /// </summary>
    private float _jumpValue;

    private bool isChargeDeta;

    #region
    private Animator animator = null;
    private Rigidbody2D rb2D = null;
    private float xSpeed = 0.0f;
    private bool isFacingRight = true;
    private bool isWalking = false;
    private bool isRunning = false;
    private bool isJumping = false;
    private bool isStunning = false;
    private string[] crusherNames = { "Girl", "QueenOfHearts", "Tenjin", "Witch" };
    private string crusherName;
    private float addSpeedX = 0.0f;
    private BuilderController builderController;
    #endregion

    private enum MOVE_DIRECTION
    {
        STOP,
        RIGHT,
        LEFT,
    }

    MOVE_DIRECTION moveDirection = MOVE_DIRECTION.STOP;

    private void Start()
    {
        Application.targetFrameRate = 60;

        _runValue = runSpeed;
        _walkValue = walkSpeed;
        _jumpValue = jumpForce;

        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();

        crusherName = crusherNames[GameDirector.Instance.crusherIndex];

        builderController = GameObject.Find("BuilderController").GetComponent<BuilderController>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
        {
            isStunning = true;
            animator.Play(crusherName + "@stun");
        }

        GameDirector.Instance.crusherPosition = this.transform.position.x;

        float horizontalKey = Input.GetAxisRaw("Horizontal");
        bool runKey = Input.GetButton("select");
        if (horizontalKey == 0)
        {
            moveDirection = MOVE_DIRECTION.STOP;
            isWalking = false;
            isRunning = false;
        }
        else if (horizontalKey > 0)
        {
            moveDirection = MOVE_DIRECTION.RIGHT;

            if (!isFacingRight)
            {
                Flip();
            }

            if (runKey)
            {
                isWalking = false;
                isRunning = true;
            }
            else
            {
                isRunning = false;
                isWalking = true;
            }
        }
        else if (horizontalKey < 0)
        {
            moveDirection = MOVE_DIRECTION.LEFT;

            if (isFacingRight)
            {
                Flip();
            }

            if (runKey)
            {
                isWalking = false;
                isRunning = true;
            }
            else
            {
                isRunning = false;
                isWalking = true;
            }
        }

        if (IsGrounded())
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
                isJumping = true;
            }
            else
            {
                isJumping = false;
            }
        }

        // FixedUpdate()に書くと他の処理との兼ね合いか、ワゴンを出た後にスピードが元に戻らないので注意
        if (builderController.wagonControllerRun != null)
        {
            // ワゴンに乗ったらクラッシャーのスピードをワゴンのスピードとも関連づける
            if (builderController.wagonControllerRun.crusherEnterCheck.isOn)
            {
                addSpeedX = builderController.wagonControllerRun.GetWagonVelocity();
            }

            //ワゴンから降りたらクラッシャーのスピードを通常に戻す.
            if (builderController.wagonControllerRun.crusherExitCheck.isOn)
            {
                addSpeedX = 0.0f;
            }
        }
        
        SetAnimation();
    }

    private void FixedUpdate()
    {
        switch (moveDirection)
        {
            case MOVE_DIRECTION.STOP:
                xSpeed = 0.0f;
                break;
            case MOVE_DIRECTION.RIGHT:
                if (isRunning)
                {
                    xSpeed = runSpeed;
                }
                else
                {
                    xSpeed = walkSpeed;
                }

                break;
            case MOVE_DIRECTION.LEFT:
                if (isRunning)
                {
                    xSpeed = -runSpeed;
                }
                else
                {
                    xSpeed = -walkSpeed;
                }

                break;
        }
        
        rb2D.velocity = new Vector2(xSpeed + addSpeedX, rb2D.velocity.y);
    }

    private bool IsGrounded()
    {
        // Vector3 startRightVec = transform.position - transform.up * 19.0f + transform.right * 5.2f;  // Girl
        // Vector3 startRightVec = transform.position - transform.up * 20.0f + transform.right * 5.2f; // Tenjin
        Vector3 startRightVec = transform.position - transform.up * 18.5f + transform.right * 5.2f; // Witch
        // Vector3 startLeftVec = transform.position - transform.up * 19.0f - transform.right * 5.2f;   // Girl
        // Vector3 startLeftVec = transform.position - transform.up * 20.0f - transform.right * 5.2f;  // Tenjin
        Vector3 startLeftVec = transform.position - transform.up * 18.5f - transform.right * 5.2f; // Witch
        // Vector3 endVec = transform.position - transform.up * 19.2f;  // Girl
        // Vector3 endVec = transform.position - transform.up * 20.2f; // Tenjin
        Vector3 endVec = transform.position - transform.up * 19.5f; // Witch
        Debug.DrawLine(startRightVec, endVec);
        Debug.DrawLine(startLeftVec, endVec);
        return Physics2D.Linecast(startRightVec, endVec, groundLayer) ||
               Physics2D.Linecast(startLeftVec, endVec, groundLayer);
    }

    private void Jump()
    {
        rb2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    private void SetAnimation()
    {
        animator.SetBool("walk", isWalking);
        animator.SetBool("run", isRunning);
        animator.SetBool("jump", isJumping);
    }

    private async void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Obstacle"))
        {
            GameDirector.Instance.crusherKillCounts++;
            animator.Play(crusherName + "@stun");
            animator.SetBool("stun",true);
            isStunning = true;
            isRunning = false;
            isWalking = false;
            isJumping = false;
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            animator.SetBool("stun",false);
            isRunning = true;
            isWalking = true;
            isJumping = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gum"))
        {
            walkSpeed *= 0.5f;
            runSpeed *= 0.3f;
            jumpForce *= 0.7f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Gum"))
        {
            walkSpeed = _walkValue;
            runSpeed = _runValue;
            jumpForce = _jumpValue;
        }
    }

    [HideInInspector]
    public bool IsContinueWaiting()
    {
        return IsStunAnimationEnded();
    }

    private bool IsStunAnimationEnded()
    {
        if (isStunning && animator != null)
        {
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
            if (currentState.IsName(crusherName + "@stun"))
            {
                if (currentState.normalizedTime >= 1)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void ContinueCrusher()
    {
        isStunning = false;
        animator.Play(crusherName + "@idle");
        isJumping = false;
        isWalking = false;
        isRunning = false;
    }
}
