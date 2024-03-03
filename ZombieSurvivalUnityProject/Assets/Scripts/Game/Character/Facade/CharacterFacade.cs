using UnityEngine;
using Zenject;

namespace Game.Character.Facade
{
    public class CharacterFacade : MonoBehaviour
    {
        private DiContainer CharacterDiContainer { get; set; }

        public void Init(DiContainer diContainer)
        {
            CharacterDiContainer = diContainer;
        }
    }
}
