using System.Collections;

namespace EmpyrionPrime.Launcher.Collections;

internal class SafeEnumerator<T> : IEnumerator<T>
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

    // IEnumerator
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
