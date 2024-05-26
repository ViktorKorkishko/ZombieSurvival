using UnityEngine;

namespace Game.Items.Properties.Implementations
{
    public class PickableItemProperty : PropertyBase
    {
        [field: SerializeField] public GameObject ItemPrefab { get; private set; }
        [field: SerializeField] public GameObject WorldObjectPrefab { get; private set; }
    }
}
