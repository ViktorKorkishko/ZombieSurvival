using Core.ViewSystem.Enums;
using Core.ViewSystem.Providers;
using Game.Settings.Controllers;
using Game.Settings.ViewModel;
using Game.Settings.Views;
using UnityEngine;
using Zenject;

namespace Game.Settings.Installers
{
    public class SettingsInstaller : MonoInstaller
    {
        [SerializeField] private SettingsView _settingsViewPrefab;

        [Inject] private IViewProvider ViewProvider { get; }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SettingsModel>().AsSingle();
            
            var viewInstance = ViewProvider.RegisterView(_settingsViewPrefab, ViewId.Settings, LayerId.Popups);
            
            Container
                .BindInterfacesAndSelfTo<SettingsViewController>()
                .AsSingle()
                .WithArguments(viewInstance);
        }
    }
}
