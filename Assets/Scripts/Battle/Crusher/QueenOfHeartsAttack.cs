using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class QueenOfHeartsAttack : MonoBehaviour
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
    [SerializeField] private float chargeTime = 5.0f;
    [SerializeField] private GameObject chargeEffect;
    [SerializeField] private GameObject chargeEffectGorld;

    #endregion

    private Animator _animator = null;
    private AudioSource _audioSource;
    private float _nextAttackTime = 0.0f;
    private float _chargeCounter;
    private int _oldDamage;
    private float _oldAttackRange;

    public bool isCharge;

    private void Start()
    {
        _chargeCounter = 0.0f;
        _oldAttackRange = attackRange;
        _oldDamage = damage;
        isCharge = true;
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Time.time >= _nextAttackTime)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                float horizontalKey = Input.GetAxisRaw("Horizontal");
                float verticalKey = Input.GetAxisRaw("Vertical");

                if (_chargeCounter >= chargeTime)
                {
                    damage = _oldDamage + 2;
                    attackRange = _oldAttackRange * 4f;
                }
                else
                {
                    damage = _oldDamage;
                    attackRange = _oldAttackRange;
                }

                if (verticalKey > 0)
                {
                    _animator.SetTrigger("attack_upwards");
                    Attack(attackPointUp);
                }
                else if (verticalKey < 0)
                {
                    _animator.SetTrigger("attack_downwards");
                    Attack(attackPointDown);
                }
                else
                {
                    _animator.SetTrigger("attack");
                    Attack(attackPoint);
                    if (horizontalKey > 0)
                    {
                        //transform.DOMoveX(-20, 0.2f).SetRelative();
                    }
                    else if (horizontalKey < 0)
                    {
                        //transform.DOMoveX(20, 0.2f).SetRelative();
                    }
                }

                if (_audioSource != null)
                {
                    _audioSource.PlayOneShot(_audioSource.clip);
                }

                _nextAttackTime = Time.time + 1.0f / attackRate;
                _chargeCounter = 0;
                if (!isCharge)
                {
                    chargeEffectGorld.SetActive(false);
                }

                chargeEffect.SetActive(false);
                isCharge = true;
            }

            if (Input.GetButton("Fire1"))
            {
                _chargeCounter += 1 * Time.deltaTime;
                _animator.SetBool("attack_charge", true);
                if (isCharge)
                {
                    if (_chargeCounter >= chargeTime)
                    {
                        chargeEffect.SetActive(false);
                        chargeEffectGorld.SetActive(true);
                        isCharge = false;
                        Debug.Log(_chargeCounter);
                    }
                    else
                    {
                        chargeEffect.SetActive(true);
                    }
                }
            }
            else
            {
                _animator.SetBool("attack_charge", false);
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
            Bushi bushi = hitInfo.transform.GetComponent<Bushi>();
            BuilderDestroy builderDestroy = hitInfo.transform.GetComponent<BuilderDestroy>();


            /*if (woodbox != null)
            {
                woodbox.TakeDamage(damage);
            }*/
            if (brick != null)
            {
                brick.TakeDamage(damage);
                Instantiate(obstaclesCrushEffect, hitInfo.transform.position, Quaternion.identity);
            }

            if (spike != null)
            {
                spike.TakeDamage(damage);
                Instantiate(obstaclesCrushEffect, hitInfo.transform.position, Quaternion.identity);
            }

            if (rose != null)
            {
                rose.TakeDamage(damage);
                Instantiate(obstaclesCrushEffect, hitInfo.transform.position, Quaternion.identity);
            }

            if (canon != null)
            {
                canon.TakeDamage(damage);
                Instantiate(obstaclesCrushEffect, hitInfo.transform.position, Quaternion.identity);
            }

            if (wolf != null)
            {
                wolf.TakeDamage(damage);
                Instantiate(obstaclesCrushEffect, hitInfo.transform.position, Quaternion.identity);
            }

            //daichi changed

            if (halberd != null)
            {
                halberd.TakeDamage(damage);
                Instantiate(obstaclesCrushEffect, hitInfo.transform.position, Quaternion.identity);
            }

            if (pig != null)
            {
                pig.TakeDamage(damage);
                Instantiate(obstaclesCrushEffect, hitInfo.transform.position, Quaternion.identity);
            }

            if (partsDestroy != null)
            {
                partsDestroy.TakeDamage(damage);
                Instantiate(obstaclesCrushEffect, hitInfo.transform.position, Quaternion.identity);
            }

            if (bushi != null)
            {
                bushi.TakeDamage(damage);
                Instantiate(obstaclesCrushEffect, hitInfo.transform.position, Quaternion.identity);
            }

            if (builderDestroy != null)
            {
                builderDestroy.TakeDamage(damage);
                Instantiate(obstaclesCrushEffect, hitInfo.transform.position, Quaternion.identity);
            }
        }
    }
}