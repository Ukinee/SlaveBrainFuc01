using System;
using System.Collections.Generic;
using Codebase.Core.Common.General.Utils;

namespace Codebase.Core.Common.General.LiveDatas
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

            if (_listeners.Contains(listener))
            {
                MaloyAlert.Warning($"Listener {listener.Method.Name} already added to LiveData");
                return;
            }

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