using Game.Cameras.Models;
using Game.Character.ObjectDetector.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.ObjectDetector.Controllers
{
    public class ObjectDetectorController : ITickable
    {
        [Inject] private ObjectDetectorModel ObjectDetectorModel { get; }
        [Inject] private CameraModel CameraModel { get; }

        private Transform MainCameraTransform => CameraModel.GetMainCamera().transform;
        
        void ITickable.Tick()
        {
            TryDetectObject();
        }

        private void TryDetectObject()
        {
            Vector3 origin = MainCameraTransform.position;
            Vector3 direction = MainCameraTransform.forward;
            float rayDistance = ObjectDetectorModel.MaxPickUpDistance;

            if (Physics.Raycast(origin, direction, out var raycastHit, rayDistance))
            {
                var detectedGameObject = raycastHit.collider.gameObject;
                ObjectDetectorModel.DetectObject(detectedGameObject);
            }
            else
            {
                ObjectDetectorModel.DetectObject(null);
            }
        }
    }
}
