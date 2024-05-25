using Cinemachine;
using Game.Settings.ViewModel;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Cameras.Models
{
    public class CameraModel : MonoBehaviour
    {
        [Header("Rotation Axis")]
        [SerializeField] private AxisState _xAxisState;
        [SerializeField] private AxisState _yAxisState;
        
        [Header("State")]
        [ReadOnly] [SerializeField] private bool _locked;
        
        [Inject] private SettingsModel SettingsModel { get; }
        [Inject] private Camera MainCamera { get; }
        
        public AxisState XAxisState => _xAxisState;
        public AxisState YAxisState => _yAxisState;
        
        public bool Locked => _locked;
        
        public Camera GetMainCamera() => MainCamera;
        
        public void Lock()
        {
            _locked = true;
        }
        
        public void Unlock()
        {
            _locked = false;
        }
        
        private void FixedUpdate()
        {
            if (Locked)
                return;
            
            // TODO: sensitivity currently does no effect (fix later)
            var updateValue = Time.fixedDeltaTime * SettingsModel.Sensitivity;
            _xAxisState.Update(updateValue);
            _yAxisState.Update(updateValue);
        }
    }
}
