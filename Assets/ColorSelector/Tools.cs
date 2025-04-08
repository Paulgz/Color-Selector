using UnityEngine;

public static class Tools
{
    public const float twoPi = Mathf.PI*2.0f;

    public static Canvas getCanvas(Transform trans)
    {
        for(Transform t = trans; t!=null; t = t.parent) {
            Canvas canvas = t.GetComponent<Canvas>();
            if((canvas!=null)) {
                return canvas;
            }
        }
        return null;
    }
    public static Canvas getCanvas(RectTransform rt)
    {
        return getCanvas(rt.transform);
    }
    public static float fixAngleDeg(float _angle)
    {
        return specialMod(_angle, 360.0f);
    }
    public static float fixAngleRad(float _angle)
    {
        return specialMod(_angle, twoPi);
    }
    public static float specialMod(float value, float period)
    {
        if(value >= 0) {
            return value % period;
        } else {
            float v = (-value) % period;
            return period - v;
        }
    }
    public static Vector2 getPixelSize(RectTransform container)
    {
        Rect rect = RectTransformUtility.PixelAdjustRect(container, getCanvas(container.transform));
        return new(rect.width, rect.height);
    }
}
