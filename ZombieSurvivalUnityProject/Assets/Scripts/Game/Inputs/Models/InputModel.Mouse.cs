using System;
using UnityEngine;

namespace Game.Inputs.Models
{
    public partial class InputModel
    {
        public Vector3 MousePosition => OnGetMousePosition?.Invoke() ?? Vector3.zero;
        public float MouseSensitivity { get; } = 150f;

        public float HorizontalMouseAxisInput => OnGetHorizontalMouseAxisInput?.Invoke() ?? 0;
        public float VerticalMouseAxisInput => OnGetVerticalMouseAxisInput?.Invoke() ?? 0;

        public bool LeftMouseButtonClicked => OnLeftMouseButtonClicked?.Invoke() ?? false;
        
        public bool RightMouseButtonClicked => OnRightMouseButtonHold?.Invoke() ?? false;

        public event Func<Vector3> OnGetMousePosition;
        public event Func<float> OnGetHorizontalMouseAxisInput;
        public event Func<float> OnGetVerticalMouseAxisInput;
        
        public event Func<bool> OnLeftMouseButtonClicked;
        
        public event Func<bool> OnRightMouseButtonHold;
    }
}
