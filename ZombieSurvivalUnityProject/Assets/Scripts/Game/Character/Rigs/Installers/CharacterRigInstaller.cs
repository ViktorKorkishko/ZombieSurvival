using Core.Installers.Ids;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

namespace Game.Character.Rigs.Installers
{
    public class CharacterRigInstaller : MonoInstaller
    {
        [SerializeField] private RigBuilder _rigBuilder;
        
        [Header("Rigs")]
        [SerializeField] private Rig _aimRig;
        
        [Header("Hand constraints")]
        [SerializeField] private TwoBoneIKConstraint _leftHandIKConstraint;
        [SerializeField] private TwoBoneIKConstraint _rightHandIKConstraint;
        
        public override void InstallBindings()
        {
            Container.Bind<RigBuilder>()
                .FromInstance(_rigBuilder)
                .AsSingle();
            
            Container.Bind<Rig>()
                .WithId(BindingIdentifiers.CharacterAimRig)
                .FromInstance(_aimRig)
                .AsSingle();
            
            Container.Bind<TwoBoneIKConstraint>()
                .WithId(BindingIdentifiers.LeftHandIKConstraint)
                .FromInstance(_leftHandIKConstraint);
            Container.Bind<TwoBoneIKConstraint>()
                .WithId(BindingIdentifiers.RightHandIKConstraint)
                .FromInstance(_rightHandIKConstraint);
        }
    }
}
