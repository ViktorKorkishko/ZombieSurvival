using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Items.Properties.Implementations
{
    public class PickableItemProperty : PropertyBase
    {
        [AssetsOnly] [field: SerializeField] public GameObject ItemPrefab { get; private set; }
        [AssetsOnly] [field: SerializeField] public GameObject WorldObjectPrefab { get; private set; }
    }
}
