using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    [SerializeField]
    float attractForce;
    private void Update()
    {
        if (rb != null)
        {
            var direction = rb.position - transform.position;
            rb.AddRelativeForce(direction.normalized * attractForce, ForceMode.Force);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Potion obj = other.GetComponent<Potion>();
        if (obj != null)
        {
            rb = obj.GetComponent<Rigidbody>();
        }
    }
}
