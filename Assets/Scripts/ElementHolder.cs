using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityCore.Audio;
using UnityEngine;

public class ElementHolder : MonoBehaviour
{
    public ElementObject element;
    public Vector3 spawnOffset;
    public Collider containerCheckCollider;
    public float elementNb = 3; // how many elements can be in the holder at the same time
    
    private GrabObject _elementObject;

    [SerializeField]
    private GameObject respawnEffect;
    [SerializeField]
    private UnityCore.Audio.AudioType respawnSound;
    private bool isOutOfHolder;

    void Start()
    {
        if (element.prefab != null)
        {
            for (int i = 0; i < elementNb; i++)
            {
                GameObject elem = Instantiate(element.prefab, transform.position + spawnOffset, Quaternion.identity, transform);
                elem.GetComponent<Element>().element = element;
            }
        }
    }

    public void Respawn(GrabObject grabObject)
    {
        grabObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        grabObject.transform.position = transform.position + spawnOffset;

        AudioController.Instance.PlayAudio(respawnSound);
        Destroy(Instantiate(respawnEffect, transform.position, Quaternion.identity), 1f);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + spawnOffset, 0.2f);
    }
}
