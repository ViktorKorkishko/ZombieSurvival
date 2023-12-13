using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    [SerializeField] private bool _isFiring;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private ParticleSystem _hitEffect;

    [SerializeField] private Transform _raycastOrigin;
    [SerializeField] private Transform _raycastDestination;

    private Ray ray;
    private RaycastHit raycastHit;

    public void StartFiring()
    {
        _isFiring = true;
        FireBullet();
    }

    private void FireBullet()
    {
        _muzzleFlash.gameObject.SetActive(true);

        ray.origin = _raycastOrigin.position;
        ray.direction = (_raycastDestination.position - _raycastOrigin.position).normalized;

        if (Physics.Raycast(ray, out raycastHit))
        {
            Debug.DrawLine(ray.origin, raycastHit.point, Color.red, 1f);
            _hitEffect.transform.position = raycastHit.point;
            _hitEffect.transform.forward = raycastHit.normal;
            _hitEffect.Emit(1);
        }
    }

    public void StopFiring()
    {
        _isFiring = false;
        _muzzleFlash.gameObject.SetActive(false);
    }
}
