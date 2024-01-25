using System;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public float force = 20f;
    public float drag = 15f;

    private Vector3 mousePos;
    private Vector3 mousePosOnPlane;
    private Plane plane;
    
    private Rigidbody _rigidbody;
    private bool isGrabbed;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        plane = new Plane(Vector3.up, Vector3.up * 2);
    }

    private void Update()
    {
        GetMouseOnPlane();
        UpdatePosition();
    }

    private void GetMouseOnPlane()
    {
        mousePos = Input.mousePosition;
        
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        float distance;
        plane.Raycast(ray, out distance);
        mousePosOnPlane = ray.GetPoint(distance);
        
    }
    
    private void UpdatePosition()
    {
        if (isGrabbed)
        {
            Vector3 direction = mousePosOnPlane - transform.position;
            _rigidbody.AddForce(direction * force);
            _rigidbody.drag = drag;
        }
        else
        {
            _rigidbody.drag = 0;
        }
    }

    void OnMouseDown()
    {
        isGrabbed = true;
    }
    
    void OnMouseUp()
    {
        isGrabbed = false;
    }
}