using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenjinAttack : MonoBehaviour
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

    [SerializeField] private GameObject attackEffect;

    #endregion

    private Animator animator = null;
    private AudioSource audioSource;
    private float nextAttackTime = 0.0f;
    private bool efCan = true;

    private void Start()

    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if (attackEffect == null)
        {
            efCan = false;
        }
    }

    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButton("Fire1"))
            {
                float verticalKey = Input.GetAxisRaw("Vertical");

                if (verticalKey > 0)
                {
                    animator.SetTrigger("attack_upwards");
                    Attack(attackPointUp);
                    if (efCan)
                    {
                        Instantiate(attackEffect, attackPointUp);
                    }
                }
                else if (verticalKey < 0)
                {
                    animator.SetTrigger("attack_downwards");
                    Attack(attackPointDown);
                    if (efCan)
                    {
                        Instantiate(attackEffect, attackPointDown);
                    }
                }
                else
                {
                    animator.SetTrigger("attack");
                    Attack(attackPoint);
                    if (efCan)
                    {
                        Instantiate(attackEffect, attackPoint);
                    }
                }

                if (audioSource != null)
                {
                    audioSource.PlayOneShot(audioSource.clip);
                }

                nextAttackTime = Time.time + 1.0f / attackRate;
            }
        }
    }

    private void Attack(Transform point)
    {
        Collider2D[] hitInfos = Physics2D.OverlapCircleAll(point.position, attackRange);

        
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