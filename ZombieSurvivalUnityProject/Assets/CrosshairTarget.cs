using UnityEngine;

public class CrosshairTarget : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float _radius;

    private Transform cameraTransform => camera.transform;

    private Ray ray;
    private RaycastHit raycastHit;

    private void Update()
    {
        ray.origin = cameraTransform.position;
        ray.direction = cameraTransform.forward;

        if (Physics.Raycast(ray, out raycastHit))
        {
            transform.position = raycastHit.point;
        }    
    }
}
