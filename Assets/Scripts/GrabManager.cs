using UnityCore.Audio;
using UnityEngine;
using UnityEngine.Serialization;

public class GrabManager : MonoBehaviour
{
    public float planeHeight = 2f;
    public float force = 20f;
    public float defaultGrab;

    public float drag = 15f;
    public float releaseUpForce = 300f;
    public float maxForceMagnitude = 10f;
    public EasingFunction.Ease easeType = EasingFunction.Ease.EaseOutCirc;
    public float timeToLive = 3f;
    
    public Vector3 mousePos;
    public Vector3 mousePosOnPlane;
    public Plane plane;
    
    // make this a singleton
    public static GrabManager Instance;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        plane = new Plane(Vector3.up, Vector3.up * planeHeight);
    }

    private void Update()
    {
        GetMouseOnPlane();
    }

    private void GetMouseOnPlane()
    {
        mousePos = Input.mousePosition;
        
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        float distance;
        plane.Raycast(ray, out distance);
        mousePosOnPlane = ray.GetPoint(distance);
        
    }
}
