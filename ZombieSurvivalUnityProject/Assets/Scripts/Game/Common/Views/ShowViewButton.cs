using Core.ViewSystem.Enums;
using Core.ViewSystem.Models;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Common.Views
{
    public class ShowViewButton : MonoBehaviour
    {
        [SerializeField] private ViewId _viewIdToShow;
        [SerializeField] private Button _button;

        [Inject] private ViewSystemModel ViewSystemModel { get; }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(ShowView);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ShowView);
        }

        private void ShowView()
        {
            ViewSystemModel.Show(_viewIdToShow);
        }
    }
}
