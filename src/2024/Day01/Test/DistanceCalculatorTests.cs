using FluentAssertions;
using Solution;

namespace Test;

public sealed class DistanceCalculatorTests
{
    [Fact]
    public void TestExampleCase()
    {
        // Arrange
        List<int> leftList = [3, 4, 2, 1, 3, 3];
        List<int> rightList = [4, 3, 5, 3, 9, 3];

        const int expectedDistance = 11;

        // Act
        var actualDistance = leftList.DistanceTo(rightList);

        // Assert
        actualDistance.Should().Be(expectedDistance);
    }
}