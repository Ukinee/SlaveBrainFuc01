using System;
using System.Collections.Generic;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Forms.Common.FormTypes;
using Codebase.Forms.Models;
using Codebase.Forms.Views.Interfaces;

namespace Codebase.Forms.Services.Implementations.Factories
{
    public class FormCreationService
    {
        private IInterfaceView _interfaceView;
        private IInterfaceService _interfaceService;
        private Dictionary<Type, Func<Tuple<FormBase, IFormView>>> _factories;

        public FormCreationService
        (
            IInterfaceView interfaceView,
            IInterfaceService interfaceService,
            Dictionary<Type, Func<Tuple<FormBase, IFormView>>> factories
        )
        {
            _interfaceView = interfaceView;
            _interfaceService = interfaceService;
            _factories = factories;
        }

        public void Create(IFormType formType)
        {
            (FormBase model, IFormView view) = _factories[formType.GetType()].Invoke();
            
            _interfaceView.SetChild(view);
            _interfaceService.Register(formType, model.Id);
        }
    }
}
