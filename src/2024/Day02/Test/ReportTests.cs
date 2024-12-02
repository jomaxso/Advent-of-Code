using FluentAssertions;
using Solution;

namespace Test;

public class ReportTests
{
    [Theory]
    [InlineData(new[] { 7, 6, 4, 2, 1 }, true)]
    [InlineData(new[] { 1, 2, 7, 8, 9 }, false)]
    [InlineData(new[] { 9, 7, 6, 2, 1 }, false)]
    [InlineData(new[] { 1, 3, 2, 4, 5 }, false)]
    [InlineData(new[] { 8, 6, 4, 4, 1 }, false)]
    [InlineData(new[] { 1, 3, 6, 7, 9 }, true)]
    public void IsSafe_ShouldReturnExpectedResult(int[] report, bool isSafe)
    {
        // Arrange
        ReadOnlySpan<int> reportSpan = report;
        
        // Act
        var result = Report.IsSafe(reportSpan);
        
        // Assert
        result.Should().Be(isSafe);
    }
}