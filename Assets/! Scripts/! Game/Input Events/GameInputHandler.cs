using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class GameInputHandler : MonoBehaviour
{
    [Header("Player Inputs")]
    [SerializeField] private InputActionReference playerControls;
    
    [Header("Camera Inputs")]
    [SerializeField] private InputActionReference cameraInputs;
}
