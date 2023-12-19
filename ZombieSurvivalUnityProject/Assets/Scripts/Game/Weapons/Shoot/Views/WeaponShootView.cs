using Core.Installers;
using UnityEngine;
using Zenject;

namespace Game.Weapons.Shoot.Views
{
    public class WeaponShootView : MonoBehaviour
    {
        [Inject(Id = BindingIdentifiers.MuzzleFleshPS)] private ParticleSystem MuzzleFlash { get; }
        [Inject(Id = BindingIdentifiers.ShotHitEffectPS)] private ParticleSystem ShotHitEffect { get; }

        public void EmitFireFlash()
        {
            MuzzleFlash.Emit(1);
        }

        public void EmitShotEffect(Vector3 position, Vector3 normal)
        {
            var hitEffectTransform = ShotHitEffect.transform;
            hitEffectTransform.position = position;
            hitEffectTransform.forward = normal;
            
            ShotHitEffect.Emit(1);
        }
    }
}
