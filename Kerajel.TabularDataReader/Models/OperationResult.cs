using Kerajel.TabularDataReader.Enums;

namespace Kerajel.TabularDataReader.Models;

public class OperationResult<T>
{
    public T? Content { get; set; }

    public OperationStatus? OperationStatus { get; set; }

    public string? ErrorMessage { get; set; }
}