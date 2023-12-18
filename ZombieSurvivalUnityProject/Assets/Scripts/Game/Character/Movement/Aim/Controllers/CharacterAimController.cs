using Core.Installers;
using Game.Character.Movement.Aim.Models;
using UnityEngine.Animations.Rigging;
using Zenject;

namespace Game.Character.Movement.Aim.Controllers
{
    public class CharacterAimController : ITickable
    {
        [Inject] private CharacterAimModel CharacterAimModel { get; }
        [Inject(Id = BindingIdentifiers.CharacterAimRig)] private Rig AimRig { get; }

        [Inject] private RaycastWeapon RaycastWeapon { get; }

        private float AimDuration => CharacterAimModel.AimDuration;

        void ITickable.Tick()
        {
            
            
            // if (Input.GetButtonDown("Fire1"))
            // {
            //     RaycastWeapon.StartFiring();
            // }
            //
            // if (Input.GetButtonUp("Fire1"))
            // {
            //     RaycastWeapon.StopFiring();
            // }
        }
    }
}
