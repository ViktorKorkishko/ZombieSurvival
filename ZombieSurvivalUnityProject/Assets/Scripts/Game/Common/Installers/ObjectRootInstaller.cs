using Core.Installers;
using UnityEngine;
using Zenject;

namespace Game.Common.Installers
{
    public class ObjectRootInstaller : MonoInstaller
    {
        [SerializeField] private Transform _rootTransform;

        public override void InstallBindings()
        {
            Container
                .Bind<Transform>()
                .WithId(BindingIdentifiers.Root)
                .FromInstance(_rootTransform)
                .AsCached();
        }
    }
}