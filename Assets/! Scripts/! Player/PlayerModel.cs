using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]

public class PlayerModel
{
    private readonly PlayerConfigSO config;
    private readonly InputActionReference moveAction;
    private UniversalStateEnum playerParentState;
    private UniversalStateEnum playerSubState;
    

    public PlayerModel(PlayerConfigSO config, InputActionReference moveAction)
    {
        this.config = config;
        this.moveAction = moveAction;
    }
    
    // ======== Player Input ======== //
    public Vector3 MoveInput => this.moveAction.action.ReadValue<Vector2>();
    
    // ======== Player Movement ======== //
    public float PlayerSpeed => this.config.PlayerSpeed;
    public float PlayerAccelerationSpeed => this.config.PlayerAccelerationSpeed;
    public float PlayerDecelerationSpeed => this.config.PlayerDecelerationSpeed;
    public float PlayerMovementThreshold => this.config.PlayerMovementThreshold;
    public bool HasMovementInput => this.MoveInput.sqrMagnitude > this.config.PlayerMovementThreshold * this.config.PlayerMovementThreshold;

    
    // ======== Player Movement ======== //
    public float PlayerRotationSpeed => this.config.PlayerRotationSpeed;
    public float PlayerMinRotationAngle => this.config.PlayerMinRotationAngle;
    public float PlayerMaxRotationAngle => this.config.PlayerMaxRotationAngle;
    public float PlayerMinRotationValue => this.config.PlayerMinRotationValue;
    

    public UniversalStateEnum PlayerParentState
    {
        get => this.playerParentState;
        set => this.playerParentState = value;
    }
    
    public UniversalStateEnum PlayerSubState
    {
        get => this.playerSubState;
        set => this.playerSubState = value;
    }
}