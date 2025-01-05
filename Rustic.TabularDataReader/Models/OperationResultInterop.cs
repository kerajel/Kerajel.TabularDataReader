using Rustic.TabularDataReader.Enums;
using System.Runtime.InteropServices;

namespace Rustic.TabularDataReader.Models;

[StructLayout(LayoutKind.Sequential)]
internal struct OperationResultInterop
{
    public nint Result; 
    public OperationStatus OperationStatus;
    public nint ErrorMessage;
}