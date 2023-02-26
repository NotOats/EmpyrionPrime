namespace EmpyrionPrime.Launcher.Collections;

internal static class EnumerableExtensions
{
    public static IEnumerable<T> AsLocked<T>(this IEnumerable<T> enumerable, object syncLock)
    {
        return new SafeEnumerable<T>(enumerable, syncLock);
    }
}
