using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using UnityEngine;

namespace Codebase.Core.Infrastructure.Curtain
{
    public class Curtain : MonoBehaviour, ICurtain
    {
        [SerializeField] private GameObject _panel;
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
        
        public void Show()
        {
            "Show".Log();
            _panel.SetActive(true);
        }

        public void Hide()
        {
            "Hide".Log();
            _panel.SetActive(false);
        }
    }
}
