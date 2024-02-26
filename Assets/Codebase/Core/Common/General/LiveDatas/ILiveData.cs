using System;

namespace ApplicationCode.Core.Common.General.LiveDatas
{
    public interface ILiveData<out T>
    {
        public T Value { get; }

        public void AddListener(Action<T> listener);

        public void RemoveListener(Action<T> listener);
    }
}