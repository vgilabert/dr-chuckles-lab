using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityCore.Audio;
using UnityEngine;

public class ElementHolder : MonoBehaviour
{
    public ElementObject element;
    public Vector3 spawnOffset;
    public float overlapSphereRadius = 0.5f;
    public float timeToLive = 5f; // how long the object can be out of the holder before it is destroyed
    private float liveTimer; // how long the object will take to respawn after it returns to the holder
    
    private GrabObject _elementObject;

    [SerializeField]
    private GameObject respawnEffect;
    [SerializeField]
    private UnityCore.Audio.AudioType respawnSound;
    private bool isOutOfHolder;

    void Start()
    {
        liveTimer = 0;
        if (element.prefab != null)
        {
            _elementObject = Instantiate(element.prefab, transform.position + spawnOffset, Quaternion.identity, transform);

        }
    }

    void Update()
    {
        CheckElementObjectPosition();
    }

    private void CheckElementObjectPosition()
    {
        isOutOfHolder = false;
        // Check if the element object is out of the holder with an overlap sphere
        Collider[] colliders = Physics.OverlapSphere(transform.position + spawnOffset, overlapSphereRadius);
        if (colliders.Any(col => col.gameObject == _elementObject.gameObject) == false)
        {
            isOutOfHolder = true;
        }
        if (isOutOfHolder && _elementObject.isGrabbed == false)
        {
            liveTimer += Time.deltaTime;
        }
        else
        {
            liveTimer = 0;
        }
        if (_elementObject.transform.position.y < - 5)
        {
            Respawn();
            DialogueManager.Instance.TriggerDialogue(DialogueType.IngredientDropped);
        }
        if (liveTimer > timeToLive)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        AudioController.Instance.PlayAudio(respawnSound);
        _elementObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _elementObject.transform.position = transform.position + spawnOffset;
        Destroy(Instantiate(respawnEffect, transform.position, Quaternion.identity), 1f);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + spawnOffset, 0.2f);
        // Draw a wire sphere to show the overlap sphere in red
        if (isOutOfHolder)
            Gizmos.color = Color.red;
        else 
            Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + spawnOffset, overlapSphereRadius);
    }
}
