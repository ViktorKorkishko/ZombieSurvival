using Core.Installers.Ids;
using UnityEngine;
using Zenject;

public class RaycastWeapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private ParticleSystem _hitEffect;

    [SerializeField] private Transform _raycastOrigin;
    
    [Inject(Id = BindingIdentifiers.CrosshairTarget)] private Transform RaycastDestination { get; }
    
    public void StartFiring()
    {
        FireBullet();
    }

    private void FireBullet()
    {
        _muzzleFlash.gameObject.SetActive(true);

        Vector3 origin = _raycastOrigin.position;
        Vector3 direction = (RaycastDestination.position - origin).normalized;

        if (Physics.Raycast(origin, direction, out var raycastHit))
        {
            var hitEffectTransform = _hitEffect.transform;
            hitEffectTransform.position = raycastHit.point;
            hitEffectTransform.forward = raycastHit.normal;
            
            _hitEffect.Emit(1);
        }
    }

    public void StopFiring()
    {
        _muzzleFlash.gameObject.SetActive(false);
    }
}
