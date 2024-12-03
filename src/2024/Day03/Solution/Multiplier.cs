using System.Text.RegularExpressions;

namespace Solution;

public static partial class Multiplier
{
    public static int UnsafeGetResult(ReadOnlySpan<char> input, ref bool isEnabled)
    {
        var result = 0;

        foreach (var match in Instruction.EnumerateMatches(input))
        {
            var instruction = input.Slice(match.Index, match.Length);

            if (instruction.StartsWith("do(") && instruction.EndsWith(")"))
            {
                isEnabled = true;
                continue;
            }

            if (instruction.StartsWith("don't(") && instruction.EndsWith(")"))
            {
                isEnabled = false;
                continue;
            }
            
            if (isEnabled is false || (instruction.StartsWith("mul(") && instruction.EndsWith(")")) is false)
            {
                continue;
            }

            var (x, y) = GetNumbers(input, match);
            result += x * y;
        }

        return result;
    }

    private static (int X, int Y) GetNumbers(ReadOnlySpan<char> input, ValueMatch match)
    {
        var start = match.Index + 4;
        var length = match.Length - 4 - 1;
        var target = input.Slice(start, length);

        var commaTarget = target.IndexOf(',');

        var x = int.Parse(target[..commaTarget]);
        var y = int.Parse(target[(commaTarget + 1)..]);
        
        return (x, y);
    }

    [GeneratedRegex(@"mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\)")]
    private static partial Regex Instruction { get; }
}