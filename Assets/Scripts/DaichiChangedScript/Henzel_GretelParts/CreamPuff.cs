using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CreamPuff : MonoBehaviour
{
    [SerializeField] private Material creamMaterial;
    [SerializeField] private float creamFadeTime = 10.0f;
    
    private GameObject _creamEffect;
    private void Start()
    {
       //_creamEffect = GameObject.Find(creamEffectName);
       creamMaterial.DOFade(0, 0.1f);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Crusher"))
        {
            Destroy(gameObject);
        }
    }

    async void OnDestroy()
    {
        creamMaterial.DOFade(3, 0.1f);
        creamMaterial.DOFade(0, creamFadeTime);
        await UniTask.Delay(TimeSpan.FromSeconds(creamFadeTime));
    }
}
