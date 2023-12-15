using System;

namespace Game.Cursors.Models
{
    public class CursorLockModel
    {
        public bool Locked { get; private set; }

        public Func<bool> OnCursorLockSwitched;

        public void SwitchCursorLock()
        {
            Locked = OnCursorLockSwitched.Invoke();
        }
    }
}
