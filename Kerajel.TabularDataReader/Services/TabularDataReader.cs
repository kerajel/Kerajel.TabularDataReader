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

        return await ReadFromBytes(buffer, extension, sheetName);
    }

    public static async Task<OperationResult<string>> Read(byte[] bytea, string fileName, string? sheetName = null)
    {
        string extension = Path.GetExtension(fileName);

        return await ReadFromBytes(bytea, extension, sheetName);
    }

    public static async Task<OperationResult<string>> Read(string filePath, string? sheetName = null)
    {
        return await ReadFromFilePath(filePath, sheetName);
    }

    private static async Task<OperationResult<string>> ReadFromBytes(byte[] bytea, string extension, string? sheetName = null)
    {
        ISpreadsheetReader spreadsheetReader = InternalContainer.GetInstance<ISpreadsheetReader>();

        if (spreadsheetReader.CanHandle(extension))
        {
            return spreadsheetReader.Read(bytea, sheetName);
        }

        ICsvReader csvReader = InternalContainer.GetInstance<ICsvReader>();

        if (csvReader.CanHandle(extension))
        {
            return await csvReader.Read(bytea);
        }

        return new OperationResult<string>
        {
            OperationStatus = OperationStatus.Faulted,
            ErrorMessage = $"Extension '{extension}' is unsupported",
        };
    }

    private static async Task<OperationResult<string>> ReadFromFilePath(string filePath, string? sheetName = null)
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
            return await csvReader.Read(filePath);
        }

        return new OperationResult<string>
        {
            OperationStatus = OperationStatus.Faulted,
            ErrorMessage = $"Extension '{extension}' is unsupported",
        };
    }
}