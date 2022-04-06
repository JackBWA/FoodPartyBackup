using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomVector
{
    public Vector3 min;
    public Vector3 max;

    public Vector3 GetRandomVector()
    {
        return new Vector3(
            Random.Range(min.x, max.x),
            Random.Range(min.y, max.y),
            Random.Range(min.z, max.z)
        );
    }
}
