using UnityEngine;

namespace Game.Character.Movement.Locomotion.Models
{
    public class CharacterLocomotionModel : MonoBehaviour
    {
        [SerializeField] private float _jumpHeight;

        public float JumpHeight => _jumpHeight;
    }
}
