using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAiming : MonoBehaviour
{
    [SerializeField] private float _turnSpeed;
    [SerializeField] private Camera _camera;

    [SerializeField] private Rig _aimRig;
    [SerializeField] private float _aimDuration;

    [SerializeField] private RaycastWeapon _raycastWeapon;

    private void Update()
    {
        float yawCamera = _camera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), _turnSpeed * Time.deltaTime);

        if (Input.GetButton("Fire2"))
        {
            _aimRig.weight += Time.deltaTime / _aimDuration;
        }
        else
        {
            _aimRig.weight -= Time.deltaTime / _aimDuration;
        }

        if (Input.GetButton("Fire1"))
        {
            _raycastWeapon.StartFiring();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            _raycastWeapon.StopFiring();
        }
    }
}
