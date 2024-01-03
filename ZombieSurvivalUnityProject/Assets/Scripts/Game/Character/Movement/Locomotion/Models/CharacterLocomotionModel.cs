using UnityEngine;

namespace Game.Character.Movement.Locomotion.Models
{
    public class CharacterLocomotionModel : MonoBehaviour
    {
        [SerializeField] private float _maxJumpHeight;
        [SerializeField] private float _maxJumpTime;

        public float MaxJumpHeight => _maxJumpHeight;
        public float MaxJumpTime => _maxJumpTime;
    }
}
