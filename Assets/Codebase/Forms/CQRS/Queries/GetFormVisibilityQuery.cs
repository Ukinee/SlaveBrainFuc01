using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Forms.Models;

namespace Codebase.Forms.CQRS.Queries
{
    public class GetFormVisibilityQuery : EntityUseCaseBase<FormBase>
    {
        public GetFormVisibilityQuery(IEntityRepository repository) : base(repository)
        {
        }

        public ILiveData<bool> Handle(int id) =>
            Get(id).VisibilityLiveData;
    }
}
