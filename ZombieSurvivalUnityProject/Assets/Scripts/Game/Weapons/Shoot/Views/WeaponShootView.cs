using Core.Installers;
using UnityEngine;
using Zenject;

namespace Game.Weapons.Shoot.Views
{
    public class WeaponShootView : MonoBehaviour
    {
        [Inject(Id = BindingIdentifiers.MuzzleFleshPS)] private ParticleSystem MuzzleFlash { get; }

        public void EmitFireFlash()
        {
            MuzzleFlash.Emit(1);
        }
    }
}
