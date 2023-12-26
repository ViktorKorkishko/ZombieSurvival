using UnityEngine;

namespace Game.Character.Movement.Aim.Models
{
    public class CharacterAimModel : MonoBehaviour
    {
        [SerializeField] private float _minAimStateCameraDistance;
        [SerializeField] private float _maxAimStateCameraDistance;
        [SerializeField] private float _aimDuration;

        public float AimDuration => _aimDuration;
        public float MinAimStateCameraDistance => _minAimStateCameraDistance;
        public float MaxAimStateCameraDistance => _maxAimStateCameraDistance;
    }
}
