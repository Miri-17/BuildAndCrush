using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CrusherDatabase : ScriptableObject
{
    public Crusher[] crusher;

    public int CrusherCount
    {
        get
        {
            return crusher.Length;
        }
    }

    public Crusher GetCrusher(int index)
    {
        return crusher[index];
    }
}
