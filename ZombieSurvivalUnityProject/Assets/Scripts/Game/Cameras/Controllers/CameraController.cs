using System;
using Cinemachine;
using Core.Installers;
using Game.Cameras.Models;
using UnityEngine;
using Zenject;

namespace Game.Cameras.Controllers
{
    public class CameraController : IInitializable, IDisposable, IFixedTickable
    {
        [Inject] private CameraModel CameraModel { get; }
        [Inject(Id = BindingIdentifiers.CameraLookAtPointTransform)] private Transform CameraLookAtPoint { get; }

        private AxisState XAxisState => CameraModel.XAxisState;
        private AxisState YAxisState => CameraModel.YAxisState;

        private Camera _mainCamera;
        
        void IInitializable.Initialize()
        {
            CameraModel.OnGetMainCamera += HandleOnGetMainCamera;
        }

        void IDisposable.Dispose()
        {
            CameraModel.OnGetMainCamera -= HandleOnGetMainCamera;
        }

        void IFixedTickable.FixedTick()
        {
            CameraLookAtPoint.eulerAngles = new Vector3(YAxisState.Value, XAxisState.Value, 0);
        }
        
        private Camera HandleOnGetMainCamera()
        {
            // TODO: refactor into the CameraSystem with switchable cameras, at least InjectCamera here
            return _mainCamera ??= Camera.main;
        }

        class XInputAxisProvider : AxisState.IInputAxisProvider
        {
            public float GetAxisValue(int axis)
            {
                var value = Input.GetAxis("Mouse X");
                Debug.Log("Mouse X = " + value);
                return value;
            }
        }

        class YInputAxisProvider : AxisState.IInputAxisProvider
        {
            public float GetAxisValue(int axis)
            {
                var value = Input.GetAxis("Mouse Y");
                Debug.Log("Mouse Y = " + value);
                return value;
            }
        }
    }
}
