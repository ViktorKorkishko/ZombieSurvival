using UnityEngine;

namespace Game.Character.Interaction.Models
{
    public class CharacterObjectInteractionModel : MonoBehaviour
    {
        [SerializeField] private float _maxPickUpDistance;
        
        public float MaxPickUpDistance => _maxPickUpDistance;
    }
}
