using UnityEngine;
[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Player/Player Config")]
public class PlayerConfigSO : ScriptableObject, IFloatingCapsuleModel
{
    // =========================
    // MOVEMENT
    // =========================
    [Header("Movement")]
    [SerializeField] private float playerSpeed = 2.5f;
    [SerializeField] private float playerAccelerationSpeed = 2.15f;
    [SerializeField] private float playerDecelerationSpeed = 1.85f;

    // =========================
    // ROTATION
    // =========================
    [Header("Rotation")]
    [SerializeField] private float playerRotationSpeed = 5f;
    [SerializeField] private float playerMinRotationAngle = 0.1f;
    [SerializeField] private float playerMaxRotationAngle = 90f;
    [SerializeField] private float playerMinRotationValue = 1.45f;

    // =========================
    // FLOATING CAPSULE
    // =========================
    [Header("Floating Capsule")]
    [SerializeField] private float capsuleHeight = 1.65f;
    [SerializeField] private float capsuleCenterY = 0.85f;
    [SerializeField] private float capsuleRadius = 0.16f;
    [SerializeField] private LayerMask capsuleLayerMask;
    
    [Header("Floating Strength")]
    [SerializeField, Range(0f, 1f)] private float capsuleStepHeight = 0.4f;
    [SerializeField, Range(0f, 5f)] private float capsuleFloatRaycast = 1.25f;
    [SerializeField, Range(0f, 50f)] private float capsuleFloatStrength = 12f;
    [SerializeField] private AnimationCurve capsuleAnimationCurve;

    // =========================
    // MOVEMENT PROPERTIES
    // =========================
    public float PlayerSpeed => playerSpeed;
    public float PlayerAccelerationSpeed => playerAccelerationSpeed;
    public float PlayerDecelerationSpeed => playerDecelerationSpeed;

    // =========================
    // ROTATION PROPERTIES
    // =========================
    public float PlayerRotationSpeed => playerRotationSpeed;
    public float PlayerMinRotationAngle => playerMinRotationAngle;
    public float PlayerMaxRotationAngle => playerMaxRotationAngle;
    public float PlayerMinRotationValue => playerMinRotationValue;

    // =========================
    // FLOATING CAPSULE PROPERTIES
    // =========================
    public float CapsuleHeight => capsuleHeight;
    public float CapsuleCenterY => capsuleCenterY;
    public float CapsuleRadius => capsuleRadius;
    public LayerMask CapsuleLayerMask => capsuleLayerMask;

    // =========================
    // FLOATING STRENGTH PROPERTIES
    // =========================
    public float CapsuleStepHeight => capsuleStepHeight;
    public float CapsuleFloatRaycast => capsuleFloatRaycast;
    public float CapsuleFloatStrength => capsuleFloatStrength;
    public AnimationCurve CapsuleAnimationCurve => capsuleAnimationCurve;
}

