using Rustic.TabularDataReader.Interfaces;
using Rustic.TabularDataReader.Services;
using SimpleInjector;

namespace Rustic.TabularDataReader;

internal static class InternalContainer
{
    internal static readonly Container container = new();

    static InternalContainer()
    {
        container.Register<ICsvReader, CsvReader>(Lifestyle.Transient);
        container.Register<ISpreadsheetReader, SpreadsheetReader>(Lifestyle.Transient);
        container.Verify();
    }

    public static T GetInstance<T>() where T : class
    {
        return container.GetInstance<T>();
    }
}