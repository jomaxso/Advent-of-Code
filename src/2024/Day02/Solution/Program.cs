using System.Diagnostics;
using Solution;

_ = GC.GetTotalAllocatedBytes();
var stamp = Stopwatch.GetTimestamp();

var lineCount = 0;
var safeReports = 0;
var possibleSafeReports = 0;
long allocatedBytes = 0;

await foreach (var reportString in File.ReadLinesAsync("input.txt"))
{
    var before = GC.GetTotalAllocatedBytes();
    lineCount++;

    if (Report.IsSafe(reportString))
    {
        safeReports++;
    }

    if (Report.IsPossiblySafe(reportString))
    {
        possibleSafeReports++;
    }

    var after = GC.GetTotalAllocatedBytes();
    allocatedBytes += after - before;
}

var elapsed = Stopwatch.GetTimestamp() - stamp;

Console.WriteLine($"Line {lineCount}: Memory: {allocatedBytes / 1024} KB");
Console.WriteLine($"Elapsed: {TimeSpan.FromTicks(elapsed).Milliseconds} ms");
Console.WriteLine($"Safe reports: {safeReports} out of {lineCount}");
Console.WriteLine($"Possible safe reports: {possibleSafeReports} out of {lineCount}");