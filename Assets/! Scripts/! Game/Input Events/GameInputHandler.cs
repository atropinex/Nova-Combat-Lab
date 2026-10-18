using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class GameInputHandler : MonoBehaviour
{
    [Header("Player Inputs")]
    [SerializeField] private InputActionReference playerMove;
    [SerializeField] private InputActionReference playerJump;
    [SerializeField] private InputActionReference playerSprint;
    [SerializeField] private InputActionReference playerStrafe;
    [SerializeField] private InputActionReference playerToggleWalkRun;
    
    [Header("Camera Inputs")]
    [SerializeField] private InputActionReference cameraControls;
    
    
    private PlayerInputHandler playerInputHandler;

    private void Awake()
    {
        this.playerInputHandler = new PlayerInputHandler();
    }


    private void OnEnable()
    {
        this.Subscribe(this.playerMove, this.playerInputHandler.MovePlayer);
    }

    private void OnDisable()
    {
        this.UnSubscribe(this.playerMove, this.playerInputHandler.MovePlayer);
        
    }

    private void Subscribe(InputActionReference reference, System.Action<InputAction.CallbackContext> callback)
    {
        if (reference == null || reference.action == null) { return; }
        reference.action.performed += callback;
        reference.action.canceled += callback;
        reference.action.Enable();
    }
    
    private void UnSubscribe(InputActionReference reference, System.Action<InputAction.CallbackContext> callback)
    {
        if (reference == null || reference.action == null) { return; }
        reference.action.performed -= callback;
        reference.action.canceled -= callback;
        reference.action.Disable();
    }
}
