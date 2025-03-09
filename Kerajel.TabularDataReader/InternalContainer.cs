using Kerajel.TabularDataReader.Interfaces;
using Kerajel.TabularDataReader.Services;
using Kerajel.TabularDataReader.Services;
using SimpleInjector;

namespace Kerajel.TabularDataReader;

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