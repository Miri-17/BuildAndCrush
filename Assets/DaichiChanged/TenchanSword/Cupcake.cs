using System;
using System.Collections;
// using System.Collections.Generic;
// using System.Runtime.CompilerServices;
// using Cysharp.Threading.Tasks;
// using DG.Tweening;
using UnityEngine;

public class Cupcake : MonoBehaviour
{
    [SerializeField] private int damage = 3;
    [SerializeField] private GameObject obstaclesCrushEffect;

    [SerializeField] private float shotSpeed = 200f;
    [SerializeField] private float delayDestroy;

    private async void Start()
    {
        // await UniTask.Delay(TimeSpan.FromSeconds(delayDestroy)); // エラー内容を私が見て追加しましたが、解決に至りませんでした。
        // if (this != null)
        // {
        StartCoroutine("DestroyCupcake", delayDestroy);
        // await UniTask.Delay(TimeSpan.FromSeconds(delayDestroy), cancellationToken: destroyCancellationToken);
        // Destroy(gameObject);
    }

    private void Update()
    {
        // StartCoroutine("DestroyCupcake", delayDestroy);
        //transform.DOMoveY(-10, 2f).SetLink(gameObject).SetRelative();
        transform.Translate(shotSpeed * Time.deltaTime, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //WoodBox woodbox = hitInfo.transform.GetComponent<WoodBox>();
        Brick brick = other.transform.GetComponent<Brick>();
        Spike spike = other.transform.GetComponent<Spike>();
        Rose rose = other.transform.GetComponent<Rose>();
        Canon canon = other.transform.GetComponent<Canon>();
        Wolf wolf = other.transform.GetComponent<Wolf>();
        //daichi changed
        Halberd halberd = other.transform.GetComponent<Halberd>();
        Pig pig = other.transform.GetComponent<Pig>();
        PartsDestroy partsDestroy = other.transform.GetComponent<PartsDestroy>();
        Bushi bushi = other.transform.GetComponent<Bushi>();
        BuilderDestroy builderDestroy = other.transform.GetComponent<BuilderDestroy>();

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
        if (partsDestroy != null)
        {
            partsDestroy.TakeDamage(damage);
        }
        
        if (halberd != null)
        {
            halberd.TakeDamage(damage);
        }

        if (pig != null)
        {
            pig.TakeDamage(damage);
        }
        if (bushi != null)
        {
            bushi.TakeDamage(damage);
        }
        if (builderDestroy != null)
        {
            builderDestroy.TakeDamage(damage);
        }


        if (!other.collider.CompareTag("Crusher"))
        {
            Instantiate(obstaclesCrushEffect, other.transform.position, Quaternion.identity);
            Destroy(gameObject); //当たったときに消す
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        /*//WoodBox woodbox = hitInfo.transform.GetComponent<WoodBox>();
        Brick brick = other.transform.GetComponent<Brick>();
        Spike spike = other.transform.GetComponent<Spike>();
        Rose rose = other.transform.GetComponent<Rose>();
        Canon canon = other.transform.GetComponent<Canon>();
        Wolf wolf = other.transform.GetComponent<Wolf>();
        //daichi changed
        Halberd halberd = other.transform.GetComponent<Halberd>();
        Pig pig = other.transform.GetComponent<Pig>();
        PartsDestroy partsDestroy = other.transform.GetComponent<PartsDestroy>();

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

        Instantiate(obstaclesCrushEffect, other.transform.position, Quaternion.identity);

        if (!other.CompareTag("Crusher"))
        {
            Destroy(gameObject); //当たったときに消す
        }*/
    }

    private IEnumerator DestroyCupcake(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}