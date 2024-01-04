using UnityEngine;

namespace Game.Character.Movement.Locomotion.Models
{
    public class CorrectGravityCharacterLocomotionModel : MonoBehaviour
    {
        [Header("Ground")]
        [SerializeField] private float _groundSpeed;
        
        [Header("Jump")]
        [SerializeField] private float _maxJumpHeight;
        [SerializeField] private float _maxJumpTime;
        [SerializeField] private float _airHorizontalMovementMultiplier;
        
        public float GroundSpeed => _groundSpeed;

        public float MaxJumpHeight => _maxJumpHeight;
        public float MaxJumpTime => _maxJumpTime;
        public float AirHorizontalMovementMultiplier => _airHorizontalMovementMultiplier;
    }
}
