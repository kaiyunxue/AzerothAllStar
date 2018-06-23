using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene{
    public static float timeScale;
    public static void Pause()
    {
        timeScale = 0.000000000000001f;
    }
    public static void Start()
    {
        timeScale = 1;
    }
}
