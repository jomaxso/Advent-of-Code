using FluentAssertions;
using Solution;

namespace Test;

public sealed class MultiplierTests
{
    [Fact]
    public void ReturnsCorrectResultWhenMulEnabled()
    {
        const string input = "do()mul(2,3)mul(4,5)";
        var isEnabled = true;
        var result = Multiplier.UnsafeGetResult(input, ref isEnabled);
        result.Should().Be(26);
    }

    [Fact]
    public void ReturnsZeroWhenMulDisabled()
    {
        const string input = "don't()mul(2,3)mul(4,5)";
        var isEnabled = true;
        var result = Multiplier.UnsafeGetResult(input, ref isEnabled);
        result.Should().Be(0);
    }

    [Fact]
    public void HandlesMixedDoAndDontInstructions()
    {
        const string input = "do()mul(2,3)dxdon't()mul(4,5)do()mul(1,1)";
        var isEnabled = true;
        var result = Multiplier.UnsafeGetResult(input, ref isEnabled);
        result.Should().Be(7);
    }

    [Fact]
    public void HandlesEmptyInput()
    {
        var input = "";
        var isEnabled = true;
        var result = Multiplier.UnsafeGetResult(input, ref isEnabled);
        result.Should().Be(0);
    }

    [Fact]
    public void HandlesInvalidMulFormat()
    {
        const string input = "do()mul(2,3)mul(4,5)mul(abc)";
        var isEnabled = true;
        var result = Multiplier.UnsafeGetResult(input, ref isEnabled);
        result.Should().Be(26);
    }

    [Fact]
    public void HandlesMulWithMoreThanThreeDigits()
    {
        const string input = "do()mul(1234,5678)";
        var isEnabled = true;
        var result = Multiplier.UnsafeGetResult(input, ref isEnabled);
        result.Should().Be(0);
    }
}