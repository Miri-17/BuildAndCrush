using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CanonBurret : MonoBehaviour
{
    [SerializeField] private float canonSpeed = 100f;
    // Start is called before the first frame update
    async void Start()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(5f));
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, canonSpeed * Time.deltaTime, 0);
    }
}