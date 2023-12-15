using System;

namespace Game.Inputs.Models
{
    public partial class InputModel
    {
        public bool JumpButtonClickInput => OnJumpButtonClicked?.Invoke() ?? false;
        public bool ReloadButtonClickInput => OnReloadButtonClicked?.Invoke() ?? false;
        public bool PickUpWeaponButtonClickInput => OnPickUpWeaponButtonClicked?.Invoke() ?? false;

        public bool SelectMainWeaponButtonClickInput => OnSelectMainWeaponButtonClicked?.Invoke() ?? false;
        public bool SelectSecondaryWeaponButtonClickInput => OnSelectSecondaryWeaponButtonClicked?.Invoke() ?? false;
        
        public float HorizontalAxisInput => OnGetHorizontalAxisInput?.Invoke() ?? 0;
        public float VerticalAxisInput => OnGetVerticalAxisInput?.Invoke() ?? 0;

        public event Func<bool> OnJumpButtonClicked;
        public event Func<bool> OnReloadButtonClicked;
        public event Func<bool> OnPickUpWeaponButtonClicked;

        public event Func<bool> OnSelectMainWeaponButtonClicked;
        public event Func<bool> OnSelectSecondaryWeaponButtonClicked;

        public event Func<float> OnGetHorizontalAxisInput;
        public event Func<float> OnGetVerticalAxisInput;
    }
}
