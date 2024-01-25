using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Element", menuName = "Objects/ElementObject", order = 1)]
public class ElementObject : ScriptableObject
{
    public string elementName;
    public string description;

    public GameObject prefab;
}
