using UnityEngine;

namespace Game.Character.Movement.Aim.Models
{
    public class CharacterAimModel : MonoBehaviour
    {
        [SerializeField] private float _aimingProcessDuration;
        
        [Header("Camera Distance")]
        [SerializeField] private float _minAimStateCameraDistance;
        [SerializeField] private float _maxAimStateCameraDistance;
        
        [Header("Camera FOV")]
        [SerializeField] private float minAimStateCameraFieldOfView;
        [SerializeField] private float maxAimStateCameraFieldOfView;

        public float AimingProcessDuration => _aimingProcessDuration;
        
        public float MinAimStateCameraDistance => _minAimStateCameraDistance;
        public float MaxAimStateCameraDistance => _maxAimStateCameraDistance;
        
        public float MinAimStateCameraFieldOfView => minAimStateCameraFieldOfView;
        public float MaxAimStateCameraFieldOfView => maxAimStateCameraFieldOfView;
    }
}