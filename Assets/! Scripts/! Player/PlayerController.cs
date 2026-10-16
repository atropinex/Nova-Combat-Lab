using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [Header("Speeds")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float airSpeed = 3f;
    public float swimSpeed = 2.5f;

    [Header("Jump")]
    public float jumpHeight = 1.5f;
    public float jumpBuffer = 0.15f;

    [Header("Ground Check")]
    public LayerMask groundMask = ~0;
    public float groundCheckDistance = 0.1f;
    
    protected StateMachineBase<PlayerController> machine;
    protected Rigidbody rb;
    protected CapsuleCollider capsule;
    
    public bool IsGrounded { get; private set; }
    public bool RunHeld { get; private set; }
    public Vector2 MoveInput { get; private set; }

    protected void Awake()
    {
        this.rb = GetComponent<Rigidbody>();
        this.capsule = GetComponent<CapsuleCollider>();
        
    }
}
