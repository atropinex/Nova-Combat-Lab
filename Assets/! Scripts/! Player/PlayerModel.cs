using System;
using UnityEngine;

[Serializable]

public class PlayerModel
{
    private readonly PlayerConfigSO config;

    public PlayerModel(PlayerConfigSO config)
    {
        this.config = config;
    }

    public float PlayerSpeed => config.PlayerSpeed;
    public float PlayerAccelerationSpeed => config.PlayerAccelerationSpeed;
    public float PlayerDecelerationSpeed => config.PlayerDecelerationSpeed;
    public float PlayerRotationSpeed => config.PlayerRotationSpeed;
    
}