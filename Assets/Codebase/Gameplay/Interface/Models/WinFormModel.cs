﻿using Codebase.Core.Common.General.LiveDatas;
using Codebase.Forms.Models;

namespace Codebase.Gameplay.Interface.Models
{
    public class WinFormModel : FormBase
    {
        private LiveData<int> _coinAmount = new LiveData<int>(0);

        public WinFormModel(bool isVisible, int id) : base(isVisible, id)
        {
        }

        public ILiveData<int> CoinAmount => _coinAmount;
        
        public void SetCoinAmount(int coinAmount) => 
            _coinAmount.Value = coinAmount;
    }
}