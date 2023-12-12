
using UnityEngine;

public class CharacterAiming : MonoBehaviour
{
    [SerializeField] private float _turnSpeed;
    [SerializeField] private Camera _camera;

    [SerializeField] private CursorLocker _cursorLocker;

    private void Update()
    {
        if (!_cursorLocker.Locked)
        {
            return;
        }

        float yawCamera = _camera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), _turnSpeed * Time.deltaTime);
    }
}
