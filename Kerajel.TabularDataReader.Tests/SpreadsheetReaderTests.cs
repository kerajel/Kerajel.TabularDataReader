using FluentAssertions;
using Kerajel.TabularDataReader.Enums;
using Kerajel.TabularDataReader.Interfaces;
using Kerajel.TabularDataReader.Models;

namespace Kerajel.TabularDataReader.Tests;

public class SpreadsheetReaderTests
{
    readonly ISpreadsheetReader _sut = InternalContainer.GetInstance<ISpreadsheetReader>();

    readonly string[] _newLineSeparators = ["\n", "\r\n"];

    [Fact]
    public void Read_ShouldReturnExpectedContent()
    {
        // Arrange
        string fileName = "TestData/Xlsx_Test1.xlsx";
        byte[] bytes = File.ReadAllBytes(fileName);

        // Act
        OperationResult<string> result = _sut.Read(bytes);

        // Assert
        string expectedContent =
@"ID,VegetableName,Color
1,Tomato,Red
2,Cucumber,Green
3,Carrot,Orange
4,Bell Pepper,Yellow
5,Broccoli,Green
";

        string[] expectedLines = expectedContent.Split(_newLineSeparators, StringSplitOptions.RemoveEmptyEntries);
        string[] resultLines = result.Content.Split(_newLineSeparators, StringSplitOptions.RemoveEmptyEntries);

        resultLines.Should().BeEquivalentTo(expectedLines);
        result.OperationStatus.Should().Be(OperationStatus.Succeeded);
    }
}