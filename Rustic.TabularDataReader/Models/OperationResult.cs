using Rustic.TabularDataReader.Enums;

namespace Rustic.TabularDataReader.Models;

public class OperationResult<T>
{
    public T? Content { get; set; }

    public OperationStatus? OperationStatus { get; set; }

    public string? ErrorMessage { get; set; }
}