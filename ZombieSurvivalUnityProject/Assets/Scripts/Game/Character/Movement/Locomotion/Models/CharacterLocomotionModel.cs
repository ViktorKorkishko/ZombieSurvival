using UnityEngine;

namespace Game.Character.Movement.Locomotion.Models
{
    public class CharacterLocomotionModel : MonoBehaviour
    {
        [Header("Ground")]
        [SerializeField] private float _groundSpeed = 1f;
        [SerializeField] private float _groundGravity = 0.3f;
        
        [Header("Jump")]
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _jumpDemping = 0.5f;
        [SerializeField] private float _airControlMultiplier = 0.025f;
        [SerializeField] private float _airGravity = Physics.gravity.y;

        public float GroundSpeed => _groundSpeed;
        public float GroundGravity => _groundGravity;
        
        public float JumpHeight => _jumpHeight;
        public float JumpDemping => _jumpDemping;
        public float AirControlMultiplier => _airControlMultiplier;
        public float AirGravity => _airGravity;
    }
}
