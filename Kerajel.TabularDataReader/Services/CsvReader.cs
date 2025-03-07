using Kerajel.Primitives.Enums;
using Kerajel.Primitives.Models;
using Kerajel.TabularDataReader.Interfaces;
using System.Text;

namespace Kerajel.TabularDataReader.Services;

internal class CsvReader : ICsvReader
{
    static readonly HashSet<string> _supportedExtensions = [".csv", ".txt"];

    public bool CanHandle(string extension)
    {
        return _supportedExtensions.Contains(extension);
    }

    public Task<OperationResult<string>> Read(byte[] bytea)
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
        return Task.FromResult(result);
    }

    public async Task<OperationResult<string>> Read(string filePath)
    {
        OperationResult<string> result = new();
        try
        {
            result.Content = await File.ReadAllTextAsync(filePath);
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