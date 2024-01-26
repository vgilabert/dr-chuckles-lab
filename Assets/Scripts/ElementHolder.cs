using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityCore.Audio;
using UnityEngine;

public class ElementHolder : MonoBehaviour
{
    public GameObject elementPrefab;
    public Collider containerCheckCollider;
    public List<Vector3> spawnPositions;
    
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
            for (int i = 0; i < spawnPositions.Count; i++)
            {
                Instantiate(elementPrefab, transform.position + spawnPositions[i], Quaternion.identity, transform);
            }
        }
    }

    public void Respawn(GrabObject grabObject)
    {
        grabObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        grabObject.transform.position = transform.position + spawnPositions[Random.Range(0, spawnPositions.Count)];

        AudioController.Instance.PlayAudio(respawnSound);
        Destroy(Instantiate(respawnEffect, transform.position, Quaternion.identity), 1f);
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < spawnPositions.Count; ++i)
        {
            Gizmos.DrawWireSphere(transform.position + spawnPositions[i], .1f);
        }
    }
}
