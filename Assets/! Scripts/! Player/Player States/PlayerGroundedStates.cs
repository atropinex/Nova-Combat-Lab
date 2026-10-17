public class GroundedState : StateBase<PlayerController>
{
    public GroundedState(PlayerController p) : base(p) { }

    public override void Update()
    {
        Owner.FloatingCapsuleBase.Update();
    }
    
    public override void FixedUpdate()
    {
        Owner.FloatingCapsuleBase.FixedUpdate();
    }
}