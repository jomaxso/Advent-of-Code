namespace Solution;

public static class Report
{
    public static bool IsSafe(ReadOnlySpan<char> report)
    {
        var reportSpan = report.Trim();
        var spaceCount = reportSpan.Count(' ');

        Span<int> levels = stackalloc int[spaceCount + 1];

        var index = 0;
        foreach (var range in reportSpan.Split(' '))
        {
            levels[index] = int.Parse(reportSpan[range]);
            index++;
        }

        return IsSafe(levels);
    }

    public static bool IsSafe(ReadOnlySpan<int> report)
    {
        if (report.Length < 2)
            return true;

        var previousLevel = report[0];
        var isIncreasing = report[1] > previousLevel;

        for (var i = 1; i < report.Length; i++)
        {
            var level = report[i];

            var isSafe = (level - previousLevel) switch
            {
                1 or 2 or 3 when isIncreasing => true,
                -1 or -2 or -3 when !isIncreasing => true,
                _ => false
            };

            if (!isSafe) return false;

            previousLevel = level;
        }

        return true;
    }
}