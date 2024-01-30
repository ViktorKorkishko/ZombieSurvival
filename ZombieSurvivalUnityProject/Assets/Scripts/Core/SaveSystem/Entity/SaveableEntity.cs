using System;
using UnityEngine;

namespace Core.SaveSystem.Entity
{
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] private string _id;

        public string Id => _id;
        
        // triggered when script is attached to a gameObject
        private void Reset()
        {
            TryGenerateId();
        }

        private void TryGenerateId()
        {
            bool generateId = string.IsNullOrEmpty(_id);
            if (generateId)
            {
                GenerateNewId();
            }
        }

        private void GenerateNewId()
        {
            _id = Guid.NewGuid().ToString();
        }
    }
}
