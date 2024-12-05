using FluentAssertions;
using Solution;

namespace Test;

public class LinearSearchTests
{
    private const string TestInput = """
                                     ....XXMAS.
                                     .SAMXMS...
                                     ...S..A...
                                     ..A.A.MS.X
                                     XMASAMX.MM
                                     X.....XA.A
                                     S.S.S.S.SS
                                     .A.A.A.A.A
                                     ..M.M.M.MM
                                     .X.X.XMASX
                                     """;

    [Fact]
    public void Count_GridWithSingleOccurrence_ReturnsOne()
    {
        var grid = new Grid<char>(TestInput.Replace("\r\n", ""), 10);
        var word = "XMAS".AsSpan();

        var result = LinearSearch.Count(grid, word);

        result.Should().Be(18); // Update the expected count to 3
    }

    [Fact]
    public void Count_GridWithMultipleOccurrences_ReturnsCorrectCount()
    {
        var grid = new Grid<char>("XMASAMX", 7);
        var word = "XMAS".AsSpan();

        var result = LinearSearch.Count(grid, word);

        result.Should().Be(2);
    }

    [Fact]
    public void Count_GridWithNoOccurrences_ReturnsZero()
    {
        var grid = new Grid<char>(TestInput.Replace("\r\n", ""), 10);
        var word = "XYZ".AsSpan();

        var result = LinearSearch.Count(grid, word);

        result.Should().Be(0);
    }
}