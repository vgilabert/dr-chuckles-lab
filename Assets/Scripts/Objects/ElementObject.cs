using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Element", menuName = "Objects/ElementObject", order = 1)]
public class ElementObject : ScriptableObject
{
    public ElementType elementType;
    public string elementName;
    public string description;
}

public enum ElementType
{
    Magical,
    Ordinary,
    Special
}