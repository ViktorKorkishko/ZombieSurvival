using Core.SaveSystem.Entity;
using Core.SaveSystem.Models;

namespace Game.Character.Movement.Position.Models
{
    public partial class CharacterPositionModel : SaveableModel<CharacterPositionModel.Data>
    {
        public CharacterPositionModel(SaveableEntity entity) : base(entity)
        {
        }
    }
}
