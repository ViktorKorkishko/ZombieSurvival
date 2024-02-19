using Game.Inventory.Cells.Core.Views;
using UnityEngine;

namespace Game.Inventory.DragAndDrop.Controllers
{
    public class DraggedViewData
    {
        public CellView View { get; }
        public GameObject DraggedItemImageGO { get; }
        public RectTransform DraggedItemImageRectTransform => (RectTransform)DraggedItemImageGO.transform;

        public DraggedViewData(CellView cellView, GameObject draggedItemImageGO)
        {
            View = cellView;

            DraggedItemImageGO =
                Object.Instantiate(draggedItemImageGO, GameObject.Find("InventoryView(Clone)").transform, true);
            var originRectTransform = (RectTransform)draggedItemImageGO.transform;
            var originRect = originRectTransform.rect;
            DraggedItemImageRectTransform.rect.Set(originRect.x, originRect.y, originRect.size.x, originRect.size.y);

            DraggedItemImageGO.GetComponent<CanvasGroup>().blocksRaycasts = false;
            DraggedItemImageGO.transform.SetAsLastSibling();
        }
    }
}