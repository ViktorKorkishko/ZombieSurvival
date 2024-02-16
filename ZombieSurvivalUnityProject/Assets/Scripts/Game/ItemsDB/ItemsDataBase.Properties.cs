using TriInspector;
using UnityEngine;

namespace Game.ItemsDB
{
    [DeclareTabGroup(Tabs.TabGroup)]
    public partial class ItemsDataBase
    {
        [Group(Tabs.TabGroup), Tab(Tabs.PropertiesTabHeader)] 
        [SerializeField] private string kek;
    }
}
