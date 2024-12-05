using System.Diagnostics;
using Solution;

var source = await File.ReadAllLinesAsync("input.txt");

// Initialize grid
var startInit = Stopwatch.GetTimestamp();

var rows = source.Length;
var columns = source[0].Length;

Span<char> compressed = stackalloc char[rows * columns];
source.CopyGridTo(compressed, rows);
var grid = compressed.AsGrid(columns);

var endeInit = Stopwatch.GetTimestamp();
var elapsedInit = TimeSpan.FromTicks(endeInit - startInit);
Console.WriteLine($"Initialized grid in {elapsedInit.TotalMilliseconds} ms");

// Solution 1
var start1 = Stopwatch.GetTimestamp();

const string word1 = "XMAS";
var counts = LinearSearch.Count(grid, word1);

var ende1 = Stopwatch.GetTimestamp();
var elapsed1 = TimeSpan.FromTicks(ende1 - start1);
Console.WriteLine($"Found {counts} occurrences of '{word1}' in {elapsed1.TotalMilliseconds} ms");

// Solution 2
var start2 = Stopwatch.GetTimestamp();

const string word2 = "MAS";
var counts2 = XSearch.Count(grid, word2);

var ende2 = Stopwatch.GetTimestamp();
var elapsed2 = TimeSpan.FromTicks(ende2 - start2);
Console.WriteLine($"Found {counts2} occurrences of '{word2}' in {elapsed2.TotalMilliseconds} ms");