using Game.Cameras.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.Cameras.Controllers
{
    public class RigLootAtPointController : ITickable
    {
        [Inject] private CameraModel CameraModel { get; }

        private Transform CameraTransform => CameraModel.GetMainCamera().transform;

        private readonly Transform _point;
        private readonly Vector3 _offset;
        
        public RigLootAtPointController(Transform point, Vector3 offset)
        {
            _point = point;
            _offset = offset;
        }
        
        void ITickable.Tick()
        {
            _point.position = CameraTransform.position + CameraTransform.forward * _offset.z;
        }
    }
}
