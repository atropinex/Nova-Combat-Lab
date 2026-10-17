using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerView))]
public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private PlayerConfigSO playerConfig;

    private PlayerModel _playerModel;
    public PlayerView _playerView { get; private set; }

    private StateMachineBase<PlayerController> machine;
    private StateBase<PlayerController> grounded;
    
    // ========== Player States ========== //
    private FloatingCapsuleBase _floatingCapsuleBase;
    
    
    // ========== State Access ========== //
    public FloatingCapsuleBase FloatingCapsuleBase => _floatingCapsuleBase;
    
    
    protected void Awake()
    {
        // ====== Setup Player Configs ====== //
        this._playerModel = new PlayerModel(this.playerConfig);
        this._playerView = GetComponent<PlayerView>();
        
        // ====== Build State Machine First ====== //
        BuildStateMachine();
        
        // ====== Other Configs ====== //
    }

    protected void Start()
    {
        this._floatingCapsuleBase = new FloatingCapsuleBase(this.playerConfig, this._playerView.Rigidbody, this._playerView.Capsule, this._playerView.transform);
    }

    protected void BuildStateMachine()
    {
        this.grounded = new GroundedState(this);
        this.machine = new StateMachineBase<PlayerController>();
        this.machine.SetDefault(this.grounded);
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