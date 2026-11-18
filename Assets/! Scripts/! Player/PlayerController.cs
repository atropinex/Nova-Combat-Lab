using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerView))]
public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private PlayerConfigSO playerConfig;

    private PlayerModel _playerModel;
    private PlayerMovementCalc _playerMovementCalc;
    public PlayerView _playerView { get; private set; }

    private StateMachineBase<PlayerController> machine;
    private StateBase<PlayerController> grounded;
    private StateBase<PlayerController> airbourne;
    
    // ========== Player States ========== //
    private FloatingCapsuleBase _floatingCapsuleBase;
    
    // ========== State Access ========== //
    public FloatingCapsuleBase FloatingCapsuleBase => _floatingCapsuleBase;
    
    // ========== Getters State ========== //
    public UniversalStateEnum PlayerParentState
    {
        get => this._playerModel.PlayerParentState;
        set => this._playerModel.PlayerParentState = value;
    }

    public UniversalStateEnum PlayerSubState
    {
        get => this._playerModel.PlayerSubState;
        set => this._playerModel.PlayerSubState = value;
    }
    
    // ========== Game Related ========== //
    protected void Awake()
    {
        // ====== Setup Player Configs ====== //
        this._playerModel = new PlayerModel(this.playerConfig, this.moveAction);
        this._playerView = GetComponent<PlayerView>();
        this._playerMovementCalc = new PlayerMovementCalc(this._playerModel);
        
        // ====== Build State Machine First ====== //
        this.BuildStateMachine();
        
        // ====== Other Configs ====== //
    }

    protected void Start()
    {
        this._floatingCapsuleBase = new FloatingCapsuleBase(this.playerConfig, this._playerView.Rigidbody, this._playerView.Capsule, this._playerView.transform);
    }
    
    
    // ========== Others ========== //
    protected void BuildStateMachine()
    {
        this.machine = new StateMachineBase<PlayerController>();
        this.grounded = new PlayerGroundedState(this, this._playerModel);
        this.airbourne = new PlayerAirbourneState(this, this._playerModel);
        this.machine.SetDefault(this.grounded);
    }
    
    public void UpdatePlayerMovement()
    {
        Vector3 currentVelocity = this._playerView.GetHorizontalVelocity();

        PlayerMovementResult result = this._playerMovementCalc.Calculate(
            currentVelocity,
            this._playerView.GetRotationY(),
            this._playerView.PlayerCamera.transform.eulerAngles.y,
            Time.fixedDeltaTime
        );

        this._playerView.ApplyVelocityChange(result.VelocityChange);
        this._playerView.ApplyRotation(result.TargetRotation);
    }


    protected void OnEnable()
    {
        this.moveAction.action.Enable();
    }

    protected void OnDisable()
    {
        this.moveAction.action.Disable();
    }

    protected void OnDestroy()
    {
        this.machine.Stop();
    }

    protected void Update()
    {
        this.machine.Update();
    }

    protected void FixedUpdate()
    {
        this.machine.FixedUpdate();
    }
}