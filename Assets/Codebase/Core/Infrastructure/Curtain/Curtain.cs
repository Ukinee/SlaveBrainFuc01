using Codebase.Core.Infrastructure.Services.Interfaces;
using UnityEngine;

namespace Codebase.Core.Infrastructure.Services.Implementations
{
    public class Curtain : MonoBehaviour, ICurtain
    {
        [SerializeField] private Canvas _canvas;
        
        public void Show()
        {
            _canvas.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _canvas.gameObject.SetActive(false);
        }
    }
}
