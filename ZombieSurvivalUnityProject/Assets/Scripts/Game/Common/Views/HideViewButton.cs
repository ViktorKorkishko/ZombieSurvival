using Core.ViewSystem.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Common.Views
{
    public class HideViewButton : MonoBehaviour
    {
        [SerializeField] private ViewBase _viewToHide;
        [SerializeField] private Button _button;
        
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
            _viewToHide.Hide();
        }
    }
}
