namespace Codebase.Core.Frameworks.MVP.Interfaces
{
    public interface IView<in TPresenter> where TPresenter : IPresenter
    {
        public void Construct(TPresenter presenter);
    }
}
