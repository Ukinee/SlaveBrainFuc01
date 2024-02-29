﻿using Codebase.Core.Frameworks.MVP.Interfaces;
using Codebase.Structures.Models;
using Codebase.Structures.Views.Interfaces;

namespace Codebase.Structures.Presentations.Implementations
{
    public class StructurePresenter : IPresenter
    {
        private readonly StructureModel _structureModel;
        private readonly IStructureView _structureView;
        private readonly IAmountView _amountView;

        private int _maxAmount;

        public StructurePresenter(StructureModel structureModel, IStructureView structureView, IAmountView amountView)
        {
            _structureModel = structureModel;
            _structureView = structureView;
            _amountView = amountView;
        }

        public void Enable()
        {
            _structureModel.AmountChanged += OnAmountChanged;
            _maxAmount = _structureModel.Amount;
        }

        public void Disable()
        {
            _structureModel.AmountChanged -= OnAmountChanged;
        }

        private void OnAmountChanged(int amount)
        {
            _amountView.Set($"{amount} / {_maxAmount}");
            
            //if(_structureModel.Amount == 0)
        }

        public void Dispose()
        {
            _structureModel.Dispose();
            _structureView.Dispose();
            _amountView.Dispose();
        }
    }
}