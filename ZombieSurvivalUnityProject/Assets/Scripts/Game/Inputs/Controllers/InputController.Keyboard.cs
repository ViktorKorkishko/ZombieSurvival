using System;
using Game.Inputs.Models;
using UnityEngine;

namespace Game.Inputs.Controllers
{
    public partial class InputController
    {
        private protected void InitializeKeyboardInputHandlers()
        {
            InputModel.OnJumpButtonClicked += HandleOnJumpButtonClicked;
            InputModel.OnReloadButtonClicked += HandleOnReloadButtonClicked;
            InputModel.OnPickUpWeaponButtonClicked += HandleOnPickUpWeaponButtonClicked;

            InputModel.OnSelectMainWeaponButtonClicked += HandleOnSelectMainWeaponButtonClicked;
            InputModel.OnSelectSecondaryWeaponButtonClicked += HandleOnSelectSecondaryWeaponButtonClicked;

            InputModel.OnGetHorizontalAxisInput += HandleOnGetHorizontalAxisInput;
            InputModel.OnGetVerticalAxisInput += HandleOnGetVerticalAxisInput;
        }

        private protected void DisposeKeyboardInputHandlers()
        {
            InputModel.OnJumpButtonClicked -= HandleOnJumpButtonClicked;
            InputModel.OnReloadButtonClicked -= HandleOnReloadButtonClicked;
            InputModel.OnPickUpWeaponButtonClicked -= HandleOnPickUpWeaponButtonClicked;

            InputModel.OnSelectMainWeaponButtonClicked -= HandleOnSelectMainWeaponButtonClicked;
            InputModel.OnSelectSecondaryWeaponButtonClicked -= HandleOnSelectSecondaryWeaponButtonClicked;

            InputModel.OnGetHorizontalAxisInput -= HandleOnGetHorizontalAxisInput;
            InputModel.OnGetVerticalAxisInput -= HandleOnGetVerticalAxisInput;
        }

        private bool HandleOnJumpButtonClicked() => Input.GetButtonDown("Jump");
        private bool HandleOnReloadButtonClicked() => Input.GetKeyDown(KeyCode.R);
        private bool HandleOnPickUpWeaponButtonClicked() => Input.GetKeyDown(KeyCode.F);

        private bool HandleOnSelectMainWeaponButtonClicked() => Input.GetKeyDown(KeyCode.Alpha1);
        private bool HandleOnSelectSecondaryWeaponButtonClicked() => Input.GetKeyDown(KeyCode.Alpha2);

        private float HandleOnGetHorizontalAxisInput() => Input.GetAxis("Horizontal");
        private float HandleOnGetVerticalAxisInput() => Input.GetAxis("Vertical");
    }
}
