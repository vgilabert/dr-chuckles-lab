using System.Collections;
using System.Collections.Generic;
using UnityCore.Audio;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ExplosionController : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Potion obj = other.GetComponent<Potion>();
        if(obj!=null)
        {
            obj.Explode();
            Destroy(obj.gameObject);
        }
    }

}
