using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class CrockPot : MonoBehaviour
{
    private List<ElementObject> elements;
    private bool isFull = false;
    
    private bool hasMagical = false;
    private bool hasOrdinary = false;
    private bool hasSpecial = false;
    
    void Start()
    {
        elements = new List<ElementObject>();
    }

    private void OnTriggerStay(Collider other)
    {
        Element element = other.GetComponent<Element>();
        GrabObject grabObject = other.GetComponent<GrabObject>();
        if(element != null)
        {
            if(grabObject.isGrabbed == false && !isFull)
            {
                bool isValid = false;
                switch (element.element.elementType)
                {
                    case ElementType.Magical:
                        if(hasMagical == false)
                        {
                            hasMagical = true;
                            isValid = true;
                            elements.Add(element.element);
                        }
                        break;
                    case ElementType.Ordinary:
                        if(hasOrdinary == false)
                        {
                            hasOrdinary = true;
                            isValid = true;
                            elements.Add(element.element);
                        }
                        break;
                    case ElementType.Special:
                        if(hasSpecial == false)
                        {
                            hasSpecial = true;
                            isValid = true;
                            elements.Add(element.element);
                        }
                        break;
                }

                if (isValid)
                {
                    Destroy(other.gameObject, 1f);
                }
                else grabObject.Respawn();
            }

            if(elements.All(x => x != null))
            {
                isFull = true;
                CheckIngredients();
            }
        }
    }

    private void CheckIngredients()
    {
        foreach(ElementObject ingredient in elements)
        {
            //if(ingredient.element != element)
            //{

            //}
        }
    }
}
