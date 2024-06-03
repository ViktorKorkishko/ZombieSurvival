using System.Linq;
using Core.SaveSystem.SaveGroups;
using UnityEngine;
using Zenject;

namespace Core.SaveSystem.Entity
{
    public class SaveableEntity : MonoBehaviour
    {
        [Header("Save params")]
        [SerializeField] private SaveGroupId _groupId;
        [SerializeField] private string _id;
        
        public SaveGroup SaveGroup { get; private set;}
        private SaveGroupId GroupId => _groupId;
        public string Id => _id;
        
        private readonly GuidFactory _guidFactory = new();
        
        [Inject]
        public void Construct(DiContainer diContainer)
        {
            var saveGroups = diContainer.ResolveAll<SaveGroup>();
            SaveGroup = saveGroups.FirstOrDefault(x => x.SaveGroupId == GroupId);
        }
        
#if UNITY_EDITOR
        // triggered when script is attached to a gameObject
        private void Reset()
        {
            GenerateNewId();
        }
#endif

        private void GenerateNewId()
        {
            _id = _guidFactory.GetGuid().ToString();
        }
    }
}
