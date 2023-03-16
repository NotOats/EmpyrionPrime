using System.Collections;

namespace EmpyrionPrime.Launcher.Extensions;

internal static class EnumerableExtensions
{
    public static IEnumerable<T> AsLocked<T>(this IEnumerable<T> enumerable, object syncLock)
    {
        return new SafeEnumerable<T>(enumerable, syncLock);
    }

    private class SafeEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _inner;
        private readonly object _syncLock;

        public SafeEnumerable(IEnumerable<T> inner, object syncLock)
        {
            _inner = inner;
            _syncLock = syncLock;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SafeEnumerator<T>(_inner.GetEnumerator(), _syncLock);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    private class SafeEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;
        private readonly object _syncLock;
        public SafeEnumerator(IEnumerator<T> inner, object syncLock)
        {
            _inner = inner;
            _syncLock = syncLock;

            Monitor.Enter(_syncLock);
        }

        public void Dispose()
        {
            Monitor.Exit(_syncLock);
        }

        public T Current => _inner.Current;

        object IEnumerator.Current => Current!;

        public bool MoveNext()
        {
            return _inner.MoveNext();
        }

        public void Reset()
        {
            _inner.Reset();
        }
    }
}
