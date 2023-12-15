using UnityEngine;

namespace Game.Character.Movement.Aim.Models
{
    public class CharacterAimModel : MonoBehaviour
    {
        [SerializeField] private float _aimDuration;

        public float AimDuration => _aimDuration;
    }
}
