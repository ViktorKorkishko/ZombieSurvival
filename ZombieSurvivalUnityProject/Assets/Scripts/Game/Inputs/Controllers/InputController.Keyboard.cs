using UnityEngine;

namespace Game.Inputs.Controllers
{
    public partial class InputController
    {
        private protected void InitializeKeyboardInputHandlers()
        {
            InputModel.OnJumpButtonClicked += HandleOnJumpButtonClicked;
            InputModel.OnSprintButtonHold += HandleOnSprintButtonHold;
            InputModel.OnReloadButtonClicked += HandleOnReloadButtonClicked;
            InputModel.OnInteractObjectButtonClicked += HandleOnInteractObjectButtonClicked;
            InputModel.OnDropWeaponButtonClicked += HandleOnDropWeaponButtonClicked;

            InputModel.OnSelectMainWeaponButtonClicked += HandleOnSelectMainWeaponButtonClicked;
            InputModel.OnSelectSecondaryWeaponButtonClicked += HandleOnSelectSecondaryWeaponButtonClicked;

            InputModel.OnGetHorizontalAxisInput += HandleOnGetHorizontalAxisInput;
            InputModel.OnGetVerticalAxisInput += HandleOnGetVerticalAxisInput;
        }

        private protected void DisposeKeyboardInputHandlers()
        {
            InputModel.OnJumpButtonClicked -= HandleOnJumpButtonClicked;
            InputModel.OnSprintButtonHold -= HandleOnSprintButtonHold;
            InputModel.OnReloadButtonClicked -= HandleOnReloadButtonClicked;
            InputModel.OnInteractObjectButtonClicked -= HandleOnInteractObjectButtonClicked;
            InputModel.OnDropWeaponButtonClicked -= HandleOnDropWeaponButtonClicked;

            InputModel.OnSelectMainWeaponButtonClicked -= HandleOnSelectMainWeaponButtonClicked;
            InputModel.OnSelectSecondaryWeaponButtonClicked -= HandleOnSelectSecondaryWeaponButtonClicked;

            InputModel.OnGetHorizontalAxisInput -= HandleOnGetHorizontalAxisInput;
            InputModel.OnGetVerticalAxisInput -= HandleOnGetVerticalAxisInput;
        }

        private bool HandleOnJumpButtonClicked() => Input.GetButtonDown("Jump");
        private bool HandleOnSprintButtonHold() => Input.GetKey(KeyCode.LeftShift);
        private bool HandleOnReloadButtonClicked() => Input.GetKeyDown(KeyCode.R);
        private bool HandleOnInteractObjectButtonClicked() => Input.GetKeyDown(KeyCode.E);
        private bool HandleOnDropWeaponButtonClicked() => Input.GetKeyDown(KeyCode.F);

        private bool HandleOnSelectMainWeaponButtonClicked() => Input.GetKeyDown(KeyCode.Alpha1);
        private bool HandleOnSelectSecondaryWeaponButtonClicked() => Input.GetKeyDown(KeyCode.Alpha2);

        private float HandleOnGetHorizontalAxisInput() => Input.GetAxis("Horizontal");
        private float HandleOnGetVerticalAxisInput() => Input.GetAxis("Vertical");
    }
}
