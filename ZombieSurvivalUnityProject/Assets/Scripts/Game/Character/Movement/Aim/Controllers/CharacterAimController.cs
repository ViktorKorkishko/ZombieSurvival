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

        private Cinemachine3rdPersonFollow _cinemachineThirdPersonFollow;
        private float _distanceInterpolationValue;
        private float _fieldOfViewInterpolationValue;
        
        void IInitializable.Initialize()
        {
            _cinemachineThirdPersonFollow = VirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        }
        
        void ITickable.Tick()
        {
            bool aimButtonHoldInput = InputModel.RightMouseButtonHold;
            float aimValueDelta = aimButtonHoldInput ? Time.deltaTime : -Time.deltaTime;

            HandleCameraDistance(aimValueDelta);
            HandleCameraFOV(aimValueDelta);
        }

        private void HandleCameraDistance(float aimValueDelta)
        {
            _distanceInterpolationValue += aimValueDelta / CharacterAimModel.AimingProcessDuration;
            _distanceInterpolationValue = Mathf.Clamp(_distanceInterpolationValue, 0f, 1f);
            
            _cinemachineThirdPersonFollow.CameraDistance = Mathf.Lerp(CharacterAimModel.MinAimStateCameraDistance,
                CharacterAimModel.MaxAimStateCameraDistance, 
                _distanceInterpolationValue);
        }

        private void HandleCameraFOV(float aimValueDelta)
        {
            _fieldOfViewInterpolationValue += aimValueDelta / CharacterAimModel.AimingProcessDuration;
            _fieldOfViewInterpolationValue = Mathf.Clamp(_fieldOfViewInterpolationValue, 0f, 1f);
            
            VirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(CharacterAimModel.MinAimStateCameraFieldOfView,
                CharacterAimModel.MaxAimStateCameraFieldOfView, 
                _fieldOfViewInterpolationValue);
        }
    }
}
