using System;
using UnityEngine;
using UnityCore.Audio;

public class GrabObject : MonoBehaviour
{
    private GrabManager grabManager;

    
    private Rigidbody _rigidbody;
    public bool isGrabbed;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        grabManager = GrabManager.Instance;
    }

    private void Update()
    {
        UpdatePosition();
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

    void OnMouseDown()
    {
        isGrabbed = true;
        AudioController.Instance.PlayAudio(UnityCore.Audio.AudioType.SFX_Grab);
    }
    
    void OnMouseUp()
    {
        isGrabbed = false;
        AudioController.Instance.PlayAudio(UnityCore.Audio.AudioType.SFX_Ungrab);

        var releaseForce = EasingFunction.GetEasingFunction(grabManager.easeType)(
            0, 
            grabManager.releaseUpForce,
            MathF.Min(_rigidbody.velocity.magnitude, grabManager.maxForceMagnitude) / grabManager.maxForceMagnitude
        );
        _rigidbody.AddForce(Vector3.up * releaseForce);
    }
}