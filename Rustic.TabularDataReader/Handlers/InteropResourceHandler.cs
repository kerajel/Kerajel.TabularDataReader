namespace Rustic.TabularDataReader.Handlers;

internal class InteropResourceHandler<T> : IDisposable where T : struct
{
    public T Resource { get; private set; }

    readonly Action<T> _cleanupFunc;
    bool _disposed = false;


    public InteropResourceHandler(T resource, Action<T> cleanupFunc)
    {
        Resource = resource;
        _cleanupFunc = cleanupFunc;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            _cleanupFunc(Resource);
            _disposed = true;
        }
    }

    ~InteropResourceHandler()
    {
        Dispose(false);
    }
}