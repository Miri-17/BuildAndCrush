using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour 
{
    [HideInInspector] public static GameDirector Instance { get; private set; }
    public int crusherIndex = 0;
    public int builderIndex = 0;
    public int crusherScore = 0;
    public int builderScore = 0;
    public int wagonCrushCounts = 0;
    public int crusherKillCounts = 0;
    public float timeLimit = 600.0f;
    public float crusherPosition;
    public float builderPosition;
    public float currentFill = 0.0f;
    public bool crusherWin = true;
    public bool builderWin = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
