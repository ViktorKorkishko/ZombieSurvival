using Game.Inventory.Cells.Core.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory.DragAndDrop.Controllers
{
    public class DragData
    {
        public CellView View { get; }
        public GameObject DraggedImageGO { get; }
        public RectTransform DraggedItemImageRectTransform => (RectTransform)DraggedImageGO.transform;

        public DragData(CellView cellView, Image itemImage, Transform inventoryRootTransform)
        {
            View = cellView;

            var prefab = itemImage.gameObject;
            DraggedImageGO = Object.Instantiate(prefab, inventoryRootTransform, true);
            DraggedImageGO.transform.SetAsLastSibling();
        }
    }
}
