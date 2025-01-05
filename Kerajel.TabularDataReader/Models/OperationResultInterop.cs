using Kerajel.TabularDataReader.Enums;
using System.Runtime.InteropServices;

namespace Kerajel.TabularDataReader.Models;

[StructLayout(LayoutKind.Sequential)]
internal struct OperationResultInterop
{
    public nint Result;
    public OperationStatus OperationStatus;
    public nint ErrorMessage;
}