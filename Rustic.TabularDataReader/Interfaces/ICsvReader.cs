using Rustic.TabularDataReader.Models;

namespace Rustic.TabularDataReader.Interfaces;

public interface ICsvReader
{
    bool CanHandle(string extension);
    OperationResult<string> Read(byte[] bytea);
}