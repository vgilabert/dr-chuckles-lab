using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class CrockPot : MonoBehaviour
{
    private List<Element> elements;
    private bool isFull = false;
    
    private bool hasMagical = false;
    private bool hasOrdinary = false;
    private bool hasSpecial = false;

    void Start()
    {
        elements = new List<Element>();
    }

    private void OnTriggerStay(Collider other)
    {
        Element element = other.GetComponent<Element>();
        GrabObject grabObject = other.GetComponent<GrabObject>();
        if(element != null)
        {
            if (elements.Contains(element))
            {
                return;
            }

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
                            elements.Add(element);
                        }
                        break;
                    case ElementType.Ordinary:
                        if(hasOrdinary == false)
                        {
                            hasOrdinary = true;
                            isValid = true;
                            elements.Add(element);
                        }
                        break;
                    case ElementType.Special:
                        if(hasSpecial == false)
                        {
                            hasSpecial = true;
                            isValid = true;
                            elements.Add(element);
                        }
                        break;
                }

                if (isValid)
                {
                    grabObject.isInPot = true;
                    grabObject.isGrabable = false;
                }
                else grabObject.Respawn();
            }

            if(hasSpecial && hasMagical && hasOrdinary)
            {
                isFull = true;
                CheckIngredients();
            }
        }
    }

    private void CheckIngredients()
    {
        foreach(Element element in elements)
        {
            //if(ingredient.element != element)
            //{

            //}
        }
    }
}
