using UnityEngine;

public class PlayerIdleState : StateBase<PlayerController>
{
    public PlayerIdleState(PlayerController p) : base(p)
    {
        this.state = UniversalStateEnum.STATE_IDLE;
    }
    
    protected override void OnEnter()
    {
        this.Owner.PlayerSubState = this.state;
        Debug.Log(this.state.ToString());
    }

    protected override void OnExit()
    {
        
    }

    protected override void OnUpdate()
    {
        Debug.Log(this.state.ToString());   
    }

    protected override void OnFixedUpdate()
    {
        this.Owner.UpdatePlayerMovement();
    }
}
