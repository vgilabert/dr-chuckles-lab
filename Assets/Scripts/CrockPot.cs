using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class CrockPot : MonoBehaviour
{
    private ElementObject[] ingredients;
    private bool isFull = false;

    void Start()
    {
        ingredients = new ElementObject[3];
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<GrabObject>() != null)
        {
            if(other.GetComponent<GrabObject>().isGrabbed == false && !isFull)
            {
                // TODO: add the right ingredient to the crock pot
                Destroy(other.gameObject, 1f);
            }

            if(ingredients.All(x => x != null))
            {
                isFull = true;
                CheckIngredients();
            }
        }
    }

    private void CheckIngredients()
    {
        foreach(ElementObject ingredient in ingredients)
        {
            //if(ingredient.element != element)
            //{

            //}
        }
    }
}
