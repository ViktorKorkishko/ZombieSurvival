using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private CharacterAiming _characterAiming;
        
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CharacterAiming>().FromInstance(_characterAiming).AsSingle();
    }
}