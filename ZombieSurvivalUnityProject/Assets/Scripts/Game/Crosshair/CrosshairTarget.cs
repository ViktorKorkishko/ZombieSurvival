using Core.Installers.Ids;
using Game.Cameras.Models;
using UnityEngine;
using Zenject;

public class CrosshairTarget : ITickable
{
    [Inject] private CameraModel CameraModel { get; }
    [Inject(Id = BindingIdentifiers.CrosshairTarget)] private Transform CrosshairPoint { get; }
    
    void ITickable.Tick()
    {
        var cameraTransform = CameraModel.GetMainCamera().transform;
        Vector3 originPoint = cameraTransform.position;
        Vector3 direction = cameraTransform.forward;

        if (Physics.Raycast(originPoint, direction, out var raycastHit))
        {
            CrosshairPoint.position = raycastHit.point;
        }
    }
}
