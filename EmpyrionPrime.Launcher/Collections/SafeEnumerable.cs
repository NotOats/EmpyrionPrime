using System.Collections;

namespace EmpyrionPrime.Launcher.Collections;

internal class SafeEnumerable<T> : IEnumerable<T>
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
