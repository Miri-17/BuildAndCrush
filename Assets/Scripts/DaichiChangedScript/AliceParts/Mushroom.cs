using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Mushroom : MonoBehaviour
{
  
    private Animator apearMushroom;

    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        apearMushroom = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Crusher"))
        {
            apearMushroom.SetBool("touched",true);
        }
    }
}
