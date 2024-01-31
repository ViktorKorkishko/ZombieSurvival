using Core.Installers;
using Core.SaveSystem.Saving.Common.Load;
using UnityEngine;
using Zenject;

namespace Game.Character.Movement.Position.Models
{
    public partial class CharacterPositionModel
    {
        [Inject(Id = BindingIdentifiers.ViewRoot)] private Transform ViewRoot { get; }
        [Inject] private CharacterController CharacterController { get; }
        
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
