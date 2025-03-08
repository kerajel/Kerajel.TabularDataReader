# Kerajel.TabularDataReader

Kerajel.TabularDataReader is a C# wrapper that simplifies access to a native library for converting spreadsheets (XLS, XLSX, etc.) to CSV format. It leverages the native library ([Kerajel.TabularDataReader.Native](https://github.com/kerajel/TabularDataReader.Native)) and [Kerajel.Primitives](https://github.com/kerajel/Primitives) for robust result handling via the `OperationResult` model.

## Features
- **Versatile Input:** Supports both file paths and raw byte arrays.
- **Simple API:** Provides clear success/error handling using `OperationResult`.
- **Automated Memory Management:** Ensures proper resource cleanup.

## Usage Examples

### Converting Using a File Path
    var result = await TabularDataReader.Read("path/to/spreadsheet.xlsx", "Sheet1");
    if (result.OperationStatus == OperationStatus.Succeeded)
    {
        Console.WriteLine("CSV Output:");
        Console.WriteLine(result.Content);
    }
    else
    {
        Console.WriteLine("Error: " + result.ErrorMessage);
    }

### Converting Using a Byte Array
    byte[] fileBytes = File.ReadAllBytes("path/to/spreadsheet.xlsx");
    var result = await TabularDataReader.Read(fileBytes, "spreadsheet.xlsx", "Sheet1");
    if (result.OperationStatus == OperationStatus.Succeeded)
    {
        Console.WriteLine("CSV Output:");
        Console.WriteLine(result.Content);
    }
    else
    {
        Console.WriteLine("Error: " + result.ErrorMessage);
    }

## License
MIT Licensed.
