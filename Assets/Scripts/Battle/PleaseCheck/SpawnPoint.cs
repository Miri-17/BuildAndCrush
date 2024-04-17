using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    #region
    public int x;
    public int y;
    public bool isOccupied;
    #endregion

    #region
    [SerializeField]
    private Color greenColor;
    [SerializeField]
    private Color redColor;
    #endregion

    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();

        greenColor = new Color(0, 1, 0, 0.2f);
        redColor = new Color(1, 0, 0, 0.2f);
    }

    private void Update()
    {
        if (isOccupied == true)
        {
            rend.color = redColor;
            this.gameObject.layer = 10;
        }
        else
        {
            rend.color = greenColor;
            this.gameObject.layer = 9;
        }
    }
}
