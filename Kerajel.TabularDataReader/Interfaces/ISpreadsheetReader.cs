using Kerajel.TabularDataReader.Models;

namespace Kerajel.TabularDataReader.Interfaces;

public interface ISpreadsheetReader
{
    bool CanHandle(string extension);

    OperationResult<string> Read(byte[] byteArray, string? sheetName = null);
}