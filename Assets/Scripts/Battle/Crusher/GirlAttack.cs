using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GirlAttack : MonoBehaviour
{
    #region

    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform attackPointUp;
    [SerializeField] private Transform attackPointDown;
    [SerializeField] private float attackRange = 100.0f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private int damage = 2;
    [SerializeField] private GameObject obstaclesCrushEffect;
    [SerializeField] private float attackRate = 2.0f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject attackEffect;

    #endregion

    private Animator animator = null;
    private AudioSource audioSource;
    private float nextAttackTime = 0.0f;
    private bool efCan = true;
    private bool isHorizon = false;

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
                    isHorizon = true;
                    animator.SetTrigger("attack_upwards");
                    Attack(attackPointUp);
                    //StartCoroutine(Shoot(attackPointUp, Vector2.up));
                    if (efCan)
                    {
                        Instantiate(attackEffect, attackPointUp.transform);
                    }

                    
                }
                else if (verticalKey < 0)
                {
                    isHorizon = true;
                    animator.SetTrigger("attack_downwards");
                    Attack(attackPointDown);
                    //StartCoroutine(Shoot(attackPointDown, Vector2.down));
                    if (efCan)
                    {
                        Instantiate(attackEffect, attackPointDown.transform);
                    }

                   
                }
                else
                {
                    isHorizon = false;
                    animator.SetTrigger("attack");
                    Attack(attackPoint);
                    if (efCan)
                    {
                        Instantiate(attackEffect, attackPoint.transform);
                    }

                    animator.SetTrigger("attack");
                    if (transform.rotation.y < 0.0f)
                    {
                        //StartCoroutine(Shoot(attackPoint, Vector2.left));
                    }
                    else
                    {
                        //StartCoroutine(Shoot(attackPoint, Vector2.right));
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
        float lineAngle = 0;
        if (isHorizon)
        {
            lineAngle = 90;
        }
        else
        {
            lineAngle = 0;
        }

        Collider2D[] hitInfos = Physics2D.OverlapBoxAll(point.position, new Vector2(attackRange, 1), lineAngle);


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

    private IEnumerator Shoot(Transform point, Vector2 direction)
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(point.position, direction);

        /*if (hitInfo && hitInfo.distance < 100.0f)
        {
            //Instantiate(obstaclesCrushEffect, hitInfo.point, Quaternion.identity);

            lineRenderer.SetPosition(0, point.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            lineRenderer.SetPosition(0, point.position);
            lineRenderer.SetPosition(1, (Vector2)point.position + direction * 99.0f);
        }*/

        lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.02f);

        lineRenderer.enabled = false;
    }
}
/*#region

[SerializeField] private Transform firePoint;
[SerializeField] private Transform firePointUp;
[SerializeField] private Transform firePointDown;
[SerializeField] private int damage = 2;
[SerializeField] private LineRenderer lineRenderer;
[SerializeField] private GameObject obstaclesCrushEffect;
[SerializeField] private float attackRate = 2.0f;
[SerializeField] private GameObject burret;

#endregion

private Animator animator = null;
private AudioSource audioSource;
private float nextAttackTime = 0.0f;

private void Start()
{
    animator = GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();
}

private void Update()
{
    if (Time.time >= nextAttackTime)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            float verticalKey = Input.GetAxisRaw("Vertical");

            if (verticalKey > 0)
            {
                animator.SetTrigger("attack_upwards");
                StartCoroutine(Shoot(firePointUp, Vector2.up));
                //Instantiate(burret, firePointUp.position, firePointUp.rotation);
                // burret.transform.DOMoveY(1000, 0.2f).SetRelative().SetLink(gameObject);
            }
            else if (verticalKey < 0)
            {
                animator.SetTrigger("attack_downwards");
                StartCoroutine(Shoot(firePointDown, Vector2.down));
                Instantiate(burret, firePointDown.position, firePointDown.rotation);
                //burret.transform.DOMoveX(-1000 ,0.2f).SetRelative().SetLink(gameObject);
            }
            else
            {
                animator.SetTrigger("attack");
                if (transform.rotation.y < 0.0f)
                {
                    StartCoroutine(Shoot(firePoint, Vector2.left));
                    //burret.transform.DOMoveX(-1000, 0.2f).SetRelative().SetLink(gameObject);
                }
                else
                {
                    StartCoroutine(Shoot(firePoint, Vector2.right));
                    // burret.transform.DOMoveX(1000, 0.2f).SetRelative().SetLink(gameObject);
                }

                Instantiate(burret, firePoint.position, firePoint.rotation);
            }


            if (audioSource != null)
            {
                audioSource.PlayOneShot(audioSource.clip);
            }

            nextAttackTime = Time.time + 1.0f / attackRate;
        }
    }
}

private IEnumerator Shoot(Transform point, Vector2 direction)
{
    RaycastHit2D hitInfo = Physics2D.Raycast(point.position, direction);

    if (hitInfo && hitInfo.distance < 100.0f)
    {
        //Instantiate(obstaclesCrushEffect, hitInfo.point, Quaternion.identity);

        lineRenderer.SetPosition(0, point.position);
        lineRenderer.SetPosition(1, hitInfo.point);
    }
    else
    {
        lineRenderer.SetPosition(0, point.position);
        lineRenderer.SetPosition(1, (Vector2)point.position + direction * 99.0f);
    }

    lineRenderer.enabled = true;

    yield return new WaitForSeconds(0.02f);

    lineRenderer.enabled = false;
}  daichi changed*/

/* private IEnumerator Shoot(Transform point, Vector2 direction)
{
    RaycastHit2D hitInfo = Physics2D.Raycast(point.position, direction);

    if (hitInfo && hitInfo.distance < 100.0f)
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
        }#1#
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

        if (bushi != null)
        {
            bushi.TakeDamage(damage);
        }

        if (builderDestroy != null)
        {
            builderDestroy.TakeDamage(damage);
        }

        Instantiate(obstaclesCrushEffect, hitInfo.point, Quaternion.identity);

        lineRenderer.SetPosition(0, point.position);
        lineRenderer.SetPosition(1, hitInfo.point);
    }
    else
    {
        lineRenderer.SetPosition(0, point.position);
        lineRenderer.SetPosition(1, (Vector2)point.position + direction * 99.0f);
    }

    lineRenderer.enabled = true;

    yield return new WaitForSeconds(0.02f);

    lineRenderer.enabled = false;
}*/

/*
//public bool isAttacking = false;

#region
[SerializeField]
private Transform firePoint;
[SerializeField]
private Transform firePointUp;
[SerializeField]
private Transform firePointDown;
[SerializeField]
private int damage = 2;
[SerializeField]
private LineRenderer lineRenderer;
#endregion

public GameObject obstaclesCrushEffect;
public AudioClip attacking;
AudioSource audioSource;
private int num;

private void Start()
{
    audioSource = GetComponent<AudioSource>();
}

private void Update()
{
    CrusherController CC = this.gameObject.GetComponent<CrusherController>();


    if (Input.GetButtonDown("Fire1") && CC.AttackOk == true)
    {
        if(audioSource != null)
        {
            audioSource.PlayOneShot(attacking);
        }
        bool isPressingUp = Input.GetAxisRaw("Vertical") > 0;
        bool isPressingDown = Input.GetAxisRaw("Vertical") < 0;

        if (isPressingUp)
        {
            num = 2;
            StartCoroutine(Shoot(Vector2.up, firePointUp));
        }
        else if (isPressingDown)
        {
            num = 3;
            StartCoroutine(Shoot(Vector2.down, firePointDown));
        }
        else
        {
            num = 1;
            //Debug.Log("transform.rotation.y = " + transform.rotation.y);
            if (transform.rotation.y > -0.5f)
                StartCoroutine(Shoot(Vector2.right, firePoint));
            else
                StartCoroutine(Shoot(Vector2.left, firePoint));
        }
    }
}

IEnumerator Shoot(Vector2 direction, Transform point)
{
    RaycastHit2D[] hitInfos = Physics2D.RaycastAll(point.position, direction);

    bool hitObstacle = false;

    foreach (RaycastHit2D hitInfo in hitInfos)
    {
        if (!hitInfo.collider.isTrigger)
        {
            WoodBox woodbox = hitInfo.transform.GetComponent<WoodBox>();
            Canon canon = hitInfo.transform.GetComponent<Canon>();
            Spike spike = hitInfo.transform.GetComponent<Spike>();
            Rose rose = hitInfo.transform.GetComponent<Rose>();
            Wolf wolf = hitInfo.transform.GetComponent<Wolf>();
            Brick brick = hitInfo.transform.GetComponent<Brick>();
            CrusherGoal crusherGoal = hitInfo.transform.GetComponent<CrusherGoal>();

            if (woodbox != null)
            {
                woodbox.TakeDamage(damage);
                Debug.Log("wood");
            }
            if (canon != null)
            {
                canon.TakeDamage(damage);
            }
            if (spike != null)
            {
                spike.TakeDamage(damage);
                Debug.Log("spike");
            }
            if (rose != null)
            {
                rose.TakeDamage(damage);
            }
            if (wolf != null)
            {
                wolf.TakeDamage(damage);
            }
            if (brick != null)
            {
                brick.TakeDamage(damage);
            }
            if (crusherGoal != null)
            {
                crusherGoal.TakeDamage(damage);
            }

            Instantiate(obstaclesCrushEffect, hitInfo.point, Quaternion.identity);

            lineRenderer.SetPosition(0, point.position);
            lineRenderer.SetPosition(1, hitInfo.point);

            hitObstacle = true;
            break;
        }
    }

    if (!hitObstacle)
    {
        lineRenderer.SetPosition(0, point.position);
        lineRenderer.SetPosition(1, (Vector2)point.position + direction * 200);

    }


    lineRenderer.enabled = true;

    yield return new WaitForSeconds(0.02f);

    lineRenderer.enabled = false;
}*/