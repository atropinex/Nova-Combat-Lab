using UnityEngine;

public struct PlayerMovementResult
{
    public Vector3 VelocityChange;
    public Quaternion TargetRotation;

    public float CurrentSpeed;
    public float CurrentRotation;
}