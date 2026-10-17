using UnityEngine;

public interface IFloatingCapsuleModel
{
    float CapsuleHeight { get; }
    float CapsuleCenterY { get; }
    float CapsuleRadius { get; }
    LayerMask CapsuleLayerMask { get; }

    float CapsuleStepHeight { get; }
    float CapsuleFloatRaycast { get; }
    float CapsuleFloatStrength { get; }
    AnimationCurve CapsuleAnimationCurve { get; }
}