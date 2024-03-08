using System;
using System.Collections.Generic;
using Codebase.Forms.Common.FormTypes;
using Codebase.Forms.CQRS;
using Codebase.Forms.Services.Implementations;

namespace Codebase.Forms.Services.Interfaces
{
    public class InterfaceService : IInterfaceService
    {
        private readonly SetFormVisibilityCommand _setFormVisibilityCommand;
        private readonly Dictionary<Type, int> _forms = new Dictionary<Type, int>();

        public InterfaceService(SetFormVisibilityCommand setFormVisibilityCommand)
        {
            _setFormVisibilityCommand = setFormVisibilityCommand;
        }

        public void Register(IFormType formType, int id)
        {
            if(_forms.TryAdd(formType.GetType(), id) == false)
                throw new Exception($"Form type {formType.GetType()} already registered");
        }

        public void Show(IFormType formType)
        {
            _setFormVisibilityCommand.Handle(_forms[formType.GetType()], true);
        }

        public void Hide(IFormType formType)
        {
            _setFormVisibilityCommand.Handle(_forms[formType.GetType()], false);
        }
    }
}
