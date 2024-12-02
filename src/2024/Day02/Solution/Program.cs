using System.Diagnostics;
using Solution;

var stamp = Stopwatch.GetTimestamp();

var lineCount = 0;
var safeReports = 0;

await foreach (var reportString in File.ReadLinesAsync("input.txt"))
{
    lineCount++;
    if (Report.IsSafe(reportString))
    {
        safeReports++;
    }
}

var elapsed = Stopwatch.GetTimestamp() - stamp;

Console.WriteLine($"Elapsed: {TimeSpan.FromTicks(elapsed).Milliseconds} ms");
Console.WriteLine($"Safe reports: {safeReports} out of {lineCount}");