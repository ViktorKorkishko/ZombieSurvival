using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Character.Movement.Rotation.Models
{
    public class CharacterRotationModel : MonoBehaviour
    {
        [field: SerializeField] public float TurnSpeed { get; private set; }
    }
}
