using UnityEngine;
using System.Collections;

public class RFX4_WindCurves : MonoBehaviour
{
    public AnimationCurve WindCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public float GraphTimeMultiplier = 1, GraphIntensityMultiplier = 1;
    public bool IsLoop;

    private bool canUpdate;
    private float startTime;
    private WindZone windZone;

    private void Awake()
    {
        windZone = GetComponent<WindZone>();
        windZone.windMain = WindCurve.Evaluate(0);
    }

    private void OnEnable()
    {
        startTime = Time.time;
        canUpdate = true;
    }

    private void Update()
    {
        var time = Time.time - startTime;
        if (canUpdate) {
            var eval = WindCurve.Evaluate(time / GraphTimeMultiplier) * GraphIntensityMultiplier;
            windZone.windMain = eval;
        }
        if (time >= GraphTimeMultiplier) {
            if (IsLoop) startTime = Time.time;
            else canUpdate = false;
        }
    }
}