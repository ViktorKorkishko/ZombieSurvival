using Core.Installers;
using Game.Cameras.Models;
using Game.Character.Movement.Rotation.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.Movement.Rotation.Controllers
{
    public class CharacterRotationController : ITickable
    {
        [Inject] private CameraModel CameraModel { get; }
        [Inject(Id = BindingIdentifiers.CharacterRigRoot)] private Transform RigRootTransform { get; }
        [Inject] private CharacterRotationModel CharacterRotationModel { get; }

        private float TurnSpeed => CharacterRotationModel.TurnSpeed;

        void ITickable.Tick()
        {
            float yawCamera = CameraModel.GetMainCamera().transform.rotation.eulerAngles.y;
            RigRootTransform.rotation = Quaternion.Slerp(RigRootTransform.rotation,
                Quaternion.Euler(0, yawCamera, 0),
                TurnSpeed * Time.deltaTime);
        }
    }
}