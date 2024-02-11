using System.Linq;
using Core.SaveSystem.SaveGroups;
using Zenject;

namespace Core.SaveSystem.Entity
{
    public partial class SaveableEntity
    {
        public SaveGroup SaveGroup { get; private set;}
        
        private SaveGroupId GroupId => _groupId;

        public string Id => _id;

        [Inject]
        public void Construct(DiContainer diContainer)
        {
            var saveGroups = diContainer.ResolveAll<SaveGroup>();
            SaveGroup = saveGroups.FirstOrDefault(x => x.SaveGroupId == GroupId);
        }
    }
}
