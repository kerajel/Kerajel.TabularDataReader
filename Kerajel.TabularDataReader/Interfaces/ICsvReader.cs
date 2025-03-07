using Kerajel.Primitives.Models;

namespace Kerajel.TabularDataReader.Interfaces;

public interface ICsvReader
{
    bool CanHandle(string extension);
    Task<OperationResult<string>> Read(byte[] bytea);
    Task<OperationResult<string>> Read(string filePath);
}