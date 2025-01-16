using Kerajel.TabularDataReader.Enums;
using Kerajel.TabularDataReader.Interfaces;
using Kerajel.TabularDataReader.Models;
using Shouldly;

namespace Kerajel.TabularDataReader.Tests;

public class CsvReaderTests
{
    readonly ICsvReader _sut = InternalContainer.GetInstance<ICsvReader>();

    [Fact]
    public void Read_ShouldReturnExpectedContent()
    {
        // Arrange
        string fileName = "TestData/Csv_Test1.txt";
        byte[] bytes = File.ReadAllBytes(fileName);

        // Act
        OperationResult<string> result = _sut.Read(bytes);

        // Assert
        string expectedContent =
@"UserID|FirstName|LastName|Email|JoinDate
1|John|Doe|john.doe@example.com|2021-01-08
2|Jane|Smith|jane.smith@example.com|2021-02-15
3|Sam|Brown|sam.brown@example.com|2021-03-22
4|Lisa|White|lisa.white@example.com|2021-04-30
5|Mark|Black|mark.black@example.com|2021-05-16";

        result.Content.ShouldBe(expectedContent);
        result.OperationStatus.ShouldBe(OperationStatus.Succeeded);
    }
}