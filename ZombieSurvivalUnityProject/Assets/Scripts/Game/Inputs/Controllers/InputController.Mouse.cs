using Game.Cameras.Models;
using UnityEngine;
using Zenject;

namespace Game.Inputs.Controllers
{
    public partial class InputController
    {
        [Inject] private CameraModel CameraModel { get; }

        private protected void InitializeMouseInputHandlers()
        {
            
            InputModel.OnGetMousePosition += HandleOnGetMousePosition;

            InputModel.OnLeftMouseButtonClicked += HandleOnLeftMouseButtonClicked;
            
            InputModel.OnRightMouseButtonHold += HandleOnRightMouseButtonHold;

            InputModel.OnGetHorizontalMouseAxisInput += HandleOnGetHorizontalMouseAxisInput;
            InputModel.OnGetVerticalMouseAxisInput += HandleOnGetVerticalMouseAxisInput;
        }

        private protected void DisposeMouseInputHandlers()
        {
            InputModel.OnGetMousePosition -= HandleOnGetMousePosition;

            InputModel.OnLeftMouseButtonClicked -= HandleOnLeftMouseButtonClicked;
            
            InputModel.OnRightMouseButtonHold -= HandleOnRightMouseButtonHold;

            InputModel.OnGetHorizontalMouseAxisInput -= HandleOnGetHorizontalMouseAxisInput;
            InputModel.OnGetVerticalMouseAxisInput -= HandleOnGetVerticalMouseAxisInput;
        }

        private Vector3 HandleOnGetMousePosition() => CameraModel.GetMainCamera().ScreenToWorldPoint(Input.mousePosition);

        private bool HandleOnLeftMouseButtonClicked() => Input.GetButtonDown("Fire1");

        private bool HandleOnRightMouseButtonHold() => Input.GetButton("Fire2");

        private float HandleOnGetHorizontalMouseAxisInput() => Input.GetAxis("Mouse X");
        private float HandleOnGetVerticalMouseAxisInput() => Input.GetAxis("Mouse Y");
    }
}
