using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{

    public float rotationSpeed = 20f;
    public enum RotationAxis
    {
        NONE,
        X,
        Y,
        Z,
        XY,
        XZ,
        YZ,
        ALL
    }

    public RotationAxis rotationAxis;

    public bool inverted;

    private float xRot;
    private float yRot;
    private float zRot;

    private void Start()
    {
        switch (rotationAxis)
        {
            case RotationAxis.NONE:
                break;

            case RotationAxis.X:
                xRot = rotationSpeed;
                yRot = 0f;
                zRot = 0f;
                break;

            case RotationAxis.Y:
                xRot = 0f;
                yRot = rotationSpeed;
                zRot = 0f;
                break;

            case RotationAxis.Z:
                xRot = 0f;
                yRot = 0f;
                zRot = rotationSpeed;
                break;

            case RotationAxis.XY:
                xRot = rotationSpeed;
                yRot = rotationSpeed;
                zRot = 0f;
                break;

            case RotationAxis.XZ:
                xRot = rotationSpeed;
                yRot = 0f;
                zRot = rotationSpeed;
                break;

            case RotationAxis.YZ:
                xRot = 0f;
                yRot = rotationSpeed;
                zRot = rotationSpeed;
                break;

            case RotationAxis.ALL:
                xRot = rotationSpeed;
                yRot = rotationSpeed;
                zRot = rotationSpeed;
                break;
            default:
                enabled = false;
                break;
        }

        if (inverted)
        {
            xRot = -xRot;
            yRot = -yRot;
            zRot = -zRot;
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(xRot, yRot, zRot) * Time.deltaTime);
    }
}
