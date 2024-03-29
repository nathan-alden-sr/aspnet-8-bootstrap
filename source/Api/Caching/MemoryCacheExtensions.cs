using Microsoft.Extensions.Caching.Memory;

namespace Company.Product.WebApi.Api.Caching;

public static class MemoryCacheExtensions
{
    private static readonly object LockObject = new();

    public static Lazy<TItem> ThreadSafeGetOrCreate<TItem>(
        this IMemoryCache memoryCache,
        object key,
        Func<ICacheEntry, Lazy<TItem>> factory,
        MemoryCacheEntryOptions? options = null)
    {
        lock (LockObject)
        {
            var entry = memoryCache.GetOrCreate(
                key,
                entry =>
                {
                    if (options is not null)
                    {
                        _ = entry.SetOptions(options);
                    }

                    return factory(entry);
                });

            ThrowIfNull(entry);

            return entry;
        }
    }

    public static TItem ThreadSafeLazyGetOrCreate<TItem>(
        this IMemoryCache memoryCache,
        object key,
        Func<ICacheEntry, TItem> factory,
        MemoryCacheEntryOptions? options = null) =>
        ThreadSafeGetOrCreate(memoryCache, key, entry => new Lazy<TItem>(() => factory(entry)), options).Value;
}
