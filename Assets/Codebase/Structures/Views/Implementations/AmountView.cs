using Codebase.Structures.Views.Interfaces;
using TMPro;
using UnityEngine;

namespace Codebase.Structures.Views.Implementations
{
    public class AmountView : MonoBehaviour, IAmountView
    {
        [SerializeField] private TMP_Text _text;

        public void Set(string value)
        {
            _text.text = value;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}
