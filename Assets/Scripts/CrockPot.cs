using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrockPot : MonoBehaviour
{

    public ElementObject element;
    public Vector3 spawnOffset;
    public float overlapSphereRadius = 0.5f;

    private ElementObject[] ingredients;
    private bool isFull = false;

    void Start()
    {
        ingredients = new ElementObject[3];
    }

    void Update()
    {
        CheckElementObjectPosition();
    }

    private void CheckElementObjectPosition()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ElementObject>() != null)
        {
            if(other.GetComponent<GrabObject>().isGrabbed == false && !isFull)
            {
                ingredients.Append(other.GetComponent<ElementObject>());
            }

            if(ingredients.Length == 3)
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

    private void Respawn()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + spawnOffset, 0.2f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + spawnOffset, overlapSphereRadius);
    }


}
