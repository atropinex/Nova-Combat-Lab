using UnityEngine;

public class FloatingCapsuleBase
{
    private readonly IFloatingCapsuleModel _floatingCapsuleModel;
    
    private readonly Rigidbody _rigidbody;
    private readonly CapsuleCollider _collider;
    private readonly Transform _transform;
    
    private Vector3 _colliderLocalSpace;
    

    public FloatingCapsuleBase(IFloatingCapsuleModel model, Rigidbody rigidbody, CapsuleCollider collider, Transform transform)
    {
        this._floatingCapsuleModel = model;
        this._rigidbody = rigidbody;
        this._collider = collider;
        this._transform = transform;
    }

    public void Update()
    {
        SetColliderRadius(this._floatingCapsuleModel.CapsuleRadius);
        SetColliderHeight(this._floatingCapsuleModel.CapsuleHeight * (1f - this._floatingCapsuleModel.CapsuleStepHeight));
        RecenterCollider();
        
        this._colliderLocalSpace = this._collider.center;
        float colliderHeight = this._collider.height / 2f;
        if (colliderHeight < this._collider.radius)
        {
            SetColliderRadius(colliderHeight);
        }
    }
    
    public void FixedUpdate()
    {
        Vector3 colliderCenterSpace = this._collider.bounds.center;
        Ray downwardRay = new Ray(colliderCenterSpace, Vector3.down);
        // Draw Ray
        Debug.DrawRay(colliderCenterSpace, Vector3.down * this._floatingCapsuleModel.CapsuleFloatRaycast, Color.blue);
        if (Physics.Raycast(downwardRay, out RaycastHit hit, this._floatingCapsuleModel.CapsuleFloatRaycast,this._floatingCapsuleModel.CapsuleLayerMask, QueryTriggerInteraction.Ignore))
        {
            float distanceToFloatingPoint = this._colliderLocalSpace.y * this._transform.localScale.y - hit.distance;
            if (distanceToFloatingPoint == 0f) return;
            float floatAmount = distanceToFloatingPoint * this._floatingCapsuleModel.CapsuleFloatStrength - this.GetPlayerVelocity().y;
            Vector3 liftForce = new Vector3(0f, floatAmount, 0f);
            this._rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
        }
    }

    private Vector3 GetPlayerVelocity()
    {
        return new Vector3(0f, this._rigidbody.linearVelocity.y, 0f);
    }

    private float SetSlopeSpeedModifier(float slope)
    {
        float slopeSpeedModifier = this._floatingCapsuleModel.CapsuleAnimationCurve.Evaluate(slope);
        return slopeSpeedModifier;
    }

    #region Collider Height, Radius and Center
    private void SetColliderRadius(float getCharacterRadius) { this._collider.radius = getCharacterRadius; }
    private void SetColliderHeight(float getCharacterHeight) { this._collider.height = getCharacterHeight; }

    private void RecenterCollider()
    {
        float colliderHeight = this._floatingCapsuleModel.CapsuleHeight - this._collider.height;
        Vector3 center = new Vector3(0f, this._floatingCapsuleModel.CapsuleCenterY + (colliderHeight / 2f), 0f);
        this._collider.center = center;
    }
    #endregion
}
