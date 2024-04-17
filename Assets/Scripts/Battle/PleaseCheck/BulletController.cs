using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    /*#region
    [SerializeField]
    private float speed = 200f;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private int damage = 2;
    #endregion

    private void Start()
    {
        rb.velocity = transform.right * speed;
    }

    #region
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        WoodBox woodbox = hitInfo.GetComponent<WoodBox>();
        Canon canon = hitInfo.GetComponent<Canon>();
        Spike spike = hitInfo.GetComponent<Spike>();

        if (woodbox != null)
        {
            woodbox.TakeDamage(damage);
        }
        if (canon != null)
        {
            canon.TakeDamage(damage);
        }
        if (spike != null)
        {
            spike.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
    #endregion

    #region
    void OnBecameInvisible()
    {
        GameObject.Destroy(this.gameObject);
    }
    #endregion*/
}
