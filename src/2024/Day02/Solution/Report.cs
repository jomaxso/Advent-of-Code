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
    
    public static bool IsPossiblySafe(ReadOnlySpan<char> report)
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

        return IsSafe(levels) || CouldBeSafe(levels);
    }
    
    public static bool IsSafe(ReadOnlySpan<int> report)
    {
        if (report.Length < 3)
            return IsIncreasing(report[0], report[1]) is not null;

        var currentIndex = 1;

        while (currentIndex < report.Length - 1)
        {
            var previousLevel = report[currentIndex - 1];
            var currentLevel = report[currentIndex];
            var nextLevel = report[currentIndex + 1];

            var diffOne = IsIncreasing(previousLevel, currentLevel);
            var diffTwo = IsIncreasing(currentLevel, nextLevel);
            
            if(diffOne is null || diffTwo is null) 
            {
                return false;
            }

            if (diffOne != diffTwo)
            {
                return false;
            }
            
            currentIndex++;
        }

        return true;
    }

    public static bool CouldBeSafe(ReadOnlySpan<int> report)
    {
        Span<int> modifiedReport = stackalloc int[report.Length - 1];

        for (var i = 0; i < report.Length; i++)
        {
            modifiedReport.Clear();

            report[..i].CopyTo(modifiedReport);
            report[(i + 1)..].CopyTo(modifiedReport[i..]);

            if (IsSafe(modifiedReport))
            {
                return true;
            }
        }

        return false;
    }
    
    private static bool? IsIncreasing(int current, int next)
    {
        return (next - current) switch
        {
            1 or 2 or 3 => true,
            -1 or -2 or -3 => false,
            _ => null
        };
    }
}