using UnityEngine;

public class PlayerWalkingState : StateBase<PlayerController>
{
    public PlayerWalkingState(PlayerController p) : base(p)
    {
        this.state = UniversalStateEnum.STATE_WALKING;
    }
    
    protected override void OnEnter()
    {
        this.Owner.PlayerSubState = this.state;
        Debug.Log(this.state.ToString());
    }

    protected override void OnExit()
    {
        // Walking exit logic
    }

    protected override void OnUpdate()
    {
        Debug.Log(this.state.ToString());
    }

    protected override void OnFixedUpdate()
    {
        // Walking physics logic
        this.Owner.UpdatePlayerMovement();
    }
}
