using FluentAssertions;
using Solution;

namespace Test;

public class GridTests
{
    [Fact]
    public void Create_GridWithValidSourceAndWidth_ReturnsGrid()
    {
        var source = "abcdef".AsSpan();
        var width = 3;

        var grid = Grid.Create(source, width);

        grid.Source.ToArray().Should().BeEquivalentTo(source.ToArray());
        grid.Width.Should().Be(width);
    }

    [Fact]
    public void AsGrid_SpanWithValidSourceAndWidth_ReturnsGrid()
    {
        var source = "abcdef".AsSpan();
        var width = 3;

        var grid = source.AsGrid(width);

        grid.Source.ToArray().Should().BeEquivalentTo(source.ToArray());
        grid.Width.Should().Be(width);
    }

    [Fact]
    public void CopyGridTo_ValidStringArray_CopiesToDestination()
    {
        var source = new[] { "abc", "def" };
        var destination = new char[6];
        var rowLength = 2;

        source.CopyGridTo(destination, rowLength);

        destination.Should().BeEquivalentTo("abcdef".ToCharArray());
    }

    [Fact]
    public void CopyGridTo_InvalidRowLength_ThrowsArgumentException()
    {
        var source = new[] { "abc", "def" };
        var destination = new char[6];
        var rowLength = 3;

        Action act = () => source.CopyGridTo(destination, rowLength);

        act.Should().Throw<IndexOutOfRangeException>();
    }

    [Fact]
    public void CopyGridTo_InconsistentRowLengths_ThrowsArgumentException()
    {
        var source = new[] { "abc", "de" };
        var destination = new char[5];
        var rowLength = 2;

        Action act = () => source.CopyGridTo(destination, rowLength);

        act.Should().Throw<ArgumentException>();
    }
}