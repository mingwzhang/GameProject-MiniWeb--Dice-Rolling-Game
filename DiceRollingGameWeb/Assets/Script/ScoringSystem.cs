using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
    public int currentScore = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
