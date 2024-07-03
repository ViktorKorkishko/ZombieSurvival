using System;

namespace Game.Hotkeys.Models
{
    public class HotKeyContainer
    {
        public Action Action { get; }
        public object Source { get; }
        
        public HotKeyContainer(Action action, object source)
        {
            Action = action;
            Source = source;
        }
    }
}
