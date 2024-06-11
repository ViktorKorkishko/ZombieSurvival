using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Coroutines.Models;
using Core.Lifetime;
using Core.Lifetime.Initialization;
using Core.SaveSystem.SaveGroups;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Loading
{
    public class GameInitializationController : SelfInitializableModel
    {
        [Inject] private DiContainer DiContainer { get; }
        [Inject] private CoroutinePlayerModel CoroutinePlayerModel { get; }
        
        private string _gameSceneName => "GameScene";
        private string _loadingSceneName => "LoadingScene";
        
        public override void Initialize()
        {
            CoroutinePlayerModel.StartCoroutine(LoadGame());
            
            SceneManager.sceneUnloaded += HandleSceneUnloaded;
            SceneManager.sceneUnloaded += HandleLoadingSceneUnloaded;
            SceneManager.UnloadSceneAsync(_loadingSceneName);
        }

        private IEnumerator LoadGame()
        {
            var saveGroups = DiContainer.ResolveAll<SaveGroup>();
            yield return WaitForSaveGroupsInitialization(saveGroups);
            yield return WaitForGameSceneLoad();
        }

        private IEnumerator WaitForSaveGroupsInitialization(List<SaveGroup> saveGroups)
        {
            yield return new WaitUntil(() => saveGroups.All(x => x.Initialized));
        }

        private IEnumerator WaitForGameSceneLoad()
        {
            yield return SceneManager.LoadSceneAsync(_gameSceneName);
        }

        private void HandleSceneUnloaded(UnityEngine.SceneManagement.Scene scene)
        {
            Debug.Log($"Scene [{scene.name}] unloaded");
        }

        private void HandleLoadingSceneUnloaded(UnityEngine.SceneManagement.Scene scene)
        {
            if (scene.name != _loadingSceneName)
                return;

            SceneManager.sceneUnloaded -= HandleSceneUnloaded;
            SceneManager.sceneUnloaded -= HandleLoadingSceneUnloaded;
        }
    }
}
