using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PositionRotation
{
    [SerializeField]
    public Vector3 position;

    [SerializeField]
    public Vector3 rotation;

    public PositionRotation(Vector3 position, Vector3 rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
    public PositionRotation(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation.eulerAngles;
    }

    public override string ToString()
    {
        return $"Position: {position}\n Rotation: {rotation}";
    }
}
