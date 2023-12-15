using UnityEngine;

namespace Game.Character.Movement.Rotation.Models
{
    public class CharacterRotationModel : MonoBehaviour
    {
        [SerializeField] private float _turnSpeed;
        
        public float TurnSpeed => _turnSpeed;
    }
}
