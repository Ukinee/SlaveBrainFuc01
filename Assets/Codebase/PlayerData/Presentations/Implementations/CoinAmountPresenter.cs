using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.MVP.Interfaces;
using Codebase.PlayerData.CQRS.Queries;
using Codebase.PlayerData.Views.Interfaces;

namespace Codebase.PlayerData.Presentations.Implementations
{
    public class CoinAmountPresenter : IPresenter
    {
        private ITextView _textView;
        private ILiveData<int> _coinAmount;
        
        public CoinAmountPresenter(GetCoinAmountQuery getCoinAmountQuery, ITextView textView)
        {
            _textView = textView;
            _coinAmount = getCoinAmountQuery.Handle();
        }
        
        public void Enable()
        {
            _coinAmount.AddListener(OnCoinAmountChanged);
        }

        public void Disable()
        {
            _coinAmount.RemoveListener(OnCoinAmountChanged);
        }

        private void OnCoinAmountChanged(int amount)
        {
            _textView.Set(amount.ToString());
        }
    }
}
