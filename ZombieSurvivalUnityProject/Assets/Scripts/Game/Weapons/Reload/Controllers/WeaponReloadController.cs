using Game.Inputs.Models;
using Game.Weapons.Reload.Models;
using Zenject;

namespace Game.Weapons.Reload.Controllers
{
    public class WeaponReloadController : ITickable
    {
        [Inject] private InputModel InputModel { get; }
        [Inject] private WeaponReloadModel WeaponReloadModel { get; }

        public void Tick()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            bool reloadButtonClicked = InputModel.ReloadButtonClickInput;
            if (reloadButtonClicked)
            {
                WeaponReloadModel.TryReload();
            }
        }
    }
}
