using System;
using UnityEngine;

namespace Game.Character.Movement.Position.Models
{
    public partial class CharacterPositionModel
    {
        [Serializable]
        public new class Data
        {
            public Vector3 Position { get; set; }
        }
        
        protected override string DataKey => "CharacterPositionModel.Data";
    }
}
