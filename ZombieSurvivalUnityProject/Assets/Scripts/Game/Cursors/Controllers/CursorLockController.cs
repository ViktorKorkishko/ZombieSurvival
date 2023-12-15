using System;
using Game.Cursors.Models;
using UnityEngine;
using Zenject;

namespace Game.Cursors.Controllers
{
    public class CursorLockController : IInitializable, IDisposable, ITickable
    {
        [Inject] private CursorLockModel CursorLockModel { get; }
        
        void IInitializable.Initialize()
        {
            CursorLockModel.OnCursorLockSwitched += HandleOnCursorLockSwitched;

            CursorLockModel.SwitchCursorLock();
        }

        void IDisposable.Dispose()
        {
            CursorLockModel.OnCursorLockSwitched -= HandleOnCursorLockSwitched;
        }

        void ITickable.Tick()
        {
            bool switchLock = Input.GetKeyDown(KeyCode.Escape);
            if (switchLock)
            {
                CursorLockModel.SwitchCursorLock();
            }
        }

        private bool SwitchCursorLock()
        {
            bool locked = CursorLockModel.Locked;
            if (locked)
            {
                Cursor.lockState = CursorLockMode.None;
                locked = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                locked = true;
            }

            return locked;
        }

        private bool HandleOnCursorLockSwitched()
        {
            return SwitchCursorLock();
        }
    }
}
