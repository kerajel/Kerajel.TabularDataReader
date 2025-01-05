using Kerajel.TabularDataReader.Models;

namespace Kerajel.TabularDataReader.Interfaces;

public interface ICsvReader
{
    bool CanHandle(string extension);
    OperationResult<string> Read(byte[] bytea);
}