using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementHolder : MonoBehaviour
{
    public ElementObject element;
    public Vector3 spawnOffset;
    public int elementCount;

    // Start is called before the first frame update
    void Start()
    {
        if (element.prefab != null)
        {
            for (int i = 0; i < elementCount; i++)
            {
                Instantiate(element.prefab, transform.position + new Vector3(0, i * 0.1f), Quaternion.identity, transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + spawnOffset, 0.2f);
    }
}
