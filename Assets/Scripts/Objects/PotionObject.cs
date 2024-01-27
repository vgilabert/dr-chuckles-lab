using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.Audio;

[CreateAssetMenu(fileName = "Potion", menuName = "Objects/PotionObject", order = 1)]
public class PotionObject : ScriptableObject
{
    public string potionName;
    public string description;
    public string hint;

    public List<string> reactionPhrases;

    public ElementObject[] ingredients;

    public UnityCore.Audio.AudioType spawnSoundFX;
    public UnityCore.Audio.AudioType explodeSoundFX;

    public GameObject resultObject;
    public GameObject spawnEffect;
    public GameObject explodeVFX;

    public bool isGroundVFX = false;

    public float effectDuration;

}
