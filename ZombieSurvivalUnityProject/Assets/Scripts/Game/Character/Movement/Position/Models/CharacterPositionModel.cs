using Core.Installers;
using Core.SaveSystem.Entity;
using Core.SaveSystem.Models;
using Core.SaveSystem.Saving.Common.Load;
using UnityEngine;
using Zenject;

namespace Game.Character.Movement.Position.Models
{
    public partial class CharacterPositionModel : SaveableModel<CharacterPositionModel.Data>,
        IFixedTickable
    {
        [Inject(Id = BindingIdentifiers.ViewRoot)] private Transform ViewRoot { get; }
        [Inject] private CharacterController CharacterController { get; }
        
        public CharacterPositionModel(SaveableEntity entity) : base(entity) { }
        
        void IFixedTickable.FixedTick()
        {
            // base.Data.Position = ViewRoot.position;
        }
        
        protected override void HandleOnDataLoaded(LoadResult<Data> loadResult)
        {
            switch (loadResult.Result)
            {
                case Result.LoadedSuccessfully:
                    RestorePlayerPosition();
                    break;
            }

            void RestorePlayerPosition()
            {
                CharacterController.enabled = false;
                ViewRoot.position = base.Data.Position;
                CharacterController.enabled = true;
            }
        }
        
        protected override void HandleOnDataPreSaved()
        {
            base.Data.Position = ViewRoot.position;
        }
    }
}
