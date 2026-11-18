using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(BoxCollider))]
public class PlayerView : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private CapsuleCollider _capsuleCollider;
    private BoxCollider _boxCollider;
    private Rigidbody _rigidbody;
    
    public CapsuleCollider Capsule
    {
        get => this._capsuleCollider;
        set => this._capsuleCollider = value;
    }

    public BoxCollider BoxCollider
    {
        get => this._boxCollider;
        set => this._boxCollider = value;
    }

    public Rigidbody Rigidbody
    {
        get => this._rigidbody;
        set => this._rigidbody = value;
    }

    public Camera PlayerCamera
    {
        get => this._camera;
        set => this._camera = value;
    }
    
    // ========== Player Information ========== //

    public Vector3 Position => this.transform.position;
    public Vector3 Forward => this.transform.forward;
    
    protected void Awake()
    {
        this._rigidbody = this.GetComponent<Rigidbody>();
        this._capsuleCollider = this.GetComponent<CapsuleCollider>();
        this._boxCollider = this.GetComponent<BoxCollider>();
    }

    public Vector3 GetHorizontalVelocity()
    {
        Vector3 velocity = this.Rigidbody.linearVelocity;
        velocity.y = 0f;

        return velocity;
    }

    public void ApplyVelocityChange(Vector3 velocityChange)
    {
        this.Rigidbody.AddForce(
            velocityChange,
            ForceMode.VelocityChange
        );
    }

    // ========== Rotation ========== //

    public float GetRotationY()
    {
        return this.Rigidbody.rotation.eulerAngles.y;
    }

    public void ApplyRotation(Quaternion rotation)
    {
        this.Rigidbody.MoveRotation(rotation);
    }
}