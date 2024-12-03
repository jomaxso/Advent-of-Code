var aggregate = 0;
var isDo = true;

await foreach (var line in File.ReadLinesAsync("input.txt"))
{
    aggregate += Solution.Multiplier.UnsafeGetResult(line, ref isDo);
}

Console.WriteLine($"The sum of all multiplications is {aggregate}");