using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Objects/PotionObject", order = 1)]
public class PotionObject : ScriptableObject
{
    public string potionName;
    public string description;
    public string hint;

    public ElementObject[] ingredients;
    public GameObject resultObject;
}
