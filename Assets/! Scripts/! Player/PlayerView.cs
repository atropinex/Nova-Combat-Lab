using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider),  typeof(BoxCollider))]
public class PlayerView : MonoBehaviour
{
    private CapsuleCollider _capsuleCollider;
    private BoxCollider _boxCollider;
    private Rigidbody _rigidbody;
    
    public CapsuleCollider Capsule { get => _capsuleCollider; set => _capsuleCollider = value; }
    public BoxCollider BoxCollider { get => _boxCollider; set => _boxCollider = value; }
    public Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }
    
    protected void Awake()
    {
        this._rigidbody = this.GetComponent<Rigidbody>();
        this._capsuleCollider = this.GetComponent<CapsuleCollider>();
        this._boxCollider = this.GetComponent<BoxCollider>();
    }
}