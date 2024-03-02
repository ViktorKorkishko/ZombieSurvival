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
        
        void IInitializable.Initialize()
        {
            CameraModel.OnGetMainCamera += HandleOnGetMainCamera;
        }

        void IDisposable.Dispose()
        {
            CameraModel.OnGetMainCamera -= HandleOnGetMainCamera;
        }
        
        private Camera HandleOnGetMainCamera()
        {
            // TODO: refactor into the CameraSystem with switchable cameras, at least InjectCamera here
            return _mainCamera ??= Camera.main;
        }
    }
}
