
using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class WitchAttack : MonoBehaviour
{
    #region

    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform attackPointUp;
    [SerializeField] private Transform attackPointDown;
    [SerializeField] private float attackRange = 10.0f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private int damage = 2;
    [SerializeField] private GameObject obstaclesCrushEffect;
    [SerializeField] private float attackRate = 2.0f;
    [SerializeField] private float burstRate = 0.2f;
    [SerializeField] private GameObject cupcake;
    // [SerializeField] private GameObject cupcakePush;
    [SerializeField] private AudioClip cupcakeSE;

    #endregion

    private Animator animator = null;
    private AudioSource audioSource;
    private float nextAttackTime = 0.0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private async void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                float verticalKey = Input.GetAxisRaw("Vertical");
                float horizontalKey = Input.GetAxisRaw("Horizontal");
                

                if (verticalKey > 0)
                {
                    animator.SetTrigger("attack_upwards");
                    Attack(attackPointUp);
                    Instantiate(cupcake, attackPointUp.position, attackPointUp.rotation);
                    await UniTask.Delay(TimeSpan.FromSeconds(burstRate));
                    Instantiate(cupcake, attackPointUp.position, attackPointUp.rotation);
                    await UniTask.Delay(TimeSpan.FromSeconds(burstRate));
                    Instantiate(cupcake, attackPointUp.position, attackPointUp.rotation);
                }
                else if (verticalKey < 0)
                {
                    animator.SetTrigger("attack_downwards");
                    Attack(attackPointDown);
                    Instantiate(cupcake, attackPointDown.position,attackPointDown.rotation);
                    await UniTask.Delay(TimeSpan.FromSeconds(burstRate));
                    Instantiate(cupcake, attackPointDown.position,attackPointDown.rotation);
                    await UniTask.Delay(TimeSpan.FromSeconds(burstRate));
                    Instantiate(cupcake, attackPointDown.position,attackPointDown.rotation);
                    //Instantiate(cupcake, attackPointDown.transform);
                }
                else
                {
                    animator.SetTrigger("attack");
                    Attack(attackPoint);
                    Instantiate(cupcake, attackPoint.position,attackPoint.rotation);
                    await UniTask.Delay(TimeSpan.FromSeconds(burstRate));
                    Instantiate(cupcake, attackPoint.position,attackPoint.rotation);
                    await UniTask.Delay(TimeSpan.FromSeconds(burstRate));
                    Instantiate(cupcake, attackPoint.position,attackPoint.rotation);
                    //Instantiate(cupcake, attackPoint.transform);
                }

                if (audioSource != null)
                {
                    audioSource.PlayOneShot(cupcakeSE);
                }

                nextAttackTime = Time.time + 1.0f / attackRate;
            }
        }
    }

    private void Attack(Transform point)
    {
        Collider2D[] hitInfos = Physics2D.OverlapCircleAll(point.position, attackRange, obstacleLayer);

        foreach (Collider2D hitInfo in hitInfos)
        {
            //WoodBox woodbox = hitInfo.transform.GetComponent<WoodBox>();
            Brick brick = hitInfo.transform.GetComponent<Brick>();
            Spike spike = hitInfo.transform.GetComponent<Spike>();
            Rose rose = hitInfo.transform.GetComponent<Rose>();
            Canon canon = hitInfo.transform.GetComponent<Canon>();
            Wolf wolf = hitInfo.transform.GetComponent<Wolf>();
            //daichi changed
            Halberd halberd = hitInfo.transform.GetComponent<Halberd>();
            Pig pig = hitInfo.transform.GetComponent<Pig>();
            PartsDestroy partsDestroy = hitInfo.transform.GetComponent<PartsDestroy>();

            /*if (woodbox != null)
            {
                woodbox.TakeDamage(damage);
            }*/
            if (brick != null)
            {
                brick.TakeDamage(damage);
            }

            if (spike != null)
            {
                spike.TakeDamage(damage);
            }

            if (rose != null)
            {
                rose.TakeDamage(damage);
            }

            if (canon != null)
            {
                canon.TakeDamage(damage);
            }

            if (wolf != null)
            {
                wolf.TakeDamage(damage);
            }

            //daichi changed
            if (halberd != null)
            {
                halberd.TakeDamage(damage);
            }

            if (pig != null)
            {
                pig.TakeDamage(damage);
            }

            if (partsDestroy != null)
            {
                partsDestroy.TakeDamage(damage);
            }

            Instantiate(obstaclesCrushEffect, hitInfo.transform.position, Quaternion.identity);
        }
    }
}