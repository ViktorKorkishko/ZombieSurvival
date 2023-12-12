
using UnityEngine;

public class CharacterAiming : MonoBehaviour
{
    [SerializeField] private float _turnSpeed;
    [SerializeField] private Camera _camera;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float yawCamera = _camera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), _turnSpeed * Time.deltaTime);
    }
}
