using Kerajel.Primitives.Enums;
using Kerajel.Primitives.Models;
using Kerajel.TabularDataReader.Handlers;
using Kerajel.TabularDataReader.Interfaces;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Rustic.TabularDataReader.Services;

internal partial class SpreadsheetReader : ISpreadsheetReader
{
    const string DllName = "lib/kerajel_tabular_data_reader.dll";

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial OperationResultInterop excel_to_csv(byte[] bytes, ulong len, string? sheetName = null);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial OperationResultInterop excel_to_csv_by_path(string filePath, string? sheetName = null);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void free_operation_result(OperationResultInterop result);

    readonly HashSet<string> _supportedExtensions = [".xls", ".xlsx", ".xlsm", ".xlsb", ".xla", ".xlam", ".ods"];

    public bool CanHandle(string extension)
    {
        return _supportedExtensions.Contains(extension);
    }

    public OperationResult<string> Read(byte[] byteArray, string? sheetName = null)
    {
        OperationResultInterop interopResult = excel_to_csv(byteArray, (ulong)byteArray.Length, sheetName);
        using InteropResourceHandler<OperationResultInterop> handler = new(interopResult, free_operation_result);
        return MarshalInteropResult(handler.Resource);
    }

    public OperationResult<string> Read(string filePath, string? sheetName = null)
    {
        OperationResultInterop interopResult = excel_to_csv_by_path(filePath, sheetName);
        using InteropResourceHandler<OperationResultInterop> handler = new(interopResult, free_operation_result);
        return MarshalInteropResult(handler.Resource);
    }

    private static OperationResult<string> MarshalInteropResult(OperationResultInterop interopResult)
    {
        if (interopResult.OperationStatus == OperationStatus.Succeeded)
        {
            string result = Marshal.PtrToStringUTF8(interopResult.Result) ?? string.Empty;
            return new OperationResult<string>
            {
                OperationStatus = OperationStatus.Succeeded,
                Content = result,
            };
        }
        else
        {
            string errorMessage = Marshal.PtrToStringUTF8(interopResult.ErrorMessage) ?? "Unknown error occurred";
            return new OperationResult<string>
            {
                OperationStatus = OperationStatus.Faulted,
                ErrorMessage = errorMessage
            };
        }
    }
}