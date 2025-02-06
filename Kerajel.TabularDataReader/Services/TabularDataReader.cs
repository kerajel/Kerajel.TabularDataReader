using Kerajel.Primitives.Enums;
using Kerajel.Primitives.Models;
using Kerajel.TabularDataReader.Helpers;
using Kerajel.TabularDataReader.Interfaces;

namespace Kerajel.TabularDataReader.Services;

public static class TabularDataReader
{
    public static async Task<OperationResult<string>> Read(Stream stream, string fileName, string? sheetName = null)
    {
        string extension = Path.GetExtension(fileName);

        byte[] buffer = await stream.ReadToByteArrayAsync();

        return ReadFromBytes(buffer, extension, sheetName);
    }

    public static OperationResult<string> Read(byte[] bytea, string fileName, string? sheetName = null)
    {
        string extension = Path.GetExtension(fileName);

        return ReadFromBytes(bytea, extension, sheetName);
    }

    public static OperationResult<string> Read(string filePath, string? sheetName = null)
    {
        return ReadFromFilePath(filePath, sheetName);
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

    private static OperationResult<string> ReadFromFilePath(string filePath, string? sheetName = null)
    {
        ISpreadsheetReader spreadsheetReader = InternalContainer.GetInstance<ISpreadsheetReader>();

        string extension = Path.GetExtension(filePath);

        if (spreadsheetReader.CanHandle(extension))
        {
            return spreadsheetReader.Read(filePath, sheetName);
        }

        ICsvReader csvReader = InternalContainer.GetInstance<ICsvReader>();

        if (csvReader.CanHandle(extension))
        {
            //return csvReader.Read(filePath);
        }

        return new OperationResult<string>
        {
            OperationStatus = OperationStatus.Faulted,
            ErrorMessage = $"Extension '{extension}' is unsupported",
        };
    }
}