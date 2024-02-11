using Core.SaveSystem.SaveGroups;
using UnityEngine;

namespace Core.SaveSystem.Entity
{
    public partial class SaveableEntity : MonoBehaviour
    {
        [Header("Save params")]
        [SerializeField] private SaveGroupId _groupId;
        [SerializeField] private string _id;

        private readonly GuidFactory _guidFactory = new();

        // triggered when script is attached to a gameObject
        private void Reset()
        {
            GenerateNewId();
        }

        private void GenerateNewId()
        {
            _id = _guidFactory.GetGuid().ToString();
        }
    }
}
