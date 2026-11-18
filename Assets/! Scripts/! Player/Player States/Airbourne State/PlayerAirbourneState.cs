using UnityEngine;

public class PlayerAirbourneState : StateBase<PlayerController>
{
    public PlayerAirbourneState(PlayerController p, PlayerModel pmodel) : base(p)
    {
        this.state = UniversalStateEnum.STATE_AIRBOURNE;
        this.Owner.PlayerParentState = this.state;
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
        this.Owner.FloatingCapsuleBase.Update();
    }

    public override void FixedUpdate()
    {
        this.Owner.FloatingCapsuleBase.FixedUpdate();
    }

    
}
