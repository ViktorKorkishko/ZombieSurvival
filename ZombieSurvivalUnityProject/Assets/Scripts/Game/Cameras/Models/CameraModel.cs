using System;
using Cinemachine;
using Game.Settings.ViewModel;
using UnityEngine;
using Zenject;

namespace Game.Cameras.Models
{
    public class CameraModel : MonoBehaviour
    {
        [SerializeField] private AxisState _xAxisState;
        [SerializeField] private AxisState _yAxisState;

        [Inject] private SettingsModel SettingsModel { get; }

        public AxisState XAxisState => _xAxisState;
        public AxisState YAxisState => _yAxisState;

        public event Func<Camera> OnGetMainCamera;
        
        public Camera GetMainCamera() => OnGetMainCamera?.Invoke();

        private void FixedUpdate()
        {
            var updateValue = Time.fixedDeltaTime * SettingsModel.Sensitivity;
            _xAxisState.Update(updateValue);
            _yAxisState.Update(updateValue);
            
            Debug.Log(updateValue);
        }
    }
}
