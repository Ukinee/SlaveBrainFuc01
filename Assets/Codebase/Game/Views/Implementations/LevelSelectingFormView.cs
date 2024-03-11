using Codebase.Core.Frameworks.MVP.Interfaces;
using Codebase.Forms.Views.Implementations;
using Codebase.Game.Views.Interfaces;
using UnityEngine;

namespace Codebase.Game.Views.Implementations
{
    public class LevelSelectingFormView : FormViewBase<IPresenter>, ILevelSelectingFormView
    {
        private void OnEnable()
        {
            Presenter.Enable();
        }

        public void SetChild(ILevelView levelView)
        {
            ((MonoBehaviour)levelView).transform.SetParent(Content.transform);
        }
    }
}
