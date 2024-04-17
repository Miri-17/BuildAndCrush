using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEffectHeart : MonoBehaviour
{
    [SerializeField] private GameObject queenOfHarts;
    private QueenOfHeartsAttack _queenOfHeartsAttack;
    // Start is called before the first frame update
    void Start()
    {
        _queenOfHeartsAttack = queenOfHarts.GetComponent<QueenOfHeartsAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_queenOfHeartsAttack.isCharge)
        {
            Destroy(gameObject);
        }
    }
}
