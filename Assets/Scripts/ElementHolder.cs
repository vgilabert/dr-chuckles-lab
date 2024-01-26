using UnityEngine;

public class ElementHolder : MonoBehaviour
{
    public ElementObject element;
    public Vector3 spawnOffset;
    public Collider containerCheckCollider;
    public float elementNb = 3; // how many elements can be in the holder at the same time
    
    void Start()
    {
        if (element.prefab != null)
        {
            for (int i = 0; i < elementNb; i++)
            {
                Instantiate(element.prefab, transform.position + spawnOffset, Quaternion.identity, transform);
            }
        }
    }

    public void Respawn(GrabObject grabObject)
    {
        grabObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        grabObject.transform.position = transform.position + spawnOffset;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + spawnOffset, 0.2f);
    }
}
