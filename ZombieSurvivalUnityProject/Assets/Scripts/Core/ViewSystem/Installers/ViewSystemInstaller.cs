using System;
using System.Collections.Generic;
using Core.Installers;
using Core.ViewSystem.Enums;
using Core.ViewSystem.Models;
using Core.ViewSystem.Providers;
using Core.ViewSystem.Views;
using Core.ViewSystem.Views.Interfaces;
using Game.Inventory.Core.Controllers;
using Game.Inventory.Core.Views;
using Game.Settings.Controllers;
using Game.Settings.Views;
using UnityEngine;
using Zenject;

namespace Core.ViewSystem.Installers
{
    public class ViewSystemInstaller : MonoInstaller
    {
        [SerializeField] private Transform _viewsParent;
        
        [Header("View prefabs")]
        [SerializeField] private SettingsView _settingsViewPrefab;
        [SerializeField] private InventoryView _inventoryViewPrefab;

        private readonly Dictionary<ViewId, IView> _viewIdToViewInstanceDictionary = new();
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ViewSystemModel>().AsSingle();
            
            Container.BindInstance(_viewsParent)
                .WithId(BindingIdentifiers.ViewsParent);
            
            Container
                .Bind<IViewProvider>()
                .To<ViewProvider>()
                .AsSingle()
                .WithArguments(_viewIdToViewInstanceDictionary);
            
            BindView(_settingsViewPrefab, ViewId.Settings, typeof(SettingsViewController));
            BindView(_inventoryViewPrefab, ViewId.Inventory, typeof(InventoryViewController));
        }

        private void BindView(ViewBase viewPrefab, ViewId viewId, Type typeViewController)
        {
            var viewInstance = Container
                .InstantiatePrefabForComponent<ViewBase>(viewPrefab, _viewsParent);

            var viewType = viewPrefab.GetType();
            // REMOVE LATER
            Container
                .Bind(viewType)
                .FromInstance(viewInstance)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo(typeViewController)
                .AsSingle()
                .WithArguments(viewInstance);
            
            _viewIdToViewInstanceDictionary.Add(viewId, viewInstance);
        }
    }
}
