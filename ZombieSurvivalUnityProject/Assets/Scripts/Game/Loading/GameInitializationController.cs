using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Coroutines.Models;
using Core.Lifetime;
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

        public override void Initialize()
        {
            var saveGroups = DiContainer.ResolveAll<SaveGroup>();
            CoroutinePlayerModel.StartCoroutine(WaitForInit(saveGroups));
        }

        private IEnumerator WaitForInit(List<SaveGroup> saveGroups)
        {
            yield return new WaitUntil(() => saveGroups.All(x => x.Initialized));
            yield return SceneManager.LoadSceneAsync("GameScene");
            Debug.Log("Inited");
        }
    }
}
