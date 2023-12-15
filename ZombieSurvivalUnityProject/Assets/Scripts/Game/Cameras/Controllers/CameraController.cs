using System;
using Game.Cameras.Models;
using UnityEngine;
using Zenject;

namespace Game.Cameras.Controllers
{
    public class CameraController : IInitializable, IDisposable
    {
        [Inject] private CameraModel CameraModel { get; }

        private Camera _mainCamera;

        public void Initialize()
        {
            CameraModel.OnGetMainCamera += HandleOnGetMainCamera;
        }

        public void Dispose()
        {
            CameraModel.OnGetMainCamera -= HandleOnGetMainCamera;
        }

        private Camera HandleOnGetMainCamera()
        {
            // TODO: refactor into the CameraSystem with switchable cameras
            return _mainCamera ??= Camera.main;
        }
    }
}
