using Game.Cameras.Models;
using UnityEngine;
using Zenject;

namespace Game.Common.Views.LookAt
{
    public class LootAtCameraController : ILateTickable
    {
        [Inject] private CameraModel CameraModel { get; }

        private Transform _objectToPoint;
        
        public LootAtCameraController(Transform objectToPoint)
        {
            _objectToPoint = objectToPoint;
        }
        
        void ILateTickable.LateTick()
        {
            var cameraTransform = CameraModel.GetMainCamera().transform;
            _objectToPoint.LookAt(_objectToPoint.position + cameraTransform.forward);
        }
    }
}