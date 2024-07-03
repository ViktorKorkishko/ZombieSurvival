using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Game.Hotkeys.Models
{
    public class HotKeysModel : ITickable
    {
        private Dictionary<KeyCode, List<HotKeyContainer>> _keyCodeStacks;

        private List<KeyCode> keyCodes => Enum.GetValues(typeof(KeyCode))
            .Cast<KeyCode>()
            .Except(new [] { KeyCode.Mouse0 })
            .ToList();

        public HotKeysModel()
        {
            _keyCodeStacks = new();
        }
        
        void ITickable.Tick()
        {
            var pressedKeyCodes = keyCodes.Where(x => Input.GetKeyDown(x));
            foreach (var pressedKey in pressedKeyCodes)
            {
                if (_keyCodeStacks.TryGetValue(pressedKey, out var keyCodeActionStack))
                {
                    if (keyCodeActionStack.Count == 0)
                        return;
                    
                    keyCodeActionStack.Last().Action?.Invoke();
                }
            }
        }
        
        public void OverrideHotKey(KeyCode keyCode, Action action, object source)
        {
            if (_keyCodeStacks.TryGetValue(keyCode, out var hotKeyStack))
            {
                hotKeyStack.Add(new HotKeyContainer(action, source));
            }
            else
            {
                _keyCodeStacks.Add(keyCode, new List<HotKeyContainer> { new(action, source) });
            }
        }
        
        public void ClearHotKey(KeyCode keyCode, object source)
        {
            if (_keyCodeStacks.TryGetValue(keyCode, out var hotKeyStack))
            {
                var hotKeyContainer = hotKeyStack.LastOrDefault(x => x.Source == source);
                if (hotKeyContainer == null)
                {
                    return;
                }

                hotKeyStack.Remove(hotKeyContainer);
            }
        }
    }
}
