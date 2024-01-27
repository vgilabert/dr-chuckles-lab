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
        Destroy(Instantiate(potion.throwEffect, transform.position, Quaternion.identity), potion.effectDuration);
        Destroy(this);
    }
}

