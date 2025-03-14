# Kerajel.TabularDataReader

Kerajel.TabularDataReader is a C# library designed to seamlessly convert spreadsheet files (XLS, XLSX, etc.) to CSV format. Leveraging .NET’s `AnonymousPipeServerStream` for efficient data streaming, it minimizes memory usage and overhead when handling large or complex workbooks.

## Features

- **High-Performance Data Streaming:** Uses .NET’s `AnonymousPipeServerStream` to transform spreadsheet content into CSV with minimal memory footprint.
- **Broad Spreadsheet Format Support:** Compatible with XLS, XLSX, XLSB and plain text formats.
- **Target Sheet Selection:** Offers optional configuration to process a specific sheet from the workbook.

## License
MIT Licensed.
