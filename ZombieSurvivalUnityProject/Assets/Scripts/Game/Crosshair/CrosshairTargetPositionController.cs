using Core.Installers;
using Game.Cameras.Models;
using UnityEngine;
using Zenject;

namespace Game.Crosshair
{
    public class CrosshairTargetPositionController : ITickable
    {
        [Inject] private CameraModel CameraModel { get; }

        [Inject(Id = BindingIdentifiers.CrosshairTargetPointTransform)]
        private Transform CrosshairPoint { get; }

        void ITickable.Tick()
        {
            var cameraTransform = CameraModel.GetMainCamera().transform;
            Vector3 originPoint = cameraTransform.position;
            Vector3 direction = cameraTransform.forward;

            if (Physics.Raycast(originPoint, direction, out var raycastHit))
            {
                CrosshairPoint.position = raycastHit.point;
            }
        }
    }
}
