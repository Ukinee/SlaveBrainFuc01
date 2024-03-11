using Codebase.Forms.Views.Implementations;
using Codebase.Game.Presentations.Interfaces;
using Codebase.Game.Views.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Game.Views.Implementations
{
    public class LevelSelectingFormView : FormViewBase<ILevelSelectingFormPresenter>, ILevelSelectingFormView
    {
        [SerializeField] private Button _backButton;
        
        [SerializeField] private GameObject _levelContents;
        
        private void OnEnable()
        {
            _backButton.onClick.AddListener(OnBackClicked);
            Presenter.Enable();
        }

        protected override void OnBeforeDisable()
        {
            _backButton.onClick.RemoveListener(OnBackClicked);
        }

        public void SetChild(ILevelView levelView)
        {
            ((MonoBehaviour)levelView).transform.SetParent(_levelContents.transform);
        }

        private void OnBackClicked()
        {
            Presenter.OnBackClicked();
        }
    }
}
