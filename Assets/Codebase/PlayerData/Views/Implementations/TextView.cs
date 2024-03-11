using Codebase.Core.Frameworks.MVP.BaseClasses;
using Codebase.Core.Frameworks.MVP.Interfaces;
using Codebase.PlayerData.Views.Interfaces;
using TMPro;
using UnityEngine;

namespace Codebase.PlayerData.Views.Implementations
{
    public class TextView : ViewBase<IPresenter>, ITextView
    {
        [SerializeField] private TMP_Text _text;
        
        public void Set(string value)
        {
            _text.text = value;
        }

        public void Dispose()
        {
        }
    }
}
