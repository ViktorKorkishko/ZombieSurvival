using System;
using UnityEngine;

namespace Game.Character.ObjectDetector.Models
{
    public class ObjectDetectorModel : MonoBehaviour
    {
        [field: SerializeField] public float MaxPickUpDistance { get; private set; }
        
        public GameObject CurrentlyDetectedObject { get; private set; }

        public Action<GameObject> OnObjectDetected { get; }

        public void DetectObject(GameObject gameObject)
        {
            CurrentlyDetectedObject = gameObject;
            OnObjectDetected?.Invoke(gameObject);
        }
    }
}
