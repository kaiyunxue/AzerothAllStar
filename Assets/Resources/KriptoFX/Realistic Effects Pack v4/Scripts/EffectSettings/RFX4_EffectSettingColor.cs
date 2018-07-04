using UnityEngine;
using System.Collections;

public class RFX4_EffectSettingColor : MonoBehaviour
{
    public Color Color = Color.red;

    private Color previousColor;

    void OnEnable()
    {
        UpdateColor();
    }

    void Update()
    {
        if (previousColor != Color)
        {
            UpdateColor();
        }
    }

    private void UpdateColor()
    {
        var transformMotion = GetComponentInChildren<RFX4_TransformMotion>(true);
        var rayCastCollision = GetComponentInChildren<RFX4_RaycastCollision>(true);
        var hue = RFX4_ColorHelper.ColorToHSV(Color).H;
        RFX4_ColorHelper.ChangeObjectColorByHUE(gameObject, hue);
        if (transformMotion != null) transformMotion.HUE = hue;
        if (rayCastCollision != null) rayCastCollision.HUE = hue;
        previousColor = Color;
    }

}
