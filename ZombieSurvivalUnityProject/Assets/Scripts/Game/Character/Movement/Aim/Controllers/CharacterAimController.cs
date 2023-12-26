using Cinemachine;
using Game.Character.Movement.Aim.Models;
using Game.Inputs.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.Movement.Aim.Controllers
{
    public class CharacterAimController : IInitializable, ITickable
    {
        [Inject] private CharacterAimModel CharacterAimModel { get; }
        [Inject] private InputModel InputModel { get; }
        [Inject] private CinemachineVirtualCamera VirtualCamera { get; }

        private Cinemachine3rdPersonFollow _cinemachine3RdPersonFollow;
        private float _aimInterpolatedValue;
        
        void IInitializable.Initialize()
        {
            _cinemachine3RdPersonFollow = VirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        }
        
        void ITickable.Tick()
        {
            bool aimButtonHoldInput = InputModel.RightMouseButtonClicked;
            float aimValueDelta = aimButtonHoldInput ? Time.deltaTime : -Time.deltaTime;
            _aimInterpolatedValue += aimValueDelta / CharacterAimModel.AimDuration;
            _aimInterpolatedValue = Mathf.Clamp(_aimInterpolatedValue, 0f, 1f);
            
            _cinemachine3RdPersonFollow.CameraDistance = Mathf.Lerp(CharacterAimModel.MinAimStateCameraDistance,
                CharacterAimModel.MaxAimStateCameraDistance, 
                _aimInterpolatedValue);
        }
    }
}
