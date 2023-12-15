using System;
using Game.Inputs.Models;
using Zenject;

namespace Game.Inputs.Controllers
{
    public partial class InputController : IInitializable, IDisposable
    {
        [Inject] private protected InputModel InputModel { get; }

        public void Initialize()
        {
            InitializeKeyboardInputHandlers();
            InitializeMouseInputHandlers();
        }

        public void Dispose()
        {
            DisposeKeyboardInputHandlers();
            DisposeMouseInputHandlers();
        }
    }
}
