public class PlayerGroundedState : StateBase<PlayerController>
{
    public PlayerGroundedState(PlayerController p) : base(p)
    {
        this.state = UniversalStateEnum.STATE_GROUNDED;
        Owner.PlayerParentState = this.state;
    }

    public override void Enter()
    {
        // Keep this Event on very top
        GameEvents.Emit(PlayerStateEnum.PLAYER_GROUNDED_ENTER);
        
    }

    public override void Exit()
    {
        // Keep this Event on very top
        GameEvents.Emit(PlayerStateEnum.PLAYER_GROUNDED_EXIT);
        
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