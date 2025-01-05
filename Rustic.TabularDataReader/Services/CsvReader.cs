using Rustic.TabularDataReader.Enums;
using Rustic.TabularDataReader.Interfaces;
using Rustic.TabularDataReader.Models;
using System.Text;

namespace Rustic.TabularDataReader.Services;

internal class CsvReader : ICsvReader
{
    static readonly HashSet<string> _supportedExtensions = [".csv", ".txt"];

    public bool CanHandle(string extension)
    {
        return _supportedExtensions.Contains(extension);
    }

    public OperationResult<string> Read(byte[] bytea)
    {
        OperationResult<string> result = new();
        try
        {
            result.Content = Encoding.UTF8.GetString(bytea);
            result.OperationStatus = OperationStatus.Succeeded;
        }
        catch (Exception ex)
        {
            result.ErrorMessage = ex.Message;
            result.OperationStatus = OperationStatus.Faulted;
        }
        return result;
    }
}