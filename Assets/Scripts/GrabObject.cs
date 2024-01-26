using System;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    private GrabManager grabManager;
    private Rigidbody _rigidbody;
    
    public bool isGrabbed;
    public bool isOutOfHolder;
    
    private float liveTimer;
    private ElementHolder holderReference;
    
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        grabManager = GrabManager.Instance;
        holderReference = GetComponentInParent<ElementHolder>();
        
        liveTimer = 0;
    }

    private void Update()
    {
        UpdatePosition();
        CheckPosition();
    }

    private void UpdatePosition()
    {
        if (isGrabbed)
        {
            Vector3 direction = grabManager.mousePosOnPlane - transform.position;
            _rigidbody.AddForce(direction * grabManager.force);
            _rigidbody.drag = grabManager.drag;
        }
        else
        {
            _rigidbody.drag = 0;
        }
    }
    
    public void Respawn()
    {
        holderReference.Respawn(this);
    }

    void CheckPosition()
    {
        isOutOfHolder = !holderReference.containerCheckCollider.bounds.Contains(transform.position);
        if (isOutOfHolder && !isGrabbed)
        {
            liveTimer += Time.deltaTime;
            if (liveTimer >= GrabManager.Instance.timeToLive)
            {
                Respawn();
            }
        }
        else
        {
            liveTimer = 0;
        }
        if (transform.position.y < -8)
        {
            Respawn();
        }
    }

    void OnMouseDown()
    {
        isGrabbed = true;
    }
    
    void OnMouseUp()
    {
        isGrabbed = false;
        var releaseForce = EasingFunction.GetEasingFunction(grabManager.easeType)(
            0, 
            grabManager.releaseUpForce,
            MathF.Min(_rigidbody.velocity.magnitude, grabManager.maxForceMagnitude) / grabManager.maxForceMagnitude
        );
        _rigidbody.AddForce(Vector3.up * releaseForce);
    }
}