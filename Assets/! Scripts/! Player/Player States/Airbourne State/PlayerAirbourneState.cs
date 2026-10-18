using UnityEngine;

public class PlayerAirbourneState : StateBase<PlayerController>
{
    public PlayerAirbourneState(PlayerController p) : base(p)
    {
        this.state = UniversalStateEnum.STATE_AIRBOURNE;
        Owner.PlayerParentState = this.state;
    }

    public override void Enter()
    {
        // Keep this Event on very top
        GameEvents.Emit(PlayerStateEnum.PLAYER_AIRBOURNE_ENTER);
        
        
    }

    public override void Exit()
    {
        // Keep this Event on very top
        GameEvents.Emit(PlayerStateEnum.PLAYER_AIRBOURNE_EXIT);
        
    }

    public override void Update()
    {
        Owner.FloatingCapsuleBase.Update();
    }

    public override void FixedUpdate()
    {
        Owner.FloatingCapsuleBase.FixedUpdate();
    }

    
}
