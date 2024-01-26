using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityCore.Audio;
using UnityEngine;

public class ElementHolder : MonoBehaviour
{
    public GameObject elementPrefab;
    public Vector3 spawnOffset;
    public float spawnRadius;
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
        if (elementPrefab != null)
        {
            for (int i = 0; i < elementNb; i++)
            {
                Vector3 spawnOffset = new Vector3(
                    Random.Range(-spawnRadius, spawnRadius),
                    Random.Range(-spawnRadius, spawnRadius),
                    Random.Range(-spawnRadius, spawnRadius)
                );
                GameObject elem = Instantiate(elementPrefab, transform.position + spawnOffset, Quaternion.identity, transform);
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
        Gizmos.DrawWireSphere(transform.position + spawnOffset, spawnRadius);
    }
}
