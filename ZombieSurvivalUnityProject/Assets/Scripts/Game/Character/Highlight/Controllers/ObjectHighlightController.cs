using Game.Character.ObjectDetector.Models;
using Game.InteractableObjects.Highlight.Views;
using UnityEngine;
using Zenject;

namespace Game.Character.Highlight.Controllers
{
    public class ObjectHighlightController : ITickable
    {
        [Inject] private ObjectDetectorModel ObjectDetectorModel { get; }
        
        // TODO: temp solution: refactor after highlight objects system is reworked (has to be injected)
        private SelectionOutlineController _selectionOutlineController;
        private SelectionOutlineController SelectionOutlineController =>
            _selectionOutlineController ??= Object.FindObjectOfType<SelectionOutlineController>();
        
        void ITickable.Tick()
        {
            var detectedObject = ObjectDetectorModel.CurrentlyDetectedObject;
            bool isObjectDetected = detectedObject != null;
            if (!isObjectDetected)
            {
                DisableHighlight();
                return;
            }

            HandleDetectedObject(detectedObject);
        }

        private void HandleDetectedObject(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<HighlightableObjectView>(out var highlightableObjectView))
            {
                EnableObjectHighlight(highlightableObjectView);
            }
            else
            {
                SelectionOutlineController.UpdateTargetRenderer(null);
            }
        }

        private void EnableObjectHighlight(HighlightableObjectView highlightableObjectView)
        {
            var renderer = highlightableObjectView.Renderer;
            SelectionOutlineController.UpdateTargetRenderer(renderer);
        }

        private void DisableHighlight()
        {
            SelectionOutlineController.UpdateTargetRenderer(null);
        }
    }
}
