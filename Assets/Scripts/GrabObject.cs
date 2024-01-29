using System;
using UnityEngine;
using UnityCore.Audio;

public class GrabObject : MonoBehaviour
{
    private GrabManager grabManager;
    private Rigidbody _rigidbody;

    public bool isGrabable = true;
    public bool isPotion = false;
    public bool isGrabbed;
    public bool isOutOfHolder;
    public bool isInPot;

    private bool teleportedToPot = false;
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
        }
        else
        {
            _rigidbody.AddForce(Vector3.down * 20);
        }
    }
    
    public void Respawn()
    {
        if (!isPotion)
        {
            holderReference.Respawn(this);
            if(transform.position.y < 1 || (isOutOfHolder && !isInPot && !isGrabbed))
            {
                DialogueManager.Instance.TriggerDialogue(DialogueType.IngredientDropped);
            }
        }
        else
        {
            transform.position = GameObject.Find("PotionHolder").transform.position;
            _rigidbody.velocity = Vector3.zero;

            DialogueManager.Instance.TriggerDialogue(DialogueType.PotionDropped);
            GameManager.Instance.TriggerPotionTrhow(true);
        }


        isInPot = false;
        isOutOfHolder = false;
        isGrabbed = false;
        isGrabable = true;
        teleportedToPot = false;
    }

    void CheckPosition()
    {
        if (!isPotion)
        {
            isOutOfHolder = !holderReference.containerCheckCollider.bounds.Contains(transform.position);
            if (isOutOfHolder && !isGrabbed && !isInPot)
            {
                liveTimer += Time.deltaTime;
            }
        }

        if (transform.position.y <= 2f)
        {
            liveTimer += Time.deltaTime;
        }

        if (liveTimer >= GrabManager.Instance.timeToLive)
        {
            Respawn();
            liveTimer = 0;
        }

        if (isInPot && !teleportedToPot)
        {
            transform.position = GameObject.Find("Pot").transform.position + Vector3.up * .5f;
            _rigidbody.velocity = Vector3.zero;
            teleportedToPot = true;
        }

    }

    void OnMouseDown()
    {
        if (isGrabable)
        {
            AudioController.Instance.PlayAudio(UnityCore.Audio.AudioType.SFX_Grab, false, 0f, -.7f);
            isGrabbed = true;
            _rigidbody.drag = grabManager.drag;
        }
    }
    
    void OnMouseUp()
    {
        isGrabbed = false;
        _rigidbody.drag = GrabManager.Instance.defaultGrab;
        AudioController.Instance.PlayAudio(UnityCore.Audio.AudioType.SFX_Ungrab, false, 0f, -.9f);

        var releaseForce = EasingFunction.GetEasingFunction(grabManager.easeType)(
            0, 
            grabManager.releaseUpForce,
            MathF.Min(_rigidbody.velocity.magnitude, grabManager.maxForceMagnitude) / grabManager.maxForceMagnitude
        );
        _rigidbody.AddForce(Vector3.up * releaseForce);
    }
}