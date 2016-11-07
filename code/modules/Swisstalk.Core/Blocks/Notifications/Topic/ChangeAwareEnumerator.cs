using System;
using System.Collections.Generic;
using Swisstalk.Foundation.Collections.Observable;

namespace Swisstalk.Core.Blocks.Notifications.Topic
{
    internal class ChangeAwareEnumerator<T> : IDisposable
    {
        private IList<T> _collection;

        private int _nextIndex;
        private T _currentItem;

        public ChangeAwareEnumerator(IList<T> collection)
        {
            _collection = collection;

            Reset();

            BeginObserve(_collection);
        }

        public void Dispose()
        {
            EndObserve(_collection);
        }

        public T Current
        {
            get
            {
                return _currentItem;
            }
        }

        public bool MoveNext()
        {
            bool hasNext = (_nextIndex < _collection.Count);

            if (hasNext)
            {
                _currentItem = _collection[_nextIndex];
                _nextIndex++;
            }

            return hasNext;
        }

        public void Reset()
        {
            _nextIndex = 0;
            _currentItem = default(T);
        }

        private bool Active
        {
            get
            {
                return (_nextIndex > 0);
            }
        }

        private void BeginObserve(ICollection<T> collection)
        {
            INotifyCollectionChanged changeNotifier = GetNotifier(collection);
            changeNotifier.CollectionChanged += CollectionChanged;
        }

        private void EndObserve(ICollection<T> collection)
        {
            INotifyCollectionChanged changeNotifier = GetNotifier(collection);
            changeNotifier.CollectionChanged -= CollectionChanged;
        }

        private INotifyCollectionChanged GetNotifier(ICollection<T> collection)
        {
            INotifyCollectionChanged changeNotifier = collection as INotifyCollectionChanged;

            if (null == changeNotifier)
            {
                throw new ArgumentException("Collection must support INotifyCollectionChanged interface");
            }

            return changeNotifier;
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (Active)
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    PatchEnumerationIndex(e.NewStartingIndex, e.NewItems.Count);
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    PatchEnumerationIndex(e.OldStartingIndex, e.OldItems.Count);
                }
                else if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    ResetEnumerationIndex();
                }
            }
        }

        private void PatchEnumerationIndex(int modificationIndex, int itemCount)
        {
            if (modificationIndex < _nextIndex)
            {
                _nextIndex = Math.Max(0, _nextIndex - itemCount);
            }
        }

        private void ResetEnumerationIndex()
        {
            _nextIndex = 0;
        }
    }
}
