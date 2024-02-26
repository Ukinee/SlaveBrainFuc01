using System;
using System.Collections.Generic;

namespace ApplicationCode.Core.Common.General.LiveDatas
{
    public class LiveData<T> : ILiveData<T>, IDisposable
    {
        private List<Action<T>> _listeners = new List<Action<T>>();

        private T _value;

        private bool _isDisposed = false;

        public LiveData(T value)
        {
            Value = value;
        }

        public T Value
        {
            get => _isDisposed == false ? _value : throw new ObjectDisposedException(nameof(LiveData<T>));
            set
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(nameof(LiveData<T>));

                _value = value;
                Notify();
            }
        }

        public void AddListener(Action<T> listener)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(LiveData<T>));

            _listeners.Add(listener);
            listener.Invoke(_value);
        }

        public void RemoveListener(Action<T> listener)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(LiveData<T>));

            _listeners.Remove(listener);
        }

        private void Notify()
        {
            foreach (Action<T> listener in _listeners)
                listener.Invoke(_value);
        }

        public void Dispose()
        {
            _listeners.Clear();
            _isDisposed = true;
        }
    }
}