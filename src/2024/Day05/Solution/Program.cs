await foreach (var line in File.ReadLinesAsync("input.txt"))
{
    var span = line.AsSpan();
    Console.WriteLine(span);
}
