using Game.Inventory.Cells.Core.Views;
using UnityEngine;

namespace Game.Inventory.DragAndDrop.Controllers
{
    public class DraggedViewData
    {
        public CellView View { get; }
        public Transform ImageParent { get; }
        public Vector2 ImagePosition { get; }

        public DraggedViewData(CellView view, Transform imageParent, Vector2 imagePosition)
        {
            View = view;
            ImageParent = imageParent;
            ImagePosition = imagePosition;
        }
    }
}