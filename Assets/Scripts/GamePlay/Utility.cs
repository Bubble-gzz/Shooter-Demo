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
        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
    }
}
