using System;
using UnityEngine;

[Serializable]

public class PlayerModel
{
    private readonly PlayerConfigSO config;
    private UniversalStateEnum playerParentState;
    private UniversalStateEnum playerSubState;
    

    public PlayerModel(PlayerConfigSO config)
    {
        this.config = config;
    }

    public float PlayerSpeed => config.PlayerSpeed;
    public float PlayerAccelerationSpeed => config.PlayerAccelerationSpeed;
    public float PlayerDecelerationSpeed => config.PlayerDecelerationSpeed;
    public float PlayerRotationSpeed => config.PlayerRotationSpeed;
    

    public UniversalStateEnum PlayerParentState
    {
        get => playerParentState;
        set => playerParentState = value;
    }
    
    public UniversalStateEnum PlayerSubState
    {
        get => playerSubState;
        set => playerSubState = value;
    }
}