using System;
using Cinemachine;
using UnityEngine;

namespace Game.Cameras.Models
{
    public class CameraModel : MonoBehaviour
    {
        [SerializeField] private AxisState _xAxisState;
        [SerializeField] private AxisState _yAxisState;

        public AxisState XAxisState => _xAxisState;
        public AxisState YAxisState => _yAxisState;

        public event Func<Camera> OnGetMainCamera;
        
        public Camera GetMainCamera() => OnGetMainCamera?.Invoke();

        private void FixedUpdate()
        {
            _xAxisState.Update(Time.fixedDeltaTime);
            _yAxisState.Update(Time.fixedDeltaTime);
        }
    }
}