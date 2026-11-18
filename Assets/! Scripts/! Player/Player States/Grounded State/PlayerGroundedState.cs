public class PlayerGroundedState : StateBase<PlayerController>
{
    private PlayerIdleState _idleState;
    private PlayerWalkingState _walkingState;
    
    public PlayerGroundedState(PlayerController p, PlayerModel pmodel) : base(p)
    {
        this.state = UniversalStateEnum.STATE_GROUNDED;
        this.Owner.PlayerParentState = this.state;
        
        this._idleState = new PlayerIdleState(Owner);
        this._walkingState = new PlayerWalkingState(Owner);
        
        this.SubStateMachine.SetDefault(this._idleState);
        this.SubStateMachine.Add(this._idleState, this._walkingState,() => pmodel.HasMovementInput);
        this.SubStateMachine.Add(this._walkingState, this._idleState,() => !pmodel.HasMovementInput);
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
        this.Owner.FloatingCapsuleBase.Update();
    }
    
    public override void FixedUpdate()
    {
        this.Owner.FloatingCapsuleBase.FixedUpdate();
    }
}