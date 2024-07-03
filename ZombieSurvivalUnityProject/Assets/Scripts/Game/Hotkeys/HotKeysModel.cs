using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Hotkeys
{
    public class HotKeysModel
    {
        private Dictionary<object, Dictionary<KeyCode, List<Action>>> _sourceToHotkeys;

        public HotKeysModel()
        {
            _sourceToHotkeys = new();
        }

        public void OverrideHotKey(object source, KeyCode keyCode, Action action)
        {
            if (_sourceToHotkeys.TryGetValue(source, out var keycodesToAction))
            {
                keycodesToAction.Add(keyCode, new() { action });
            }
            else
            {
                _sourceToHotkeys.Add(source, new Dictionary<KeyCode, List<Action>>()
                {
                    (keyCode, new List<Action>() { action })
                });
            }
        }

        public void ClearHotKey()
        {
            
        }
    }
}