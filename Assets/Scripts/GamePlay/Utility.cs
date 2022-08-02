using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    // Start is called before the first frame update
    public static float Vec2Angle(Vector3 vector) {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }
    public static Vector3 Angle2Vec(float angleDeg) {
        float angle = angleDeg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0).normalized;
    }
    public static Sprite LoadSprite(string path)
    {
        Texture2D tex = Resources.Load(path) as Texture2D;
        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        return sprite;
    }
    public static Sprite LoadSprite(string path, int pixelsPerUnit)
    {
        Texture2D tex = Resources.Load(path) as Texture2D;
        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), pixelsPerUnit);
        return sprite;
    }
}
