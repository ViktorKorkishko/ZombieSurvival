using System;
using UnityEngine;

namespace Game.Cameras.Models
{
    public class CameraModel
    {
        public event Func<Camera> OnGetMainCamera;

        public Camera GetMainCamera() => OnGetMainCamera?.Invoke();
    }
}
