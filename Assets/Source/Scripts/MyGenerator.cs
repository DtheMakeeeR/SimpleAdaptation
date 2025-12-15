using System.Collections;
using UnityEngine;


public static class MyGenerator
{
    public static Vector2 GenerateDirection(Vector2 direction, float coeff)
    {
        Vector2 res;
        int sign = Random.value > 0.5 ? 1 : -1;
        res = Quaternion.Euler(0, 0, Random.Range(0, 180 * coeff) * sign) * direction;
        return res;
    }
    public static float GenerateSpeed(float speed, float coeff)
    {
        float res = Random.Range(speed - speed * coeff, speed + speed * coeff);
        return res;
    }
}

