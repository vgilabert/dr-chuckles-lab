using System.Collections;
using System.Collections.Generic;
using UnityCore.Audio;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GrabObject))]
public class Potion : MonoBehaviour
{
    public PotionObject potion;

    private void Start()
    {
        Destroy(Instantiate(potion.spawnEffect, transform.position, Quaternion.identity), 1.5f);
        AudioController.Instance.PlayAudio(potion.spawnSoundFX);
    }

    public void Explode()
    {
        AudioController.Instance.PlayAudio(potion.explodeSoundFX);

        Vector3 explosionPositon = Vector3.zero;
        if (potion.isGroundVFX)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 20f))
            {
                explosionPositon = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
        }
        else
            explosionPositon = transform.position;

        Destroy(Instantiate(potion.explodeVFX, explosionPositon, Quaternion.identity), potion.effectDuration);
        Destroy(this);
    }
}

