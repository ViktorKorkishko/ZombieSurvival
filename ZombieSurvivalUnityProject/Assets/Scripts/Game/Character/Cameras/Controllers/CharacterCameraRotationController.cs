using System;
using Cinemachine;
using Core.Installers;
using Game.Cameras.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.Cameras.Controllers
{
    public class CharacterCameraRotationController : IFixedTickable
    {
        [Inject] private CameraModel CameraModel { get; }
        [Inject(Id = BindingIdentifiers.CameraLookAtPointTransform)] private Transform CameraLookAtPoint { get; }
        
        private AxisState XAxisState => CameraModel.XAxisState;
        private AxisState YAxisState => CameraModel.YAxisState;
        
        void IFixedTickable.FixedTick()
        {
            CameraLookAtPoint.eulerAngles = new Vector3(YAxisState.Value, XAxisState.Value, 0);
            Debug.Log($"eulerAngles: " + CameraLookAtPoint.eulerAngles + 
                      "X.Value: " + XAxisState.Value +
                      "Y.Value: " + YAxisState.Value);
        }
    }
}
