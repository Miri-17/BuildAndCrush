using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuilderDatabase : ScriptableObject
{
    public Builder[] builder;

    public int BuilderCount
    {
        get
        {
            return builder.Length;
        }
    }

    public Builder GetBuilder(int index)
    {
        return builder[index];
    }
}
