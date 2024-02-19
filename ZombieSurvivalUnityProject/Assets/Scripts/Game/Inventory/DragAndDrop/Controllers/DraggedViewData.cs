using Game.Inventory.Cells.Core.Views;
using UnityEngine;

namespace Game.Inventory.DragAndDrop.Controllers
{
    public class DraggedViewData
    {
        public CellView View { get; }
        public GameObject DraggedItemImageGO { get; }
        public RectTransform DraggedItemImageRectTransform => (RectTransform)DraggedItemImageGO.transform;

        public DraggedViewData(CellView cellView)
        {
            View = cellView;

            var prefab = cellView.ItemImage.gameObject;
            DraggedItemImageGO = Object.Instantiate(prefab, GameObject.Find("InventoryView(Clone)").transform, true);
            DraggedItemImageGO.transform.SetAsLastSibling();
        }
    }
}
