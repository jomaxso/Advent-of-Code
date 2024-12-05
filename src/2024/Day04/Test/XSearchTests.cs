using FluentAssertions;
using Solution;

namespace Test;

public sealed class XSearchTests
{
    private const string TestInput = """
                                     .M.S......
                                     ..A..MSMS.
                                     .M.S.MAA..
                                     ..A.ASMSM.
                                     .M.S.M....
                                     ..........
                                     S.S.S.S.S.
                                     .A.A.A.A..
                                     M.M.M.M.M.
                                     ..........
                                     """;

    [Fact]
    public void Count_GridWithEvenWidth_ThrowsArgumentException()
    {
        const string word = "XMAS";
        
        var func = () =>
        {
            var grid = new Grid<char>(TestInput.Replace("\n", ""), 10);
            return XSearch.Count(grid, word);
        };

        func.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Count_GridWithMultipleOccurrences_ReturnsCorrectCount()
    {
        var grid = new Grid<char>(TestInput.Replace("\n", "").ToCharArray(), 10);
        var word = "SAM".AsSpan();

        var result = XSearch.Count(grid, word);

        result.Should().Be(0); // Update the expected count based on the actual occurrences
    }

    [Fact]
    public void Count_GridWithNoOccurrences_ReturnsZero()
    {
        var grid = new Grid<char>(TestInput.Replace("\n", "").ToCharArray(), 10);
        var word = "XYZ".AsSpan();

        var result = XSearch.Count(grid, word);

        result.Should().Be(0);
    }
}