using Game.Cameras.Models;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

public class CharacterAiming : MonoBehaviour, ITickable
{
    [Inject] private CameraModel CameraModel { get; }

    [SerializeField] private float _turnSpeed;

    [SerializeField] private Rig _aimRig;
    [SerializeField] private float _aimDuration;

    [SerializeField] private RaycastWeapon _raycastWeapon;

    public void Tick()
    {
        float yawCamera = CameraModel.GetMainCamera().transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.Euler(0, yawCamera, 0),
            _turnSpeed * Time.deltaTime);

        if (Input.GetButton("Fire2"))
        {
            _aimRig.weight += Time.deltaTime / _aimDuration;
        }
        else
        {
            _aimRig.weight -= Time.deltaTime / _aimDuration;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            _raycastWeapon.StartFiring();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            _raycastWeapon.StopFiring();
        }
    }
}
