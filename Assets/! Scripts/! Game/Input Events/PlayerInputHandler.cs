using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler
{
    public void MovePlayer(InputAction.CallbackContext context)
    {
        GameEvents.Emit(InputEventsEnum.PLAYER_MOVE, context.ReadValue<Vector2>());
    }
}
