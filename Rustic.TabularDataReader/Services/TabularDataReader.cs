using Rustic.TabularDataReader.Enums;
using Rustic.TabularDataReader.Interfaces;
using Rustic.TabularDataReader.Models;

namespace Rustic.TabularDataReader.Services;

public static class TabularDataReader
{
    public static OperationResult<string> Read(byte[] bytea, string fileName, string? sheetName = null)
    {
        string extension = Path.GetExtension(fileName);

        return ReadFromBytes(bytea, extension, sheetName);
    }

    private static OperationResult<string> ReadFromBytes(byte[] bytea, string extension, string? sheetName = null)
    {
        ISpreadsheetReader spreadsheetReader = InternalContainer.GetInstance<ISpreadsheetReader>();

        if (spreadsheetReader.CanHandle(extension))
        {
            return spreadsheetReader.Read(bytea, sheetName);
        }

        ICsvReader csvReader = InternalContainer.GetInstance<ICsvReader>();

        if (csvReader.CanHandle(extension))
        {
            return csvReader.Read(bytea);
        }

        return new OperationResult<string>
        {
            OperationStatus = OperationStatus.Faulted,
            ErrorMessage = $"Extension '{extension}' is unsupported",
        };
    }
}