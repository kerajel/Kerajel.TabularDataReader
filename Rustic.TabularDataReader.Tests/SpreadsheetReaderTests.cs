using FluentAssertions;
using Rustic.TabularDataReader.Enums;
using Rustic.TabularDataReader.Interfaces;
using Rustic.TabularDataReader.Models;

namespace Rustic.TabularDataReader.Tests;

public class SpreadsheetReaderTests
{
    readonly ISpreadsheetReader _sut = InternalContainer.GetInstance<ISpreadsheetReader>();

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
5,Broccoli,Green";

        string[] expectedLines = expectedContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        string[] resultLines = result.Content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        resultLines.Should().BeEquivalentTo(expectedLines);
        result.OperationStatus.Should().Be(OperationStatus.Succeeded);
    }
}