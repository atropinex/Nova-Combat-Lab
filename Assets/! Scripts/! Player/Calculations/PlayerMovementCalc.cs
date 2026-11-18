
using UnityEngine;

public class PlayerMovementCalc
{
    private readonly PlayerModel _playerModel;

    // ========== Rotation Data ========== //

    private Vector3 currentTargetRotation;
    private Vector3 dampedRotationVelocity;
    private Vector3 dampedRotationTime;

    private readonly Vector3 rotationTime = new Vector3(0f, 0.375f, 0f);

    private float currentPlayerRotation;

    // ========== Constructor ========== //

    public PlayerMovementCalc(PlayerModel playerModel)
    {
        this._playerModel = playerModel;
    }

    // ========== Movement Calculation ========== //

    public PlayerMovementResult Calculate(
        Vector3 currentVelocity,
        float currentRotation,
        float cameraRotation,
        float deltaTime)
    {
        // Movement direction
        Vector3 movementDirection = new Vector3(
            this._playerModel.MoveInput.x,
            0f,
            this._playerModel.MoveInput.y
        );

        movementDirection = Vector3.ClampMagnitude(movementDirection, 1f);

        bool hasMovementInput = movementDirection.sqrMagnitude > 0.0001f;

        // Current horizontal velocity
        Vector3 currentPlayerVelocity = currentVelocity;
        currentPlayerVelocity.y = 0f;

        // ========== Target Rotation ========== //

        float targetRotationAngle = this.currentTargetRotation.y;

        if (hasMovementInput)
        {
            targetRotationAngle = this.UpdateTargetRotation(
                movementDirection,
                cameraRotation
            );
        }

        // ========== Target Velocity ========== //

        Vector3 targetVelocity = Vector3.zero;

        if (hasMovementInput)
        {
            Vector3 targetRotationDirection =
                this.GetTargetRotationDirection(targetRotationAngle);

            float movementSpeed = this._playerModel.PlayerSpeed;

            targetVelocity = targetRotationDirection * movementSpeed;
        }

        // ========== Acceleration / Deceleration ========== //

        float acceleration = hasMovementInput
            ? this._playerModel.PlayerAccelerationSpeed
            : this._playerModel.PlayerDecelerationSpeed;

        Vector3 newVelocity = Vector3.Lerp(
            currentPlayerVelocity,
            targetVelocity,
            acceleration * deltaTime
        );

        Vector3 velocityChange = newVelocity - currentPlayerVelocity;

        // ========== Rotation Direction (Lean / Animation) ========== //

        float deltaAngle = Mathf.DeltaAngle(
            currentRotation,
            targetRotationAngle
        );

        float targetRotationDir = 0f;

        if (hasMovementInput)
        {
            targetRotationDir = Mathf.Clamp(
                deltaAngle / this._playerModel.PlayerMinRotationAngle,
                -this._playerModel.PlayerMinRotationValue,
                this._playerModel.PlayerMinRotationValue
            );
        }

        this.currentPlayerRotation = Mathf.MoveTowards(
            this.currentPlayerRotation,
            targetRotationDir,
            this._playerModel.PlayerRotationSpeed * deltaTime
        );

        if (Mathf.Abs(deltaAngle) < this._playerModel.PlayerMinRotationAngle ||
            !hasMovementInput)
        {
            this.currentPlayerRotation = 0f;
        }

        // ========== Smooth Rigidbody Rotation ========== //

        float smoothedYAngle = Mathf.SmoothDampAngle(
            currentRotation,
            this.currentTargetRotation.y,
            ref this.dampedRotationVelocity.y,
            this.rotationTime.y - this.dampedRotationTime.y,
            Mathf.Infinity,
            deltaTime
        );

        this.dampedRotationTime.y += deltaTime;

        // ========== Return Movement Result ========== //

        return new PlayerMovementResult
        {
            VelocityChange = velocityChange,
            TargetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f),
            CurrentSpeed = newVelocity.magnitude,
            CurrentRotation = this.currentPlayerRotation
        };
    }

    // ========== Target Rotation ========== //

    private float UpdateTargetRotation(Vector3 direction, float cameraRotation)
    {
        float directionAngle = Mathf.Atan2(
            direction.x,
            direction.z
        ) * Mathf.Rad2Deg;

        if (directionAngle < 0f)
        {
            directionAngle += 360f;
        }

        directionAngle += cameraRotation;
        directionAngle %= 360f;

        if (!Mathf.Approximately(
                directionAngle,
                this.currentTargetRotation.y))
        {
            this.currentTargetRotation.y = directionAngle;
            this.dampedRotationTime.y = 0f;
        }

        return directionAngle;
    }

    // ========== Target Movement Direction ========== //

    private Vector3 GetTargetRotationDirection(float angle)
    {
        return Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
    }
}
